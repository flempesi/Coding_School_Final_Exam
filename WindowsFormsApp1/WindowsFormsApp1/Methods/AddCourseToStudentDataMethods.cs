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
            }
        }
        public void DeleteCourse(DataGridView dGVScheduleStudents,University newUniversity,bool hasDeletedRecords, Action<object, DataGridViewCellEventArgs> DeleteButton_CellClick) {
            //
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
            //    CANNOT ADD SAME STUDENT   IN SAME DATE & HOUR

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

            foreach (var scheduleCourse in sceduleListOfCourse) {
                dateTime = scheduleCourse.DateTimeSchedule;
                Course courseExist = courses.Find(x => x.Id == scheduleCourse.CourseID);

                foreach (var scheduleStudent in sceduleListOfStudent) {
                    if (scheduleStudent.DateTimeSchedule.Date.ToString("yyyyMMdd") == dateTime.Date.ToString("yyyyMMdd")) {
                        if (scheduleStudent.DateTimeSchedule.Hour == dateTime.Hour) {
                            MessageBox.Show("You can not add in the same student in the same time of the same date  more than one courses");
                            return false;
                        }
                        if (courseExist != null) {
                            MessageBox.Show("You can not add the same course in the same student in same date");
                            return false;

                        }
                        if (coursePerDay > 3) {// EACH STUDENT CANNOT HAVE MORE THAN 3 COURSES PER DAY!
                            MessageBox.Show("A student cannt attend more than 3 courses a day");
                            return false;
                        }
                        coursePerDay++;
                    }

                }
            }
            return true;
        }
    }
}
