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
    public partial class CourseSchedulerForm : Form {

        public University NewUniversity = new University();
        private List<Schedule> _ScheduleList { get; set; }

        private Storage _Storage = new Storage();
        public CourseSchedulerForm() {
            InitializeComponent();
        }

        private void CourseSchedulerForm_Load(object sender, EventArgs e) {
            OnLoad();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            SaveButtonActions();
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            AddSchedule();
        }
        private void btnDelete_Click(object sender, EventArgs e) {
            DeleteSchedule();
        }
        private void OnLoad() {
            ctrlTime.ShowUpDown = true;
            LoadGridViewCourses();
            LoadGridViewProfessors();
            _ScheduleList = new List<Schedule>();
            _ScheduleList = NewUniversity.ScheduleList;
            RefreshDataGridSchedule();
        }

        public void LoadGridViewCourses() {
            dataGridViewCourses.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCourses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCourses.ReadOnly = true;
            dataGridViewCourses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCourses.DataSource = NewUniversity.Courses;
            dataGridViewCourses.Columns["Id"].Visible = false;
        }
        public void LoadGridViewProfessors() {
            dataGridViewProfessors.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewProfessors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewProfessors.ReadOnly = true;
            dataGridViewProfessors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewProfessors.DataSource = NewUniversity.Professors;
            dataGridViewProfessors.Columns["Id"].Visible = false;
            dataGridViewProfessors.Columns["Age"].Visible = false;
            dataGridViewProfessors.Columns["Rank"].Visible = false;
        }
        private DateTime GetDateTime() {
            string timeValue = ctrlTime.Value.ToString("HH:mm:00");
            string dateValue = ctrlDate.Value.ToString("yyyy MM dd");
            string dateTimeValue = string.Format("{0} {1}", dateValue, timeValue);
            DateTime dateTime = Convert.ToDateTime(dateTimeValue);
            return dateTime;
        }

        public void AddSchedule() {
            
            if (dataGridViewCourses.SelectedRows.Count == 1 && dataGridViewProfessors.SelectedRows.Count == 1) {
                DataGridViewRow rowCourse = dataGridViewCourses.SelectedRows[0];
                DataGridViewRow rowProfessor = dataGridViewProfessors.SelectedRows[0];
                Guid courseID = Guid.Parse(rowCourse.Cells["id"].Value.ToString());
                Guid professorID = Guid.Parse(rowProfessor.Cells["id"].Value.ToString());
                DateTime dateTimeSchedule = GetDateTime();

                if (CheckIfSameProfessorInSameDateTimeHasCourse(professorID, dateTimeSchedule,courseID) &&
                    CheckMaxCoursesForProfessor(professorID, dateTimeSchedule)) {
                    _ScheduleList.Add(new Schedule(professorID, courseID, dateTimeSchedule));
                    RefreshDataGridSchedule();
                }
            }
        }
        public void RefreshDataGridSchedule() {
            dataGridViewSchedules.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewSchedules.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewSchedules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewSchedules.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewSchedules.ReadOnly = true;
            dataGridViewSchedules.Rows.Clear();
            MakeColumnsDataGridView();
            SetRowsDataGridView();
        }
        public void MakeColumnsDataGridView() {

            dataGridViewSchedules.ColumnCount = 5;
            dataGridViewSchedules.ColumnHeadersVisible = true;

            dataGridViewSchedules.Columns[0].Name = "Id";
            dataGridViewSchedules.Columns[1].Name = "Subject";
            dataGridViewSchedules.Columns[2].Name = "Professor Name";
            dataGridViewSchedules.Columns[3].Name = "Professor Surname";
            dataGridViewSchedules.Columns[4].Name = "Date";

            dataGridViewSchedules.Columns["Id"].Visible = false;
        }
        public void SetRowsDataGridView() {

            List<string[]> rows = new List<string[]>();
            foreach (var item in _ScheduleList) {
                string ScheduleId = item.Id.ToString();
                string name = NewUniversity.Professors.Find(x => x.Id == item.ProfessorID).Name;
                string surname= NewUniversity.Professors.Find(x => x.Id == item.ProfessorID).Surname;
                string CourseSubject= NewUniversity.Courses.Find(x => x.Id == item.CourseID).Subject;
                string datetime= item.DateTimeSchedule.ToString();
                rows.Add(new string[] { ScheduleId, CourseSubject, name, surname, datetime});
            }
          
            foreach (string[] rowArray in rows) {
                dataGridViewSchedules.Rows.Add(rowArray);
            }
        }
        public void DeleteSchedule() {
            if (dataGridViewSchedules.SelectedRows.Count==1 &&
                 dataGridViewSchedules.CurrentRow.Index != dataGridViewSchedules.Rows.Count - 1)//to not select the empty row
                      {
                DataGridViewRow rowSchedule = dataGridViewSchedules.SelectedRows[0];
                Guid scheduleID = Guid.Parse(rowSchedule.Cells["id"].Value.ToString());
                Schedule schedule = _ScheduleList.Find(x => x.Id == scheduleID);
                _ScheduleList.Remove(schedule);
                RefreshDataGridSchedule();
            }
            
        }
        private void SaveButtonActions() {
            // todo: add exception handling?
            if (dataGridViewSchedules.Rows.Count > 1) {
            NewUniversity.ScheduleList = _ScheduleList;
                _Storage.NewUniversity = NewUniversity;
            _Storage.SerializeToJson();
            _Storage.DeserializeFromJson();
             NewUniversity = _Storage.NewUniversity;
                Close();
            }
            else {
                MessageBox.Show("Please Add a schedule to save it");
            }
        }
        public void validate_professorCourse_with_studentCourse() {

            //TODO: ???

        }
        public bool CheckIfSameProfessorInSameDateTimeHasCourse(Guid professorId, DateTime dateTime,Guid courseid) {
            //  CANNOT ADD SAME   PROFESSOR IN SAME DATE & HOUR
            //cannot add same course in same date
            List<Schedule> proffesorSceduleList = new List<Schedule>();
            proffesorSceduleList = _ScheduleList.FindAll(x => x.ProfessorID == professorId);
            foreach (var item in proffesorSceduleList) {
                //if (item.DateTimeSchedule == dateTime) {
                //    return false;
                //}
                if (item.DateTimeSchedule.Date.ToString("yyyyMMdd") == dateTime.Date.ToString("yyyyMMdd")) {
                    if (item.DateTimeSchedule.Hour == dateTime.Hour) {
                        return false;
                    }
                    else if (item.CourseID==courseid){
                        return false;

                    }
                }

            }
            return true;

        }
       
        public bool CheckMaxCoursesForProfessor(Guid professorId, DateTime dateTime) {
            // A PROFESSOR CANNOT TEACH MORE THAN 4 COURSES PER DAY AND  40(20 right) COURSES PER WEEK
            List<Schedule> proffesorSceduleList = new List<Schedule>();
            proffesorSceduleList = _ScheduleList.FindAll(x => x.ProfessorID == professorId);
            int coursesPerDay = 0;
            int coursesPerMonth = 0;
            foreach (var item in proffesorSceduleList) {

                if (item.DateTimeSchedule.Date.ToString("yyyyMMdd") == dateTime.Date.ToString("yyyyMMdd")) {
                    coursesPerDay++;
                }
                if (item.DateTimeSchedule.Date.ToString("MM") == dateTime.Date.ToString("MM")) {
                    coursesPerMonth++;
                }

            }
            if (coursesPerDay > 3 || coursesPerMonth > 20) {
                return false;
            }
            return true;

        }
    }
}
