using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Impl {

    public class University  {
        public List<Student> Students { get; set; }
        public List<Professor> Professors { get; set; }
        public List<Schedule> ScheduleList { get; set; }
        public List<Course> Courses { get; set; }

        public University() {
            Students = new List<Student>();
            Professors = new List<Professor>();
            Courses = new List<Course>();
            ScheduleList = new List<Schedule>();
        }

        public void InsertDataToUniversity() {

        }

    }

}

