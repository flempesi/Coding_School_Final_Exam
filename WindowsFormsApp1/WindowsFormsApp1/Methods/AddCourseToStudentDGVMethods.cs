using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Impl;

namespace WindowsFormsApp1.Methods {
    class AddCourseToStudentDGVMethods {
        private CommonDGVMethods _CommonDGVMethods = new CommonDGVMethods();
        public void OnLoad(Form form, DataGridView dGVCoursesForStudents, DataGridView dGVStudents, DataGridView dGVScheduleStudents,University newUniversity, Action<object, DataGridViewCellEventArgs> DeleteButton_CellClick) {
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MaximumSize = form.Size;
            form.MinimumSize = form.Size;
            LoadGridViewCourses(dGVCoursesForStudents, newUniversity);
            LoadGridViewStudents(dGVStudents, newUniversity);

            RefreshDataGridScheduleStudents( dGVScheduleStudents, newUniversity , DeleteButton_CellClick);
            
        }
        public void LoadGridViewCourses(DataGridView dGVCoursesForStudents, University newUniversity) {
            _CommonDGVMethods.SetDataGridViewProperties(dGVCoursesForStudents);
            List<string> ColumnsNames = new List<string> { "Id", "Code", "Subject", "Hours", "Category" };
            List<string> HideColumnsNames = new List<string> { "Id" };

            _CommonDGVMethods.MakeColumnsDataGridView(dGVCoursesForStudents, false, 5, ColumnsNames, HideColumnsNames,null);
            SetRowsDataGridViewCourses(newUniversity.Courses, dGVCoursesForStudents, newUniversity);

        }
        public void LoadGridViewStudents(DataGridView dGVStudents, University newUniversity) {
            _CommonDGVMethods.SetDataGridViewProperties(dGVStudents);

            List<string> ColumnsNames = new List<string> { "Id", "Name", "Surname", "RegistrationNumber" };
            List<string> HideColumnsNames = new List<string> { "Id" };

            _CommonDGVMethods.MakeColumnsDataGridView(dGVStudents, false, 4, ColumnsNames, HideColumnsNames,null);
            SetRowsDataGridViewStudents(newUniversity.Students, dGVStudents);


        }

        //public void SetDataGridViewProperties(DataGridView dataGridView) {
        //    dataGridView.MultiSelect = false;
        //    dataGridView.AllowUserToAddRows = false;
        //    dataGridView.AllowUserToResizeRows = false;
        //    dataGridView.RowHeadersVisible = false;
        //    dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
        //    dataGridView.EnableHeadersVisualStyles = false;
        //    dataGridView.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;
        //    dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
        //    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //    dataGridView.ReadOnly = true;
        //    dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //}

        public void RefreshDataGridScheduleStudents(DataGridView dGVScheduleStudents, University newUniversity, Action<object, DataGridViewCellEventArgs> DeleteButton_CellClick) {
            _CommonDGVMethods.SetDataGridViewProperties(dGVScheduleStudents);
            dGVScheduleStudents.Rows.Clear();
            List<string> ColumnsNames = new List<string> { "Subject", "Professor Name", "Professor Surname", "Date", "Student Name", "Student Surname", "StudentId", "CourseId" };
            List<string> HideColumnsNames = new List<string> { "StudentId", "CourseId" };

            _CommonDGVMethods.MakeColumnsDataGridView(dGVScheduleStudents, true, 9, ColumnsNames, HideColumnsNames, DeleteButton_CellClick);
            //dGVScheduleStudents_DeleteButton_CellClick


            SetRowsDataGridView(newUniversity, dGVScheduleStudents);
        }
        public void SetRowsDataGridViewStudents(List<Student> Students, DataGridView dataGridView) {
            string[] row;

            foreach (var item in Students) {
                row = (new string[] { item.Id.ToString(), item.Name, item.Surname, item.RegistrationNumber });
                dataGridView.Rows.Add(row);
            }
        }
        public void SetRowsDataGridView(University newUniversity, DataGridView dGVScheduleStudents) {
            string[] row = new string[9];

            foreach (var student in newUniversity.Students) {
                string studentId = student.Id.ToString();
                string studentName = newUniversity.Students.Find(x => x.Id == student.Id).Name;
                string studentSurname = newUniversity.Students.Find(x => x.Id == student.Id).Surname;


                foreach (var course in student.Courses) {
                    string courseId = course.Id.ToString();
                    string courseSubject = newUniversity.Courses.Find(x => x.Id == course.Id).Subject;

                    Schedule schedule = newUniversity.ScheduleList.Find(x => x.CourseID == course.Id);
                    if (schedule != null) {
                        Guid professorId = newUniversity.ScheduleList.Find(x => x.CourseID == course.Id).ProfessorID;
                        string name = newUniversity.Professors.Find(x => x.Id == professorId).Name;
                        string surname = newUniversity.Professors.Find(x => x.Id == professorId).Surname;
                        string datetime = schedule.DateTimeSchedule.ToString();

                        row = (new string[] { "", courseSubject, name, surname, datetime, studentName, studentSurname, studentId, courseId });
                        dGVScheduleStudents.Rows.Add(row);
                    }

                }

            }
        }

        public void SetRowsDataGridViewCourses(List<Course> Courses, DataGridView dataGridView, University newUniversity) {
            string[] row;

            foreach (var course in Courses) {
                Schedule courseScheduled = newUniversity.ScheduleList.FirstOrDefault(x => x.CourseID == course.Id);
                if (courseScheduled != null) {
                    row = (new string[] { course.Id.ToString(), course.Code, course.Subject, course.Hours.ToString(), course.Category.ToString() });
                    dataGridView.Rows.Add(row);
                }

            }
        }

        //public void MakeColumnsDataGridView(DataGridView dataGridView, bool hasDeleteButton, int columnsCount, List<string> columnsNames, List<string> hideColumnsNames,) {
        //    dataGridView.ColumnCount = columnsCount;
        //    int i = 0;
        //    if (hasDeleteButton == true) {
        //        DataGridViewButtonColumn DeleteButton = new DataGridViewButtonColumn();
        //        DeleteButton.Name = "Delete";
        //        DeleteButton.Text = "Delete";
        //        DeleteButton.UseColumnTextForButtonValue = true;
        //        if (dataGridView.Columns["Delete"] == null) {
        //            dataGridView.Columns.Insert(0, DeleteButton);
        //            dataGridView.CellClick += new DataGridViewCellEventHandler(dGVScheduleStudents_DeleteButton_CellClick);
        //        }
        //        i++;
        //    }
        //    foreach (string name in columnsNames) {
        //        dataGridView.Columns[i].Name = name;
        //        i++;
        //    }
        //    foreach (string name in hideColumnsNames) {
        //        dataGridView.Columns[name].Visible = false;
        //    }

        //    if (dataGridView.Columns.Contains("") == true) {
        //        dataGridView.Columns.Remove("");
        //    }
        //}
    }
}
