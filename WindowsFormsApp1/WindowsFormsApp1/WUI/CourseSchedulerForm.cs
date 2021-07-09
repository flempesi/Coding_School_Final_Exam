using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using WindowsFormsApp1.Impl;

namespace WindowsFormsApp1.WUI {

    public partial class CourseSchedulerForm : Form {

        private University objects = new University();

        public CourseSchedulerForm() {
            InitializeComponent();
        }

        #region old code
        private void CourseSchedulerForm_Load(object sender, EventArgs e) {

            ctrlTime.ShowUpDown = true;
            // todo : load data on enter!
            //LoadData

            GetDateTime();

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

        private void loadDataToolStripMenuItem_Click(object sender, EventArgs e) {

            JavaScriptSerializer r = new JavaScriptSerializer();

            objects = r.Deserialize<University>(File.ReadAllText("Data.json"));

            foreach (Student a in objects.Students) {
                list1.Items.Add(a.Name + " " + a.Surname);
            }

            for (int i = 0; i < objects.Courses.Count - 1; i++) {

                listBox1.Items.Add(objects.Courses[i].Code + "--" + objects.Courses[i].Subject);
            }


            foreach (Professor k in objects.Professors) {
                list3.Items.Add(string.Format("{0}  {1}", k.Name, k.Surname));
            }
        }

        private void saveDataToolStripMenuItem_Click(object sender, EventArgs e) {
            JavaScriptSerializer save_Serializer = new JavaScriptSerializer();

            File.WriteAllText("Data.json", save_Serializer.Serialize(objects));
        }
        #endregion

        #region new code
       

        private void initializeDedomenaToolStripMenuItem_Click(object sender, EventArgs e) {

            //objects.run_once();

            foreach (Student a in objects.Students) {
                list1.Items.Add(a.Name + " " + a.Surname);
            }

            foreach (Course bb in objects.Courses) {
                listBox1.Items.Add(bb.Code + "--" + bb.Subject);
            }


            foreach (Professor cc1 in objects.Professors) {

                list3.Items.Add(string.Format("{0}  {1}", cc1.Name, cc1.Surname));
            }

            //should run only once!
            button11.Hide();
        }

        private void button9_Click(object sender, EventArgs e) {

            JavaScriptSerializer GG = new JavaScriptSerializer();

            objects = GG.Deserialize<University>(File.ReadAllText("Data.json"));

            foreach (Student a in objects.Students) {
                list1.Items.Add(a.Name + " " + a.Surname);
            }

            for (int i = 0; i < objects.Courses.Count - 1; i++) {

                listBox1.Items.Add(objects.Courses[i].Code + "--" + objects.Courses[i].Subject);
            }

            // we do a loop
            foreach (Professor cc1 in objects.Professors) {
                // we add to the list
                list3.Items.Add(string.Format("{0}  {1}", cc1.Name, cc1.Surname));
            }

        }

        private void button10_Click(object sender, EventArgs e) {
            JavaScriptSerializer ff = new JavaScriptSerializer();

            File.WriteAllText("Data.json", ff.Serialize(objects));
        }

        private void ctrlExit_Click(object sender, EventArgs e) {

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

        private void button11_Click(object sender, EventArgs e) {

            

            //objects.run_once();

            foreach (Student a in objects.Students) {
                list1.Items.Add(a.Name + " " + a.Surname);
            }

            foreach (Course bb in objects.Courses) {
                listBox1.Items.Add(bb.Code + "--" + bb.Subject);
            }


            foreach (Professor cc1 in objects.Professors) {

                list3.Items.Add(string.Format("{0}  {1}", cc1.Name, cc1.Surname));
            }

            //should run only once!
            button11.Hide();
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

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {

        }

        private void ctrlDateTime_ValueChanged(object sender, EventArgs e) {

        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e) {

        }
    }
}

