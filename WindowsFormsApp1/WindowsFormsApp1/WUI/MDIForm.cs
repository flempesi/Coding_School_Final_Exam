﻿using System;
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
    public partial class MDIForm : Form {

        public University NewUniversity = new University();
        private Storage _Storage = new Storage();
        public MDIForm() {
            InitializeComponent();
        }
        private void MDIForm_Load(object sender, EventArgs e) {
            NewUniversity.InsertDataToUniversity();
            _Storage.DeserializeFromJson();
        }

        private void courseSchedulerToolStripMenuItem_Click(object sender, EventArgs e) {
            CourseSchedulerForm courseSchedulerForm = new CourseSchedulerForm();
            courseSchedulerForm.MdiParent = this;
            courseSchedulerForm.NewUniversity = NewUniversity;
            courseSchedulerForm.Show();
            
        }

        private void addCourseToStudentToolStripMenuItem_Click(object sender, EventArgs e) {
            AddCourseToStudentForm addCourseToStudentForm = new AddCourseToStudentForm();
            addCourseToStudentForm.MdiParent = this;
            //addCourseToStudentForm.NewUniversity = NewUniversity;
            addCourseToStudentForm.Show();
        }

        
    }
}