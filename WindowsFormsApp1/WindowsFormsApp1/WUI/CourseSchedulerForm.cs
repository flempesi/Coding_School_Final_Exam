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
        private List<Schedule> ScheduleList  = new List<Schedule>();

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
            
            ScheduleList = NewUniversity.ScheduleList;
            RefreshDataGridSchedule();
        }

        public void LoadGridViewCourses() {
            SetDataGridViewProperties(dGVCourses);

            List<string> ColumnsNames = new List<string> { "Id", "Code", "Subject", "Hours", "Category" };
            List<string> HideColumnsNames = new List<string> { "Id" };

            MakeColumnsDataGridView(dGVCourses, false, 5, ColumnsNames, HideColumnsNames);
            SetRowsDataGridViewCourses(NewUniversity.Courses, dGVCourses);

        }
        public void SetRowsDataGridViewCourses(List<Course> Courses, DataGridView dataGridView) {
            string[] row;

            foreach (var item in Courses) {
                row = (new string[] { item.Id.ToString(), item.Code, item.Subject, item.Hours.ToString(), item.Category.ToString() });
                dataGridView.Rows.Add(row);
            }
        }
        public void LoadGridViewProfessors() {
            SetDataGridViewProperties(dGVProfessors);

            List<string> ColumnsNames = new List<string> { "Id", "Name", "Surname" };
            List<string> HideColumnsNames = new List<string> { "Id" };

            MakeColumnsDataGridView(dGVProfessors, false, 3, ColumnsNames, HideColumnsNames);
            SetRowsDataGridViewProfessors(NewUniversity.Professors, dGVProfessors);
        }
        public void SetRowsDataGridViewProfessors(List<Professor> professors, DataGridView dataGridView) {
            string[] row;

            foreach (var item in professors) {
                row = (new string[] { item.Id.ToString(), item.Name, item.Surname});
                dataGridView.Rows.Add(row);
            }
        }
        private DateTime GetDateTime() {
            string timeValue = ctrlTime.Value.ToString("HH:mm:00");
            string dateValue = ctrlDate.Value.ToString("yyyy MM dd");
            string dateTimeValue = string.Format("{0} {1}", dateValue, timeValue);
            DateTime dateTime = Convert.ToDateTime(dateTimeValue);
            return dateTime;
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

        public void AddSchedule() {

            if (dGVCourses.SelectedRows.Count == 1 && dGVProfessors.SelectedRows.Count == 1) {
                DataGridViewRow rowCourse = dGVCourses.SelectedRows[0];
                DataGridViewRow rowProfessor = dGVProfessors.SelectedRows[0];
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
            SetDataGridViewProperties(dGVSchedule);
            dGVSchedule.Rows.Clear();

            List<string> ColumnsNames = new List<string> { "Id","Subject", "Professor Name", "Professor Surname", "Date" };
            List<string> HideColumnsNames = new List<string> { "Id"};
            MakeColumnsDataGridView(dGVSchedule, true,6, ColumnsNames, HideColumnsNames);
            SetRowsDataGridView();
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
                    dataGridView.CellClick += new DataGridViewCellEventHandler(dataGridViewSchedules_DeleteButton_CellClick);
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
        //public void MakeColumnsDataGridView() {

        //    dGVSchedule.ColumnCount = 6;

        //    DataGridViewButtonColumn DeleteButton = new DataGridViewButtonColumn();
        //    DeleteButton.Name = "Delete";
        //    DeleteButton.Text = "Delete";
        //    DeleteButton.UseColumnTextForButtonValue = true;
        //    if (dGVSchedule.Columns["Delete"] == null) {
        //        dGVSchedule.Columns.Insert(0, DeleteButton);
        //        dGVSchedule.CellClick += new DataGridViewCellEventHandler(dataGridViewSchedules_DeleteButton_CellClick);
        //   }
           

        //    dGVSchedule.Columns[1].Name = "Id";
        //    dGVSchedule.Columns[2].Name = "Subject";
        //    dGVSchedule.Columns[3].Name = "Professor Name";
        //    dGVSchedule.Columns[4].Name = "Professor Surname";
        //    dGVSchedule.Columns[5].Name = "Date";

        //    dGVSchedule.Columns[1].Visible = false;

        //    if (dGVSchedule.Columns.Contains("")==true) {
        //        dGVSchedule.Columns.Remove("");
        //    }
           
        //}
        private void dataGridViewSchedules_DeleteButton_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == dGVSchedule.Columns["Delete"].Index) {
                DeleteSchedule();
            }
        }


        public void SetRowsDataGridView() {

            string[] row = new string[6];
            foreach (var item in ScheduleList) {
                string ScheduleId = item.Id.ToString();
                if (ScheduleId != null) {
                    string name = NewUniversity.Professors.Find(x => x.Id == item.ProfessorID).Name;
                    string surname = NewUniversity.Professors.Find(x => x.Id == item.ProfessorID).Surname;
                    string CourseSubject = NewUniversity.Courses.Find(x => x.Id == item.CourseID).Subject;
                    string datetime = item.DateTimeSchedule.ToString();
                    row = (new string[] { "", ScheduleId, CourseSubject, name, surname, datetime });
                    dGVSchedule.Rows.Add(row);
                }
            }

        }
        public void DeleteSchedule() {
            if (dGVSchedule.SelectedRows.Count == 1) {
                int r = dGVSchedule.SelectedRows[0].Index;
                DataGridViewRow rowSchedule = dGVSchedule.SelectedRows[0];
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
            
            if (dGVSchedule.Rows.Count > 1 ) {
                SaveChanges();
                Close();
            }else if ( HasDeletedRecords == true) {
                Close();
            }
            else {
                MessageBox.Show("Please add a schedule to save it");
            }
        }

        private void SaveChanges() {
            Storage.NewUniversity = NewUniversity;
            Storage.SerializeToJson();
        }

        public void validate_professorCourse_with_studentCourse() {

            //TODO: ?Not doent need cause it did it logically cause i have two forms

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
