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
        private List<Student> _StudentsList { get; set; }

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
            SaveButtonActions();
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            AddCourse();
        }
        private void btnDelete_Click(object sender, EventArgs e) {
           DeleteCourse();
        }
        private void OnLoad() {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            LoadGridViewCourses();
            LoadGridViewStudents();
            
            _StudentsList = new List<Student>();
            _StudentsList = NewUniversity.Students;
            RefreshDataGridScheduleStudents();
        }
        public void LoadGridViewCourses() {
            SetDataGridViewProperties(dGVCoursesForStudents);
            dGVCoursesForStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dGVCoursesForStudents.DataSource = NewUniversity.Courses;
            dGVCoursesForStudents.Columns["Id"].Visible = false;
        }
        public void LoadGridViewStudents() {
            SetDataGridViewProperties(dGVStudents);
            dGVStudents.DataSource = NewUniversity.Students;
            dGVStudents.Columns["Id"].Visible = false;
            dGVStudents.Columns["Age"].Visible = false;
            
        }

        public void SetDataGridViewProperties(DataGridView dataGridView) {
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        public void AddCourse() {

            if (dGVCoursesForStudents.SelectedRows.Count == 1 && dGVStudents.SelectedRows.Count == 1) {
                DataGridViewRow rowCourse = dGVCoursesForStudents.SelectedRows[0];
                DataGridViewRow rowProfessor = dGVStudents.SelectedRows[0];
                Guid courseID = Guid.Parse(rowCourse.Cells["id"].Value.ToString());
                Guid studentID = Guid.Parse(rowProfessor.Cells["id"].Value.ToString());
                //DateTime dateTimeSchedule = GetDateTime();
                //Student student= _StudentsList.Find(x => x.Id == studentID);
               // Course[] courses;
                Course course = _StudentsList.Find(x => x.Id == studentID).Courses.Find(x => x.Id == courseID);
                if (course == null) {//if have already this lesson
                    //if (CheckIfSameProfessorInSameDateTimeHasCourse(professorID, dateTimeSchedule, courseID) &&
                    //    CheckMaxCoursesForProfessor(professorID, dateTimeSchedule)) {
                    _StudentsList.Find(x => x.Id == studentID).Courses.Add(NewUniversity.Courses.Find(x => x.Id == courseID));
                    
                    RefreshDataGridScheduleStudents();
                }
            }
        }

        public void RefreshDataGridScheduleStudents() {
            SetDataGridViewProperties(dGVScheduleStudents);
            dGVScheduleStudents.Rows.Clear();
            MakeColumnsDataGridView();
            SetRowsDataGridView();
        }
        public void MakeColumnsDataGridView() {
            dGVScheduleStudents.ColumnCount = 8;
            dGVScheduleStudents.ColumnHeadersVisible = true;

            dGVScheduleStudents.Columns[0].Name = "StudentId";
            dGVScheduleStudents.Columns[1].Name = "CourseId";
            dGVScheduleStudents.Columns[2].Name = "Subject";
            dGVScheduleStudents.Columns[3].Name = "Professor Name";
            dGVScheduleStudents.Columns[4].Name = "Professor Surname";
            dGVScheduleStudents.Columns[5].Name = "Date";
            dGVScheduleStudents.Columns[6].Name = "Student Name";
            dGVScheduleStudents.Columns[7].Name = "Student Surname";

            dGVScheduleStudents.Columns["StudentId"].Visible = false;
            dGVScheduleStudents.Columns["CourseId"].Visible = false;
        }
        public void SetRowsDataGridView() {

            List<string[]> rows = new List<string[]>();
            foreach (var student in _StudentsList) {
                string studentId = student.Id.ToString();
                string studentName = NewUniversity.Students.Find(x => x.Id == student.Id).Name;
                string studentSurname = NewUniversity.Students.Find(x => x.Id == student.Id).Surname;
                
                
                foreach (var course in student.Courses) {
                    string courseId = course.Id.ToString();
                    string courseSubject = NewUniversity.Courses.Find(x => x.Id == course.Id).Subject;

                    //Guid scheduleListRowId = NewUniversity.ScheduleList.Find(x => x.CourseID == course.Id).Id;
                    Schedule schedule = NewUniversity.ScheduleList.Find(x => x.CourseID == course.Id);
                    Guid professorId= NewUniversity.ScheduleList.Find(x => x.CourseID == course.Id).ProfessorID;
                    string name = NewUniversity.Professors.Find(x => x.Id == professorId).Name;
                    string surname = NewUniversity.Professors.Find(x => x.Id == professorId).Surname;
                    string datetime = schedule.DateTimeSchedule.ToString();

                    rows.Add(new string[] { studentId,courseId, courseSubject, name, surname, datetime, studentName, studentSurname });
                }
                
            }

            foreach (string[] rowArray in rows) {
                dGVScheduleStudents.Rows.Add(rowArray);
            }
        }

        public void DeleteCourse() {
            //
            if (dGVScheduleStudents.SelectedRows.Count == 1 &&
               dGVScheduleStudents.CurrentRow.Index != dGVScheduleStudents.Rows.Count - 1)//to not select the empty row
                    {
                DataGridViewRow rowSchedule = dGVScheduleStudents.SelectedRows[0];
                Guid courseID = Guid.Parse(rowSchedule.Cells["CourseId"].Value.ToString());
                Guid studentID = Guid.Parse(rowSchedule.Cells["StudentId"].Value.ToString());

                Course course = _StudentsList.Find(x => x.Id == studentID).Courses.Find(x => x.Id == courseID);
                //Schedule schedule = _ScheduleList.Find(x => x.Id == scheduleID);

                _StudentsList.Find(x => x.Id == studentID).Courses.Remove(course);
                RefreshDataGridScheduleStudents();
            }
        }
        public void SaveButtonActions() {

            if (dGVScheduleStudents.Rows.Count > 1) {
                NewUniversity.Students = _StudentsList;
                _Storage.NewUniversity = NewUniversity;
                _Storage.SerializeToJson();
                _Storage.DeserializeFromJson();
                NewUniversity = _Storage.NewUniversity;
                Close();
            }
            else {
                MessageBox.Show("Please Add a course to student to save it");
            }



        }
        public void CheckIfSameStudentInSameDateTimeHasCourse() {
            // TODO:   CANNOT ADD SAME STUDENT   IN SAME DATE & HOUR

        }
        public void CheckMaxCoursesPerStudentIsLessThan4PerDay() {
            // TODO:   EACH STUDENT CANNOT HAVE MORE THAN 3 COURSES PER DAY!

        }
    }
}
