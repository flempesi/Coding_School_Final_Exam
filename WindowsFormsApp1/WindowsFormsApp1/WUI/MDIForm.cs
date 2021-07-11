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
using WindowsFormsApp1.Storages;

namespace WindowsFormsApp1.WUI {
    public partial class MDIForm : Form {

        private University NewUniversity = new University();
        private Storage _Storage = new Storage();
        public MDIForm() {
            InitializeComponent();
        }
        private void MDIForm_Load(object sender, EventArgs e) {
            OnLoad();
        }
        private void courseSchedulerToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenCourseschedulerForm();

        }
        private void addCourseToStudentToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenAddCourseToStudentForm();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }
        private void OnLoad() {
            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.LightGray;
            //Initialize();
            _Storage.DeserializeFromJson();
            NewUniversity = _Storage.NewUniversity;
        }
        private void OpenCourseschedulerForm() {
            CourseSchedulerForm courseSchedulerForm = new CourseSchedulerForm();
            courseSchedulerForm.MdiParent = this;
            courseSchedulerForm.NewUniversity = NewUniversity;
            courseSchedulerForm.Show();
        }
        private void OpenAddCourseToStudentForm() {
            AddCourseToStudentForm addCourseToStudentForm = new AddCourseToStudentForm();
            addCourseToStudentForm.MdiParent = this;
            addCourseToStudentForm.NewUniversity = NewUniversity;
            addCourseToStudentForm.Show();
        }
        private void Initialize() {
            NewUniversity.InsertDataToUniversity();
            _Storage.NewUniversity = NewUniversity;
            _Storage.SerializeToJson();
        }
    }
}
