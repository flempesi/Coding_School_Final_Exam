using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Impl;
using WindowsFormsApp1.Methods;
using WindowsFormsApp1.Storages;

namespace WindowsFormsApp1.WUI {
    public partial class AddCourseToStudentForm : Form {
        public University NewUniversity = new University();

        private AddCourseToStudentDGVMethods _AddCourseToStudentDGVMethods = new AddCourseToStudentDGVMethods();
        private AddCourseToStudentDataMethods _AddCourseToStudentDataMethods = new AddCourseToStudentDataMethods();

        private bool HasDeletedRecords = false;
        public AddCourseToStudentForm() {
            InitializeComponent();
        }

        private void AddCourseToStudentForm_Load(object sender, EventArgs e) {
            _AddCourseToStudentDGVMethods.OnLoad( this,  dGVCoursesForStudents,  dGVStudents,  dGVScheduleStudents,  NewUniversity, dGVScheduleStudents_DeleteButton_CellClick);
        }
        private void btnCancel_Click(object sender, EventArgs e) {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            _AddCourseToStudentDataMethods.SaveButtonActions( dGVScheduleStudents,  HasDeletedRecords,  this,  NewUniversity);
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            _AddCourseToStudentDataMethods.AddCourse( dGVCoursesForStudents,  dGVStudents, dGVScheduleStudents,   NewUniversity, dGVScheduleStudents_DeleteButton_CellClick);

        }

        private void dGVScheduleStudents_DeleteButton_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == dGVScheduleStudents.Columns["Delete"].Index) {
                _AddCourseToStudentDataMethods.DeleteCourse( dGVScheduleStudents,  NewUniversity,  HasDeletedRecords, dGVScheduleStudents_DeleteButton_CellClick);

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e) {
            _AddCourseToStudentDGVMethods.RefreshData(dGVStudents, dGVCoursesForStudents, dGVScheduleStudents, NewUniversity, dGVScheduleStudents_DeleteButton_CellClick);

        }
    }
}
