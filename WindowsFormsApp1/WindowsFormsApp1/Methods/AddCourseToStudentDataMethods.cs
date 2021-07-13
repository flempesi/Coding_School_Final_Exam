using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Impl;
using WindowsFormsApp1.Storages;

namespace WindowsFormsApp1.Methods {
    class AddCourseToStudentDataMethods {
        private AddCourseToStudentDGVMethods _AddCourseToStudentDGVMethods = new AddCourseToStudentDGVMethods();

        private Storage _Storage = new Storage();

        public void AddCourse(DataGridView dGVCoursesForStudents, DataGridView dGVStudents, DataGridView dGVScheduleStudents, University newUniversity, Action<object, DataGridViewCellEventArgs> DeleteButton_CellClick) {

            if (dGVCoursesForStudents.SelectedRows.Count == 1 && dGVStudents.SelectedRows.Count == 1) {
                DataGridViewRow rowCourse = dGVCoursesForStudents.SelectedRows[0];
                DataGridViewRow rowProfessor = dGVStudents.SelectedRows[0];
                Guid courseID = Guid.Parse(rowCourse.Cells["id"].Value.ToString());
                Guid studentID = Guid.Parse(rowProfessor.Cells["id"].Value.ToString());
                 
                Course course = newUniversity.Students.Find(x => x.Id == studentID).Courses.Find(x => x.Id == courseID);
                if (course == null) {//if dont have already this lesson

                    if (CheckIfSameStudentInSameDateTimeHasCourse(courseID, studentID, newUniversity)) {
                        newUniversity.Students.Find(x => x.Id == studentID).Courses.Add(newUniversity.Courses.Find(x => x.Id == courseID));

                        _AddCourseToStudentDGVMethods.RefreshDataGridScheduleStudents(dGVScheduleStudents, newUniversity, DeleteButton_CellClick);
                    }
                }
                else {
                    MessageBox.Show("The student have already this course!");
                }
            }
        }
        public void DeleteCourse(DataGridView dGVScheduleStudents,University newUniversity,bool hasDeletedRecords, Action<object, DataGridViewCellEventArgs> DeleteButton_CellClick) {
            
            if (dGVScheduleStudents.SelectedRows.Count == 1) {
                DataGridViewRow rowSchedule = dGVScheduleStudents.SelectedRows[0];
                Guid courseID = Guid.Parse(rowSchedule.Cells["CourseId"].Value.ToString());
                Guid studentID = Guid.Parse(rowSchedule.Cells["StudentId"].Value.ToString());

                Course course = newUniversity.Students.Find(x => x.Id == studentID).Courses.Find(x => x.Id == courseID);

                DialogResult result = MessageBox.Show("Are you sure to delete this record ? ", "Delete record", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) {
                    hasDeletedRecords = true;
                    newUniversity.Students.Find(x => x.Id == studentID).Courses.Remove(course);
                    _AddCourseToStudentDGVMethods.RefreshDataGridScheduleStudents(dGVScheduleStudents, newUniversity, DeleteButton_CellClick);
                    SaveChanges(newUniversity);
                }


            }
        }
        public void SaveButtonActions(DataGridView dGVScheduleStudents, bool hasDeletedRecords,Form form,University newUniversity) {

            if (dGVScheduleStudents.Rows.Count > 0) {
                SaveChanges(newUniversity);
                form.Close();

            }
            else if (hasDeletedRecords == true) {
                form.Close();
            }
            else {
                MessageBox.Show("Please Add a course to student to save it");
            }

        }

        private void SaveChanges(University newUniversity) {
            _Storage.NewUniversity = newUniversity;
            _Storage.SerializeToJson();
        }

        public bool CheckIfSameStudentInSameDateTimeHasCourse(Guid courseId, Guid studentId,University newUniversity) {
            //CANNOT ADD SAME STUDENT   IN SAME DATE & HOUR
            //cannot add same course in same date
            List<Schedule> sceduleListOfCourse = new List<Schedule>();
            List<Schedule> sceduleListOfStudent = new List<Schedule>();
            sceduleListOfCourse = newUniversity.ScheduleList.FindAll(x => x.CourseID == courseId);
            DateTime dateTime;
            List<Course> courses = new List<Course>();
            courses = newUniversity.Students.Find(x => x.Id == studentId).Courses;
            int coursePerDay = 0;
            foreach (var course in courses) {
                sceduleListOfStudent = newUniversity.ScheduleList.FindAll(x => x.CourseID == course.Id);
            }

            foreach (Schedule  scheduleCourse in sceduleListOfCourse) {
                dateTime = scheduleCourse.DateTimeSchedule;
                Course courseExist = courses.Find(x => x.Id == scheduleCourse.CourseID);

                foreach (Schedule scheduleStudent in sceduleListOfStudent) {
                    if (scheduleStudent.DateTimeSchedule.Date.ToString("yyyyMMdd") == dateTime.Date.ToString("yyyyMMdd")) {
                        int HoursExistingCourse = newUniversity.Courses.Find(x => x.Id == scheduleCourse.CourseID).Hours;
                        int HoursNewCourse = newUniversity.Courses.Find(x => x.Id == courseId).Hours;

                        if (courseExist != null) {
                            MessageBox.Show("You can not add the same course in the same student in same date");
                            return false;

                        }
                        if (coursePerDay > 3) {// EACH STUDENT CANNOT HAVE MORE THAN 3 COURSES PER DAY!
                            MessageBox.Show("A student cannt attend more than 3 courses a day");
                            return false;
                        }
                        coursePerDay++;
                        if (!CheckbyHour(dateTime, scheduleStudent, HoursExistingCourse, HoursNewCourse)) {
                            return false;
                        }
                    }

                }
            }
            return true;
        }
        private static bool CheckbyHour(DateTime dateTime, Schedule item, int HoursExistingCourse, int HoursNewCourse) {
            if (item.DateTimeSchedule.Hour == dateTime.Hour) {
                MessageBox.Show("You can not add the same student in the same time of the same date");
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
                        MessageBox.Show("During of the new scheduled course , student have already lesson!");
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
