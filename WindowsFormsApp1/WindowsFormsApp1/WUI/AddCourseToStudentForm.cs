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

        private List<Student> _StudentsList = new List<Student>();

        private Storage _Storage = new Storage();
        private bool HasDeletedRecords;
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
        private void OnLoad() {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            LoadGridViewCourses();
            LoadGridViewStudents();

            _StudentsList = NewUniversity.Students;
            RefreshDataGridScheduleStudents();
        }
        public void LoadGridViewCourses() {
            SetDataGridViewProperties(dGVCoursesForStudents);
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
            dataGridView.MultiSelect = false;
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

                Course course = _StudentsList.Find(x => x.Id == studentID).Courses.Find(x => x.Id == courseID);
                if (course == null) {//if dont have already this lesson
                    if (CheckIfSameStudentInSameDateTimeHasCourse(courseID, studentID)) {
                        _StudentsList.Find(x => x.Id == studentID).Courses.Add(NewUniversity.Courses.Find(x => x.Id == courseID));

                        RefreshDataGridScheduleStudents();
                    }
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
            dGVScheduleStudents.ColumnCount = 9;
           
            dGVScheduleStudents.Columns[0].Name = "Delete";
            DataGridViewButtonColumn DeleteButton = new DataGridViewButtonColumn();
            DeleteButton.Name = "Delete";
            DeleteButton.Text = "Delete";
            DeleteButton.UseColumnTextForButtonValue = true;
            if (dGVScheduleStudents.Columns["Delete"] == null) {
                dGVScheduleStudents.Columns.Insert(0, DeleteButton);
                dGVScheduleStudents.CellClick += new DataGridViewCellEventHandler(dGVScheduleStudents_DeleteButton_CellClick);
            }


            dGVScheduleStudents.Columns[1].Name = "Subject";
            dGVScheduleStudents.Columns[2].Name = "Professor Name";
            dGVScheduleStudents.Columns[3].Name = "Professor Surname";
            dGVScheduleStudents.Columns[4].Name = "Date";
            dGVScheduleStudents.Columns[5].Name = "Student Name";
            dGVScheduleStudents.Columns[6].Name = "Student Surname";
            dGVScheduleStudents.Columns[7].Name = "StudentId";
            dGVScheduleStudents.Columns[7].Visible = false;
            dGVScheduleStudents.Columns[8].Name = "CourseId";
            dGVScheduleStudents.Columns[8].Visible = false;
        }
        private void dGVScheduleStudents_DeleteButton_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == dGVScheduleStudents.Columns["Delete"].Index) {
                DeleteCourse();
            }
        }

        public void SetRowsDataGridView() {
            
            //List<string[]> rows = new List<string[]>();
            string[] row = new string[9];

            foreach (var student in _StudentsList) {
                string studentId = student.Id.ToString();
                string studentName = NewUniversity.Students.Find(x => x.Id == student.Id).Name;
                string studentSurname = NewUniversity.Students.Find(x => x.Id == student.Id).Surname;


                foreach (var course in student.Courses) {
                    string courseId = course.Id.ToString();
                    string courseSubject = NewUniversity.Courses.Find(x => x.Id == course.Id).Subject;

                    Schedule schedule = NewUniversity.ScheduleList.Find(x => x.CourseID == course.Id);
                    if (schedule != null) {
                        Guid professorId = NewUniversity.ScheduleList.Find(x => x.CourseID == course.Id).ProfessorID;
                        string name = NewUniversity.Professors.Find(x => x.Id == professorId).Name;
                        string surname = NewUniversity.Professors.Find(x => x.Id == professorId).Surname;
                        string datetime = schedule.DateTimeSchedule.ToString();

                        row=(new string[] {"",courseSubject, name, surname, datetime, studentName, studentSurname , studentId, courseId });
                        dGVScheduleStudents.Rows.Add(row);
                    }
                    else {
                        MessageBox.Show("The course is not scheduled.Please select other");
                    }
                }

            }
        }

        public void DeleteCourse() {
            //
            if (dGVScheduleStudents.SelectedRows.Count == 1) {
                DataGridViewRow rowSchedule = dGVScheduleStudents.SelectedRows[0];
                Guid courseID = Guid.Parse(rowSchedule.Cells["CourseId"].Value.ToString());
                Guid studentID = Guid.Parse(rowSchedule.Cells["StudentId"].Value.ToString());

                Course course = _StudentsList.Find(x => x.Id == studentID).Courses.Find(x => x.Id == courseID);

                DialogResult result = MessageBox.Show("Are you sure to delete this record ? ", "Delete record", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) {
                    HasDeletedRecords = true;
                    _StudentsList.Find(x => x.Id == studentID).Courses.Remove(course);
                    RefreshDataGridScheduleStudents();
                    SaveChanges();
                }


            }
        }
        public void SaveButtonActions() {

            if (dGVScheduleStudents.Rows.Count > 0) {
                SaveChanges();
                Close();

            }
            else if (HasDeletedRecords == true) {
                Close();
            }
            else {
                MessageBox.Show("Please Add a course to student to save it");
            }

        }

        private void SaveChanges() {
            _Storage.NewUniversity = NewUniversity;
            _Storage.SerializeToJson();
        }

        public bool CheckIfSameStudentInSameDateTimeHasCourse(Guid courseId, Guid studentId) {
            //    CANNOT ADD SAME STUDENT   IN SAME DATE & HOUR

            //cannot add same course in same date
            List<Schedule> sceduleListOfCourse = new List<Schedule>();
            List<Schedule> sceduleListOfStudent = new List<Schedule>();
            sceduleListOfCourse = NewUniversity.ScheduleList.FindAll(x => x.CourseID == courseId);
            DateTime dateTime;
            List<Course> courses = new List<Course>();
            courses = NewUniversity.Students.Find(x => x.Id == studentId).Courses;
            int coursePerDay = 0;
            foreach (var course in courses) {
                sceduleListOfStudent = NewUniversity.ScheduleList.FindAll(x => x.CourseID == course.Id);
            }

            foreach (var scheduleCourse in sceduleListOfCourse) {
                dateTime = scheduleCourse.DateTimeSchedule;

                foreach (var scheduleStudent in sceduleListOfStudent) {
                    if (scheduleStudent.DateTimeSchedule.Date.ToString("yyyyMMdd") == dateTime.Date.ToString("yyyyMMdd")) {
                        if (scheduleStudent.DateTimeSchedule.Hour == dateTime.Hour) {
                            return false;
                        }
                        if (coursePerDay >= 3) {// EACH STUDENT CANNOT HAVE MORE THAN 3 COURSES PER DAY!
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
