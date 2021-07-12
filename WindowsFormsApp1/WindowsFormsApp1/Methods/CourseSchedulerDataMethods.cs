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

                DialogResult result = MessageBox.Show("Are you sure to delete this record ? ", "Delete record", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) {
                    hasDeletedRecords = true;
                    newUniversity.ScheduleList.Remove(schedule);
                    _CourseSchedulerDVGMethods.RefreshDataGridSchedule(dGVSchedule, newUniversity, dataGridViewSchedules_DeleteButton_CellClick);

                    SaveChanges(newUniversity);
                }

            }

        }
        public void SaveButtonActions(DataGridView dGVSchedule, Form form, bool hasDeletedRecords, University newUniversity) {

            if (dGVSchedule.Rows.Count > 1) {
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

        public void AddSchedule(DataGridView dGVCourses, DataGridView dGVProfessors, DataGridView dGVSchedule,University newUniversity, DateTimePicker ctrlTime, DateTimePicker ctrlDate, Action<object, DataGridViewCellEventArgs> dataGridViewSchedules_DeleteButton_CellClick) {

            if (dGVCourses.SelectedRows.Count == 1 && dGVProfessors.SelectedRows.Count == 1) {
                DataGridViewRow rowCourse = dGVCourses.SelectedRows[0];
                DataGridViewRow rowProfessor = dGVProfessors.SelectedRows[0];
                Guid courseID = Guid.Parse(rowCourse.Cells["Id"].Value.ToString());
                Guid professorID = Guid.Parse(rowProfessor.Cells["Id"].Value.ToString());
                DateTime dateTimeSchedule = GetDateTime( ctrlTime,  ctrlDate);

                if (CheckIfSameProfessorInSameDateTimeHasCourse(professorID, dateTimeSchedule, courseID,newUniversity) &&
                    CheckMaxCoursesForProfessor(professorID, dateTimeSchedule, newUniversity)) {
                    newUniversity.ScheduleList.Add(new Schedule(professorID, courseID, dateTimeSchedule));
                    _CourseSchedulerDVGMethods.RefreshDataGridSchedule(dGVSchedule, newUniversity, dataGridViewSchedules_DeleteButton_CellClick);
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
                    if (item.DateTimeSchedule.Hour == dateTime.Hour) {
                        MessageBox.Show("You can not add the same professor in the same time of the same date");
                        return false;
                    }
                    if (item.CourseID == courseid) {
                        MessageBox.Show("You can not add the same course in the same professor in same date");
                        return false;

                    }
                }

            }
            return true;

        }

        public bool CheckMaxCoursesForProfessor(Guid professorId, DateTime dateTime,University newUniversity) {
            // A PROFESSOR CANNOT TEACH MORE THAN 4 COURSES PER DAY AND  40(20 right) COURSES PER WEEK
            List<Schedule> proffesorSceduleList = new List<Schedule>();
            proffesorSceduleList = newUniversity.ScheduleList.FindAll(x => x.ProfessorID == professorId);
            int coursesPerDay = 0;
            int coursesPerWeek = 0;
            CultureInfo culture = CultureInfo.CurrentCulture;

            foreach (var item in proffesorSceduleList) {

                if (item.DateTimeSchedule.Date.ToString("yyyyMMdd") == dateTime.Date.ToString("yyyyMMdd")) {
                    coursesPerDay++;
                }
                //if (item.DateTimeSchedule.WeekOfYear == dateTime.Date.ToString("MM")) {
                //    coursesPerMonth++;
                //}


                //var firstDayWeek = cul.Calendar.GetWeekOfYear(item.DateTimeSchedule, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                int weekNumberOfesheduledCourse = culture.Calendar.GetWeekOfYear(item.DateTimeSchedule, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

                int weekNumberOfCourse = culture.Calendar.GetWeekOfYear(dateTime.Date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                if (weekNumberOfesheduledCourse == weekNumberOfCourse) {
                    coursesPerWeek++;

                }
                if (coursesPerDay > 4) {
                    MessageBox.Show("The professor cant teach more than 4 courses per day!");
                    return false;
                }
                else if (coursesPerWeek > 20) {
                    MessageBox.Show("The professor cant teach more than 20 courses per week!");
                }
            }
            return true;
        }
    }
}
