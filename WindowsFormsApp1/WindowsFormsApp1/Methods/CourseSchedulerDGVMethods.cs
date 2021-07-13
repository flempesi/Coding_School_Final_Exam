using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Impl;
using WindowsFormsApp1.Storages;

namespace WindowsFormsApp1.Methods {
    class CourseSchedulerDGVMethods {

        private CommonDGVMethods _CommonDGVMethods = new CommonDGVMethods();


        #region LoadMethods

        public void OnLoad(Form form, DateTimePicker ctrlTime, DataGridView dGVCourses, DataGridView dGVProfessors, DataGridView dGVSchedule, University newUniversity, Action<object, DataGridViewCellEventArgs> dataGridViewSchedules_DeleteButton_CellClick)  {

            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MaximumSize = form.Size;
            form.MinimumSize = form.Size;

            DateTime dateTime = DateTime.Now;
            TimeSpan timeSpan = new TimeSpan(08, 00, 00);
            dateTime = dateTime.Date + timeSpan;
            ctrlTime.Value = dateTime;

            ctrlTime.ShowUpDown = true;
            LoadGridViewCourses(dGVCourses, newUniversity);
            LoadGridViewProfessors(dGVProfessors, newUniversity);


            RefreshDataGridSchedule(dGVSchedule, newUniversity, dataGridViewSchedules_DeleteButton_CellClick);
        }
        public void LoadGridViewProfessors(DataGridView dGVProfessors, University NewUniversity) {
            _CommonDGVMethods.SetDataGridViewProperties(dGVProfessors);

            List<string> ColumnsNames = new List<string> { "Id", "Name", "Surname" };
            List<string> HideColumnsNames = new List<string> { "Id" };

            _CommonDGVMethods.MakeColumnsDataGridView(dGVProfessors, false, 3, ColumnsNames, HideColumnsNames, null);
            SetRowsDataGridViewProfessors(NewUniversity.Professors, dGVProfessors);
        }
        public void LoadGridViewCourses(DataGridView dGVCourses, University NewUniversity) {
            _CommonDGVMethods.SetDataGridViewProperties(dGVCourses);

            List<string> ColumnsNames = new List<string> { "Id", "Code", "Subject", "Hours", "Category" };
            List<string> HideColumnsNames = new List<string> { "Id" };
            _CommonDGVMethods.MakeColumnsDataGridView(dGVCourses, false, 5, ColumnsNames, HideColumnsNames, null);
            SetRowsDataGridViewCourses(NewUniversity.Courses, dGVCourses);

        }
        #endregion


        #region SetRowsDGVMethods
        public void SetRowsDataGridViewCourses(List<Course> courses, DataGridView dataGridView) {
            string[] row;

            foreach (var item in courses) {
                row = (new string[] { item.Id.ToString(), item.Code, item.Subject, item.Hours.ToString(), item.Category.ToString() });
                dataGridView.Rows.Add(row);
            }
        }

        public void SetRowsDataGridViewProfessors(List<Professor> professors, DataGridView dataGridView) {
            string[] row;

            foreach (var item in professors) {
                row = (new string[] { item.Id.ToString(), item.Name, item.Surname });
                dataGridView.Rows.Add(row);
            }
        }
        public void SetRowsDataGridView(List<Schedule> scheduleList, University newUniversity, DataGridView dGVSchedule) {

            string[] row = new string[6];
            foreach (var item in scheduleList) {
                string ScheduleId = item.Id.ToString();
                if (ScheduleId != null) {
                    string name = newUniversity.Professors.Find(x => x.Id == item.ProfessorID).Name;
                    string surname = newUniversity.Professors.Find(x => x.Id == item.ProfessorID).Surname;
                    string CourseSubject = newUniversity.Courses.Find(x => x.Id == item.CourseID).Subject;
                    string datetime = item.DateTimeSchedule.ToString();
                    row = (new string[] { "", ScheduleId, CourseSubject, name, surname, datetime });
                    dGVSchedule.Rows.Add(row);
                }
            }

        }
        #endregion

        #region refreshMethods
        public void RefreshData(DataGridView dGVCourses, DataGridView dGVProfessors, University newUniversity) {
            dGVProfessors.Rows.Clear();
            LoadGridViewProfessors( dGVProfessors,  newUniversity);
            dGVCourses.Rows.Clear();
            LoadGridViewCourses( dGVCourses,  newUniversity);
        }
        public void RefreshDataGridSchedule(DataGridView dGVSchedule, University newUniversity, Action<object, DataGridViewCellEventArgs> dataGridViewSchedules_DeleteButton_CellClick) {
            _CommonDGVMethods.SetDataGridViewProperties(dGVSchedule);
            dGVSchedule.Rows.Clear();

            List<string> ColumnsNames = new List<string> { "Id", "Subject", "Professor Name", "Professor Surname", "Date" };
            List<string> HideColumnsNames = new List<string> { "Id" };
            _CommonDGVMethods.MakeColumnsDataGridView(dGVSchedule, true, 6, ColumnsNames, HideColumnsNames, dataGridViewSchedules_DeleteButton_CellClick);
            SetRowsDataGridView(newUniversity.ScheduleList, newUniversity, dGVSchedule);
        }

        #endregion


    }  
}
