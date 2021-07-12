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
using WindowsFormsApp1.Methods;
using WindowsFormsApp1.Storages;

namespace WindowsFormsApp1.WUI {
    public partial class MDIForm : Form {

        private University NewUniversity = new University();
        private MdiMethods _MdiMethods = new MdiMethods();
        private Storage _Storage = new Storage();

        public MDIForm() {
            InitializeComponent();
        }
        private void MDIForm_Load(object sender, EventArgs e) {
            OnLoad();
        }
        private void courseSchedulerToolStripMenuItem_Click(object sender, EventArgs e) {
            _MdiMethods.OpenCourseschedulerForm(this, NewUniversity);

        }
        private void addCourseToStudentToolStripMenuItem_Click(object sender, EventArgs e) {
            _MdiMethods.OpenAddCourseToStudentForm(this, NewUniversity);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }
        public void OnLoad() {
            this.WindowState = FormWindowState.Maximized;
            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.LightGray;
            //_MdiMethods.Initialize(NewUniversity);
            _Storage.DeserializeFromJson();
            NewUniversity = _Storage.NewUniversity;
        }

    }
}
