using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Impl;
using WindowsFormsApp1.Storages;
using WindowsFormsApp1.WUI;

namespace WindowsFormsApp1.Methods {
    class MdiMethods {
        private Storage _Storage = new Storage();
        private InsertDataMethod _InsertDataMethod = new InsertDataMethod();


        public void OpenCourseschedulerForm(Form form, University newUniversity) {
            CourseSchedulerForm courseSchedulerForm = new CourseSchedulerForm();
            courseSchedulerForm.MdiParent = form;
            courseSchedulerForm.NewUniversity = newUniversity;
            courseSchedulerForm.Show();
        }
        public void OpenAddCourseToStudentForm(Form form, University newUniversity) {
            AddCourseToStudentForm addCourseToStudentForm = new AddCourseToStudentForm();
            addCourseToStudentForm.MdiParent = form;
            addCourseToStudentForm.NewUniversity = newUniversity;
            addCourseToStudentForm.Show();
        }
        public void Initialize(University newUniversity) {
            _InsertDataMethod.InsertDataToUniversity(newUniversity);
            _Storage.NewUniversity = newUniversity;
            _Storage.SerializeToJson();
        }
    }
}
