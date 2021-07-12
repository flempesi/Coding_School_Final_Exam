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
            List<string> ColumnsNames = new List<string> { "Id", "Code", "Subject", "Hours","Category" };
            List<string> HideColumnsNames = new List<string> { "Id" };

            MakeColumnsDataGridView(dGVCoursesForStudents, false, 5, ColumnsNames, HideColumnsNames);
            SetRowsDataGridViewCourses(NewUniversity.Courses, dGVCoursesForStudents);

        }
        public void LoadGridViewStudents() {
            SetDataGridViewProperties(dGVStudents);

            List<string> ColumnsNames = new List<string> { "Id", "Name", "Surname", "RegistrationNumber" };
            List<string> HideColumnsNames = new List<string> { "Id" };

            MakeColumnsDataGridView(dGVStudents, false, 4, ColumnsNames, HideColumnsNames);
            SetRowsDataGridViewStudents(NewUniversity.Students, dGVStudents);


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
            List<string> ColumnsNames = new List<string> { "Subject", "Professor Name", "Professor Surname", "Date", "Student Name", "Student Surname", "StudentId", "CourseId" };
            List<string> HideColumnsNames = new List<string> { "StudentId", "CourseId" };

            MakeColumnsDataGridView(dGVScheduleStudents, true, 9, ColumnsNames, HideColumnsNames);
            SetRowsDataGridView();
        }
        
        public void SetRowsDataGridViewCourses(List<Course> Courses, DataGridView dataGridView) {
            string[] row;

            foreach (var course in Courses) {
                Schedule courseScheduled = NewUniversity.ScheduleList.FirstOrDefault(x => x.CourseID == course.Id);
                if (courseScheduled != null) {
                    row = (new string[] { course.Id.ToString(), course.Code, course.Subject, course.Hours.ToString(), course.Category.ToString() });
                    dataGridView.Rows.Add(row);
                }
               
            }
        }

        public void MakeColumnsDataGridView(DataGridView dataGridView, bool hasDeleteButton, int columnsCount, List<string> columnsNames, List<string> hideColumnsNames) {
            dataGridView.ColumnCount = columnsCount;
            int i = 0;
            if (hasDeleteButton == true) {
                DataGridViewButtonColumn DeleteButton = new DataGridViewButtonColumn();
                DeleteButton.Name = "Delete";
                DeleteButton.Text = "Delete";
                DeleteButton.UseColumnTextForButtonValue = true;
                if (dataGridView.Columns["Delete"] == null) {
                    dataGridView.Columns.Insert(0, DeleteButton);
                    dataGridView.CellClick += new DataGridViewCellEventHandler(dGVScheduleStudents_DeleteButton_CellClick);
                }
                i++;
            }
            foreach (string name in columnsNames) {
                dataGridView.Columns[i].Name = name;
                i++;
            }
            foreach (string name in hideColumnsNames) {
                dataGridView.Columns[name].Visible = false;
            }

            if (dataGridView.Columns.Contains("") == true) {
                dataGridView.Columns.Remove("");
            }
        }
        private void dGVScheduleStudents_DeleteButton_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == dGVScheduleStudents.Columns["Delete"].Index) {
                DeleteCourse();
            }
        }

        public void  SetRowsDataGridViewStudents(List<Student> Students, DataGridView dataGridView) {
           string[] row;

            foreach (var item in Students) {
                row = (new string[] { item.Id.ToString(), item.Name, item.Surname, item.RegistrationNumber });
                dataGridView.Rows.Add(row);
            }
        }
        public void SetRowsDataGridView() {
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

                        row = (new string[] { "", courseSubject, name, surname, datetime, studentName, studentSurname, studentId, courseId });
                        dGVScheduleStudents.Rows.Add(row);
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
