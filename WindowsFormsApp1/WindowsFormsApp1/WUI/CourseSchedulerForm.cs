using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using WindowsFormsApp1.Impl;
using WindowsFormsApp1.Storages;

namespace WindowsFormsApp1.WUI {

    public partial class CourseSchedulerForm : Form {

        public University NewUniversity = new University();
        private Storage _Storage = new Storage();
        private const string _JsonFile = "UniversityData.json";

        public CourseSchedulerForm() {
            InitializeComponent();
        }
        private void CourseSchedulerForm_Load(object sender, EventArgs e) {
            ctrlTime.ShowUpDown = true;
            NewUniversity.InsertDataToUniversity();
            _Storage.DeserializeFromJson();
            AddToListBoxes();
        }
        private void btnSave_Click(object sender, EventArgs e) {
            _Storage.SerializeToJson();
        }

        private void btnRemove_Click(object sender, EventArgs e) {
           
        }
        private void btnCancel_Click(object sender, EventArgs e) {
            Close();
        }
        private void GetDateTime() {
            //ctrlTime.Format = DateTimePickerFormat.Custom;
            //ctrlTime.CustomFormat = "HH:mm";
           
            string timeValue = ctrlTime.Value.ToString("HH:mm:00");
            string dateValue = ctrlDate.Value.ToString("yyyy MM dd");
            string dateTimeValue = string.Format("{0} {1}", dateValue, timeValue);
            //DateTime Date = Convert.ToDateTime(dateValue);
            DateTime dt = Convert.ToDateTime(dateTimeValue);
            // DateTime dateTime = new DateTime//(Date.Year,Date.Month,Date.Day,Time.Hour,Time.Minute,Time.Second);
            //ctrlDate.CustomFormat = "yyyyMMdd";
        }

        #region new code
       


       

        private void btnAdd_Click(object sender, EventArgs e) {
            //btnAdd
            try {

                // TODO: 1. CANNOT ADD SAME STUDENT + PROFESSOR IN SAME DATE & HOUR

                // TODO: 2. EACH STUDENT CANNOT HAVE MORE THAN 3 COURSES PER DAY!

                // TODO: 3. A PROFESSOR CANNOT TEACH MORE THAN 4 COURSES PER DAY AND 40 COURSES PER WEEK

                //objects.ScheduleList.Add(new Schedule() { Course = listBox1.SelectedItem.ToString(), Student = list1.SelectedItem.ToString(), Professor = list3.SelectedItem.ToString(), Calendar = dateTimePicker2.Value });

                //ctrlSchedule.Items.Clear();
                //foreach (var AA in objects.ScheduleList) {

                //    ctrlSchedule.Items.Add(AA.Calendar + " | " + AA.Course + " | " + AA.Student + " | " + AA.Professor);

                //}
            }
            catch { 
            
            }
            finally {
                MessageBox.Show("all ok!");

            }
        }

        public void validate_professorCourse_with_studentCourse() { 
        
            //TODO: ???

        }

        #endregion

       

        private void AddToListBoxes() {
            foreach (Student a in NewUniversity.Students) {
                list1.Items.Add(a.Name + " " + a.Surname);
            }

            foreach (Course bb in NewUniversity.Courses) {
                listBox1.Items.Add(bb.Code + "--" + bb.Subject);
            }


            foreach (Professor cc1 in NewUniversity.Professors) {

                list3.Items.Add(string.Format("{0}  {1}", cc1.Name, cc1.Surname));
            }
        }

        private void addToScheduleToolStripMenuItem_Click(object sender, EventArgs e) {

            // todo : display on a grid??

            // todo: add exception handling?
                //objects.ScheduleList.Add(new Schedule() { 
                //    Course = listBox1.SelectedItem.ToString(), Student = list1.SelectedItem.ToString()
                //        , Professor = list3.SelectedItem.ToString(), Calendar = dateTimePicker2.Value });

                //ctrlSchedule.Items.Clear();
                //foreach (var AA in objects.ScheduleList) {

                //    ctrlSchedule.Items.Add(
                //        AA.Calendar + " " + 
                //        AA.Course + " " + 
                //        AA.Student + " " + 
                //        AA.Professor);

                //}
        
        }

       
    }
}

