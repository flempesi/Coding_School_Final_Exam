using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Impl;
using WindowsFormsApp1.Storages;

namespace WindowsFormsApp1.Methods {
    class CourseSchedulerDataMethods {

        private CourseSchedulerDGVMethods _CourseSchedulerDVGMethods = new CourseSchedulerDGVMethods();
        private Storage _Storage = new Storage();


        public void DeleteSchedule(DataGridView dGVSchedule, University newUniversity, bool hasDeletedRecords, Action<object, DataGridViewCellEventArgs> dataGridViewSchedules_DeleteButton_CellClick) {
            if (dGVSchedule.SelectedRows.Count == 1) {
                int r = dGVSchedule.SelectedRows[0].Index;
                DataGridViewRow rowSchedule = dGVSchedule.SelectedRows[0];
                Guid scheduleID = Guid.Parse(rowSchedule.Cells["Id"].Value.ToString());
                Schedule schedule = newUniversity.ScheduleList.Find(x => x.Id == scheduleID);
                Course course = newUniversity.Professors.Find(x => x.Id == schedule.ProfessorID).Courses.Find(x => x.Id == schedule.CourseID);
                DialogResult result = MessageBox.Show("Are you sure to delete this record ? ", "Delete record", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) {
                    hasDeletedRecords = true;
                    newUniversity.ScheduleList.Remove(schedule);
                    newUniversity.Professors.Find(x => x.Id == schedule.ProfessorID).Courses.Remove(course);
                    //remove too from student
                    foreach (Student student in newUniversity.Students) {
                        Course courseStudent=newUniversity.Students.Find(x=>x.Id==student.Id).Courses.Find(x=>x.Id== course.Id);
                        if (courseStudent != null) {
                            newUniversity.Students.Find(x => x.Id == student.Id).Courses.Remove(courseStudent);
                        }
                    }
                   
                    _CourseSchedulerDVGMethods.RefreshDataGridSchedule(dGVSchedule, newUniversity, dataGridViewSchedules_DeleteButton_CellClick);

                    SaveChanges(newUniversity);
                }
            }
        }
        public void SaveButtonActions(DataGridView dGVSchedule, Form form, bool hasDeletedRecords, University newUniversity) {

            if (dGVSchedule.Rows.Count > 0) {
                SaveChanges(newUniversity);
                form.Close();
            }
            else if (hasDeletedRecords == true) {
                form.Close();
            }
            else {
                MessageBox.Show("Please add a schedule to save it");
            }
        }
        public void SaveChanges(University newUniversity) {
            _Storage.NewUniversity = newUniversity;
            _Storage.SerializeToJson();
        }
        public void AddSchedule(DataGridView dGVCourses, DataGridView dGVProfessors, DataGridView dGVSchedule, University newUniversity, DateTimePicker ctrlTime, DateTimePicker ctrlDate, Action<object, DataGridViewCellEventArgs> dataGridViewSchedules_DeleteButton_CellClick) {

            if (dGVCourses.SelectedRows.Count == 1 && dGVProfessors.SelectedRows.Count == 1) {
                DataGridViewRow rowCourse = dGVCourses.SelectedRows[0];
                DataGridViewRow rowProfessor = dGVProfessors.SelectedRows[0];
                Guid courseID = Guid.Parse(rowCourse.Cells["Id"].Value.ToString());
                Guid professorID = Guid.Parse(rowProfessor.Cells["Id"].Value.ToString());
                DateTime dateTimeSchedule = GetDateTime(ctrlTime, ctrlDate);
                Schedule scheduledCourse = newUniversity.ScheduleList.Find(x => x.CourseID == courseID);
                if (scheduledCourse == null) {
                    if (CheckIfSameProfessorInSameDateTimeHasCourse(professorID, dateTimeSchedule, courseID, newUniversity) &&
                        CheckMaxCoursesForProfessor(professorID, dateTimeSchedule, newUniversity)) {
                        newUniversity.ScheduleList.Add(new Schedule(professorID, courseID, dateTimeSchedule));
                        newUniversity.Professors.Find(x => x.Id == professorID).Courses.Add(newUniversity.Courses.Find(x => x.Id == courseID));

                        _CourseSchedulerDVGMethods.RefreshDataGridSchedule(dGVSchedule, newUniversity, dataGridViewSchedules_DeleteButton_CellClick);

                    }
                }else {
                    MessageBox.Show("This course is already scheduled!");
                }
            }
        }
        private DateTime GetDateTime(DateTimePicker ctrlTime, DateTimePicker ctrlDate) {
            string timeValue = ctrlTime.Value.ToString("HH:mm:00");
            string dateValue = ctrlDate.Value.ToString("yyyy MM dd");
            string dateTimeValue = string.Format("{0} {1}", dateValue, timeValue);
            DateTime dateTime = Convert.ToDateTime(dateTimeValue);
            return dateTime;
        }

        public bool CheckIfSameProfessorInSameDateTimeHasCourse(Guid professorId, DateTime dateTime, Guid courseid, University newUniversity) {
            //  CANNOT ADD SAME   PROFESSOR IN SAME DATE & HOUR
            //cannot add same course in same date
            List<Schedule> proffesorSceduleList = new List<Schedule>();
            proffesorSceduleList = newUniversity.ScheduleList.FindAll(x => x.ProfessorID == professorId);
            foreach (var item in proffesorSceduleList) {

                if (item.DateTimeSchedule.Date.ToString("yyyyMMdd") == dateTime.Date.ToString("yyyyMMdd")) {
                    int HoursExistingCourse = newUniversity.Courses.Find(x => x.Id == item.CourseID).Hours;
                    int HoursNewCourse = newUniversity.Courses.Find(x => x.Id == courseid).Hours;

                    if (item.CourseID == courseid) {
                        MessageBox.Show("You can not add the same course in the same professor in same date");
                        return false;
                    }
                    if( !CheckbyHour(dateTime, item, HoursExistingCourse, HoursNewCourse)) {
                        return false;
                    }
                }
            }
            return true;
        }
        private static bool CheckbyHour(DateTime dateTime, Schedule item, int HoursExistingCourse, int HoursNewCourse) {
            if (item.DateTimeSchedule.Hour == dateTime.Hour) {
                MessageBox.Show("You can not add the same professor in the same time of the same date");
                return false;

            }
            else {//by hours check
                  //not having already lesson in the time of the new schedule for course
                  //looking in the existing hours of the existing scheduled course 
                  //An exei hdh mauhma se kapoio allo ma8hma
                for (int i = 1; i < HoursExistingCourse; i++) {
                    if (item.DateTimeSchedule.Hour + i == dateTime.Hour) {
                        MessageBox.Show("During of existing lesson , cant added new lesson!");
                        return false;
                    }
                }
                //not having  lesson in the time of the new schedule for course
                //looking in the new hours of th new course to schedule
                //An kata ths diarkeias toy neoy ma8hmatos exei allo ma8hma
                for (int i = 1; i < HoursNewCourse; i++) {
                    if (item.DateTimeSchedule.Hour == dateTime.Hour + i) {
                        MessageBox.Show("During of the new scheduled course , professor have already lesson!");
                        return false;
                    }
                }
            }
            return true;
        }

        public bool CheckMaxCoursesForProfessor(Guid professorId, DateTime dateTime, University newUniversity) {
            // A PROFESSOR CANNOT TEACH MORE THAN 4 COURSES PER DAY AND   20  COURSES PER WEEK
            List<Schedule> proffesorSceduleList = new List<Schedule>();
            proffesorSceduleList = newUniversity.ScheduleList.FindAll(x => x.ProfessorID == professorId);
            int coursesPerDay = 0;
            int coursesPerWeek = 0;
            CultureInfo culture = CultureInfo.CurrentCulture;

            foreach (var item in proffesorSceduleList) {

                if (item.DateTimeSchedule.Date.ToString("yyyyMMdd") == dateTime.Date.ToString("yyyyMMdd")) {
                    coursesPerDay++;
                }
                int weekNumberOfesheduledCourse = culture.Calendar.GetWeekOfYear(item.DateTimeSchedule, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

                int weekNumberOfCourse = culture.Calendar.GetWeekOfYear(dateTime.Date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                if (weekNumberOfesheduledCourse == weekNumberOfCourse) {
                    coursesPerWeek++;

                }
            }
            if (coursesPerDay >= 4) {
                MessageBox.Show("The professor cant teach more than 4 courses per day!");
                return false;
            }
            if (coursesPerWeek >= 20) {
                MessageBox.Show("The professor cant teach more than 20 courses per week!");
                return false;
            }
            return true;
        }
    }
}
