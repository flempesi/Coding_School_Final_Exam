using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Impl;
using WindowsFormsApp1.Methods;
using WindowsFormsApp1.Storages;

namespace WindowsFormsApp1.WUI {
    public partial class CourseSchedulerForm : Form {

        public University NewUniversity = new University();
        private CourseSchedulerDGVMethods _CourseSchedulerDGVMethods = new CourseSchedulerDGVMethods();
        private CourseSchedulerDataMethods _CourseSchedulerDataMethods = new CourseSchedulerDataMethods();
        private bool _HasDeletedRecords = false;

        public CourseSchedulerForm() {
            InitializeComponent();
        }

        private void CourseSchedulerForm_Load(object sender, EventArgs e) {
            _CourseSchedulerDGVMethods.OnLoad(this,  ctrlTime,  dGVCourses,  dGVProfessors,  dGVSchedule,  NewUniversity, dataGridViewSchedules_DeleteButton_CellClick);
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            Close();
        }
        private void btnSave_Click(object sender, EventArgs e) {

            _CourseSchedulerDataMethods.SaveButtonActions(dGVSchedule, this, _HasDeletedRecords, NewUniversity);
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            _CourseSchedulerDataMethods.AddSchedule(dGVCourses, dGVProfessors, dGVSchedule, NewUniversity,  ctrlTime,  ctrlDate, dataGridViewSchedules_DeleteButton_CellClick);
        }
        private void dataGridViewSchedules_DeleteButton_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == dGVSchedule.Columns["Delete"].Index) {
                _CourseSchedulerDataMethods.DeleteSchedule(dGVSchedule, NewUniversity, _HasDeletedRecords, dataGridViewSchedules_DeleteButton_CellClick);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e) {
            _CourseSchedulerDGVMethods.RefreshData(dGVCourses, dGVProfessors, NewUniversity);
        }
    }
}
