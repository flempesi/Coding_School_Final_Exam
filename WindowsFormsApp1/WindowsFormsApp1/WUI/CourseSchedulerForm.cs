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
using WindowsFormsApp1.Storages;

namespace WindowsFormsApp1.WUI {
    public partial class CourseSchedulerForm : Form {

        public University NewUniversity = new University();
        private List<Schedule> ScheduleList { get; set; }

        private Storage Storage = new Storage();

        private bool HasDeletedRecords;
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
        private void OnLoad() {
            
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            ctrlTime.ShowUpDown = true;
            LoadGridViewCourses();
            LoadGridViewProfessors();
            //ScheduleList = new List<Schedule>();
            ScheduleList = NewUniversity.ScheduleList;
            RefreshDataGridSchedule();
        }

        public void LoadGridViewCourses() {
            SetDataGridViewProperties(dataGridViewCourses);
            dataGridViewCourses.DataSource = NewUniversity.Courses;
            dataGridViewCourses.Columns["Id"].Visible = false;
        }
        public void LoadGridViewProfessors() {
            SetDataGridViewProperties(dataGridViewProfessors);
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

        public void AddSchedule() {

            if (dataGridViewCourses.SelectedRows.Count == 1 && dataGridViewProfessors.SelectedRows.Count == 1) {
                DataGridViewRow rowCourse = dataGridViewCourses.SelectedRows[0];
                DataGridViewRow rowProfessor = dataGridViewProfessors.SelectedRows[0];
                Guid courseID = Guid.Parse(rowCourse.Cells["Id"].Value.ToString());
                Guid professorID = Guid.Parse(rowProfessor.Cells["Id"].Value.ToString());
                DateTime dateTimeSchedule = GetDateTime();

                if (CheckIfSameProfessorInSameDateTimeHasCourse(professorID, dateTimeSchedule, courseID) &&
                    CheckMaxCoursesForProfessor(professorID, dateTimeSchedule)) {
                    ScheduleList.Add(new Schedule(professorID, courseID, dateTimeSchedule));
                    RefreshDataGridSchedule();
                }
            }
        }
        public void RefreshDataGridSchedule() {
            SetDataGridViewProperties(dataGridViewSchedules);
            dataGridViewSchedules.Rows.Clear();
            MakeColumnsDataGridView();
            SetRowsDataGridView();
        }
        public void MakeColumnsDataGridView() {

            dataGridViewSchedules.ColumnCount = 6;
            dataGridViewSchedules.ColumnHeadersVisible = true; 

            DataGridViewButtonColumn DeleteButton = new DataGridViewButtonColumn();
            DeleteButton.Name = "Delete";
            DeleteButton.Text = "Delete";
            DeleteButton.UseColumnTextForButtonValue = true;
            if (dataGridViewSchedules.Columns["Delete"] == null) {
                dataGridViewSchedules.Columns.Insert(0, DeleteButton);
                dataGridViewSchedules.CellClick += new DataGridViewCellEventHandler(dataGridViewSchedules_DeleteButton_CellClick);
            }
           
            //new DataGridViewCellEventHandler(dataGridUsers_CellContentClick);



            dataGridViewSchedules.Columns[1].Name = "Id";
            //dataGridViewSchedules.Columns[1].DataPropertyName = "Id";
            dataGridViewSchedules.Columns[2].Name = "Subject";
            dataGridViewSchedules.Columns[3].Name = "Professor Name";
            dataGridViewSchedules.Columns[4].Name = "Professor Surname";
            dataGridViewSchedules.Columns[5].Name = "Date";

            dataGridViewSchedules.Columns["Id"].Visible = false;
        }
        private void dataGridViewSchedules_DeleteButton_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == dataGridViewSchedules.Columns["Delete"].Index) {
                int rowIndex = e.RowIndex ;
                DeleteSchedule(rowIndex);
            }
        }


        public void SetRowsDataGridView() {

            List<string[]> rows = new List<string[]>();
            foreach (var item in ScheduleList) {
                string ScheduleId = item.Id.ToString();
                string name = NewUniversity.Professors.Find(x => x.Id == item.ProfessorID).Name;
                string surname = NewUniversity.Professors.Find(x => x.Id == item.ProfessorID).Surname;
                string CourseSubject = NewUniversity.Courses.Find(x => x.Id == item.CourseID).Subject;
                string datetime = item.DateTimeSchedule.ToString();
                rows.Add(new string[] { "",ScheduleId, CourseSubject, name, surname, datetime });
            }

            foreach (string[] rowArray in rows) {
                dataGridViewSchedules.Rows.Add(rowArray);
            }
        }
        public void DeleteSchedule( int rowIndex) {
            //if (dataGridViewSchedules.SelectedRows.Count == 1 &&
            // dataGridViewSchedules.CurrentRow.Index != dataGridViewSchedules.Rows.Count - 1)//to not select the empty row
            //  {


            if (dataGridViewSchedules.SelectedRows.Count == 1) {
                int r = dataGridViewSchedules.SelectedRows[0].Index;
                DataGridViewRow rowSchedule = dataGridViewSchedules.SelectedRows[0];
                Guid scheduleID = Guid.Parse(rowSchedule.Cells["Id"].Value.ToString());
                Schedule schedule = ScheduleList.Find(x => x.Id == scheduleID);

                DialogResult result = MessageBox.Show("Are you sure to delete this record ? ", "Delete record", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) {
                    HasDeletedRecords = true;
                    ScheduleList.Remove(schedule);
                    RefreshDataGridSchedule();
                    SaveChanges();
                }

            }

        }
        private void SaveButtonActions() {
            // todo: add exception handling?
            if (dataGridViewSchedules.Rows.Count > 1 ) {
                SaveChanges();
                //Storage.DeserializeFromJson();
                //NewUniversity = Storage.NewUniversity;
                Close();
            }else if ( HasDeletedRecords == true) {
                Close();
            }
            else {
                MessageBox.Show("Please add a schedule to save it");
            }
        }

        private void SaveChanges() {
            //NewUniversity.ScheduleList = ScheduleList;
            Storage.NewUniversity = NewUniversity;
            Storage.SerializeToJson();
        }

        public void validate_professorCourse_with_studentCourse() {

            //TODO: ???

        }
        public bool CheckIfSameProfessorInSameDateTimeHasCourse(Guid professorId, DateTime dateTime, Guid courseid) {
            //  CANNOT ADD SAME   PROFESSOR IN SAME DATE & HOUR
            //cannot add same course in same date
            List<Schedule> proffesorSceduleList = new List<Schedule>();
            proffesorSceduleList = ScheduleList.FindAll(x => x.ProfessorID == professorId);
            foreach (var item in proffesorSceduleList) {

                if (item.DateTimeSchedule.Date.ToString("yyyyMMdd") == dateTime.Date.ToString("yyyyMMdd")) {
                    if (item.DateTimeSchedule.Hour == dateTime.Hour) {
                        return false;
                    }
                    else if (item.CourseID == courseid) {
                        return false;

                    }
                }

            }
            return true;

        }

        public bool CheckMaxCoursesForProfessor(Guid professorId, DateTime dateTime) {
            // A PROFESSOR CANNOT TEACH MORE THAN 4 COURSES PER DAY AND  40(20 right) COURSES PER WEEK
            List<Schedule> proffesorSceduleList = new List<Schedule>();
            proffesorSceduleList = ScheduleList.FindAll(x => x.ProfessorID == professorId);
            int coursesPerDay = 0;
            int coursesPerWeek = 0;
            CultureInfo culture = CultureInfo.CurrentCulture;

            foreach (var item in proffesorSceduleList) {

                if (item.DateTimeSchedule.Date.ToString("yyyyMMdd") == dateTime.Date.ToString("yyyyMMdd")) {
                    coursesPerDay++;
                }
                //if (item.DateTimeSchedule.WeekOfYear == dateTime.Date.ToString("MM")) {
                //    coursesPerMonth++;
                //}


                //var firstDayWeek = cul.Calendar.GetWeekOfYear(item.DateTimeSchedule, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                int weekNumberOfesheduledCourse = culture.Calendar.GetWeekOfYear(item.DateTimeSchedule, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

                int weekNumberOfCourse = culture.Calendar.GetWeekOfYear(dateTime.Date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

                if (weekNumberOfesheduledCourse == weekNumberOfCourse) {
                    coursesPerWeek++;

                }


                if (coursesPerDay > 3 || coursesPerWeek > 20) {
                    return false;
                }
            }
            return true;

        }
    }
}
