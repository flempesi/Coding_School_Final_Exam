using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using WindowsFormsApp1.Impl;

namespace WindowsFormsApp1.WUI {

    public partial class DataForm1 : Form {

        private University objects = new University();

        public DataForm1() {
            InitializeComponent();
        }

        #region old code
        private void DataForm_Load(object sender, EventArgs e) {

            // todo : load data on enter!
        }

        private void loadDataToolStripMenuItem_Click(object sender, EventArgs e) {

            JavaScriptSerializer r = new JavaScriptSerializer();

            objects = r.Deserialize<University>(File.ReadAllText("Data.json"));

            foreach (Student a in objects.Students) {
                list1.Items.Add(a.name + " " + a.surname);
            }

            for (int i = 0; i < objects._course.Count - 1; i++) {

                listBox1.Items.Add(objects._course[i].Code + "--" + objects._course[i].Subject);
            }


            foreach (Professor k in objects.prof_list) {
                list3.Items.Add(string.Format("{0}  {1}", k.Name, k.Surname));
            }
        }

        private void saveDataToolStripMenuItem_Click(object sender, EventArgs e) {
            JavaScriptSerializer save_Serializer = new JavaScriptSerializer();

            File.WriteAllText("Data.json", save_Serializer.Serialize(objects));
        }
        #endregion

        #region new code
        private void DataForm1_Load(object sender, EventArgs e) {

            // todo : load data on enter!
        }

        private void initializeDedomenaToolStripMenuItem_Click(object sender, EventArgs e) {

            objects.run_once();

            foreach (Student a in objects.Students) {
                list1.Items.Add(a.name + " " + a.surname);
            }

            foreach (Course bb in objects._course) {
                listBox1.Items.Add(bb.Code + "--" + bb.Subject);
            }


            foreach (Professor cc1 in objects.prof_list) {

                list3.Items.Add(string.Format("{0}  {1}", cc1.Name, cc1.Surname));
            }

            //should run only once!
            button11.Hide();
        }

        private void button9_Click(object sender, EventArgs e) {

            JavaScriptSerializer GG = new JavaScriptSerializer();

            objects = GG.Deserialize<University>(File.ReadAllText("Data.json"));

            foreach (Student a in objects.Students) {
                list1.Items.Add(a.name + " " + a.surname);
            }

            for (int i = 0; i < objects._course.Count - 1; i++) {

                listBox1.Items.Add(objects._course[i].Code + "--" + objects._course[i].Subject);
            }

            // we do a loop
            foreach (Professor cc1 in objects.prof_list) {
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

                objects.ScheduleList.Add(new Schedule() { Course = listBox1.SelectedItem.ToString(), Student = list1.SelectedItem.ToString(), Professor = list3.SelectedItem.ToString(), Calendar = dateTimePicker2.Value });

                ctrlSchedule.Items.Clear();
                foreach (var AA in objects.ScheduleList) {

                    ctrlSchedule.Items.Add(AA.Calendar + " | " + AA.Course + " | " + AA.Student + " | " + AA.Professor);

                }
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

            

            objects.run_once();

            foreach (Student a in objects.Students) {
                list1.Items.Add(a.name + " " + a.surname);
            }

            foreach (Course bb in objects._course) {
                listBox1.Items.Add(bb.Code + "--" + bb.Subject);
            }


            foreach (Professor cc1 in objects.prof_list) {

                list3.Items.Add(string.Format("{0}  {1}", cc1.Name, cc1.Surname));
            }

            //should run only once!
            button11.Hide();
        }

        private void addToScheduleToolStripMenuItem_Click(object sender, EventArgs e) {

            // todo : display on a grid??

            // todo: add exception handling?
                objects.ScheduleList.Add(new Schedule() { 
                    Course = listBox1.SelectedItem.ToString(), Student = list1.SelectedItem.ToString()
                        , Professor = list3.SelectedItem.ToString(), Calendar = dateTimePicker2.Value });

                ctrlSchedule.Items.Clear();
                foreach (var AA in objects.ScheduleList) {

                    ctrlSchedule.Items.Add(
                        AA.Calendar + " " + 
                        AA.Course + " " + 
                        AA.Student + " " + 
                        AA.Professor);

                }
        
        }

    }
}

