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
        }
        public void LoadGridViewCourses() {
            dataGridViewCourses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCourses.DataSource = NewUniversity.Courses;
            dataGridViewCourses.Columns["Id"].Visible = false;
        }
        public void LoadGridViewProfessors() {
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
            DataGridViewRow rowCourse = dataGridViewCourses.SelectedRows[0];
            DataGridViewRow rowProfessor = dataGridViewProfessors.SelectedRows[0];

            Guid courseID = Guid.Parse(rowCourse.Cells["id"].Value.ToString());
            Guid professorID = Guid.Parse(rowProfessor.Cells["id"].Value.ToString());
            DateTime dateTimeSchedule = GetDateTime();

            if (CheckIfSameProfessorInSameDateTimeHasCourse (professorID, dateTimeSchedule) &&
                CheckMaxCoursesForProfessor(professorID, dateTimeSchedule)) {
                _ScheduleList.Add(new Schedule(professorID, courseID, dateTimeSchedule));
                RefreshDataGridSchedule();
            }
        }
        public void RefreshDataGridSchedule() {
            dataGridViewSchedules.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewSchedules.DataSource = null;
            dataGridViewSchedules.DataSource = NewUniversity.ScheduleList;
        }
        public void DeleteSchedule() {
            DataGridViewRow rowSchedule = dataGridViewSchedules.SelectedRows[0];
            Guid scheduleID = Guid.Parse(rowSchedule.Cells["id"].Value.ToString());
            Schedule schedule = _ScheduleList.Find(x => x.Id == scheduleID);
            _ScheduleList.Remove(schedule);
            RefreshDataGridSchedule();
        }
        private void SaveButtonActions() {
            // todo: add exception handling?
            NewUniversity.ScheduleList = _ScheduleList;
            _Storage.SerializeToJson();
            _Storage.DeserializeFromJson();
            Close();
        }
        public void validate_professorCourse_with_studentCourse() {

            //TODO: ???

        }
        public bool CheckIfSameProfessorInSameDateTimeHasCourse(Guid professorId,DateTime dateTime) {
            //  CANNOT ADD SAME   PROFESSOR IN SAME DATE & HOUR
            List<Schedule> proffesorSceduleList = new List<Schedule>();
            proffesorSceduleList = _ScheduleList.FindAll(x => x.ProfessorID == professorId);
            foreach (var item in proffesorSceduleList) {
                if(item.DateTimeSchedule== dateTime) {
                    return false;
                }

            }
            return true;

        }
        public void CheckIfSameStudentInSameDateTimeHasCourse() {
            // TODO:   CANNOT ADD SAME STUDENT   IN SAME DATE & HOUR

        }
        public void CheckMaxCoursesPerStudentIsLessThan4PerDay() {
            // TODO:   EACH STUDENT CANNOT HAVE MORE THAN 3 COURSES PER DAY!

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
