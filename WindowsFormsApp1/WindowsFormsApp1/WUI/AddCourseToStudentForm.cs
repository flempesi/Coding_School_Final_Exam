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
    public partial class AddCourseToStudentForm : Form {
        public University NewUniversity = new University();
        private List<Schedule> _ScheduleList { get; set; }

        private Storage _Storage = new Storage();
        public AddCourseToStudentForm() {
            InitializeComponent();
        }

        private void AddCourseToStudentForm_Load(object sender, EventArgs e) {
            OnLoad();
        }
        private void btnCancel_Click(object sender, EventArgs e) {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            //SaveButtonActions();
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            AddCourse();
        }
        private void btnDelete_Click(object sender, EventArgs e) {
           // DeleteSchedule();
        }
        private void OnLoad() {
           
            LoadGridViewCourses();
            LoadGridViewStudents();
            _ScheduleList = new List<Schedule>();
            _ScheduleList = NewUniversity.ScheduleList;
        }
        public void LoadGridViewCourses() {
            dataGridViewCourses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCourses.DataSource = NewUniversity.Courses;
            dataGridViewCourses.Columns["Id"].Visible = false;
        }
        public void LoadGridViewStudents() {
            dataGridViewStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewStudents.DataSource = NewUniversity.Students;
            dataGridViewStudents.Columns["Id"].Visible = false;
            dataGridViewStudents.Columns["Age"].Visible = false;
            

        }
        public void AddCourse() {

        }
        public void CheckIfSameStudentInSameDateTimeHasCourse() {
            // TODO:   CANNOT ADD SAME STUDENT   IN SAME DATE & HOUR

        }
        public void CheckMaxCoursesPerStudentIsLessThan4PerDay() {
            // TODO:   EACH STUDENT CANNOT HAVE MORE THAN 3 COURSES PER DAY!

        }



    }
}
