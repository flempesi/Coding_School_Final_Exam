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
            //Courses
            Courses.Add(new Course("111", "hlektromagnhtikh", 5, CoursesCategoryEnum.Physics));
            Courses.Add(new Course("111", "Math I", 5, CoursesCategoryEnum.Mathematics));

            //Students
            Students.Add(new Student("Foteini","Lempesi",26,"123"));
            Students.Add(new Student("Anna", "Papa", 23, "124"));

            //Professors
            Professors.Add(new Professor("maria","Papadopouloy",30,"Assistant"));
            Professors.Add(new Professor("Fotis", "Chrisoulas", 35, "Assistant"));

        }

    }

}

