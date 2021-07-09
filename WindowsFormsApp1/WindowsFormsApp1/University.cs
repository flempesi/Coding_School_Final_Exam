using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Impl {

    public class University  {

        public List<Student> Students { get; set; }
        public List<Professor> prof_list { get; set; }
        
        public University() {
            Students = new List<Student>();
            prof_list = new List<Professor>();
            _course = new List<Course>();
            ScheduleList = new List<Schedule>();
        }

        public List<Schedule> ScheduleList { get; set; }

        public void run_once() {


            // TODO: MUST IMPLEMENT ENUMERATION FOR CATEGORY ?

            _course.Add( new Course() {
                id = "1", 
                Code = "1",
                Subject = "Quantum Physics",
                Category = 0, // physics
                Hours = 100
            });

            _course.Add(new Course() {
                id = "2",
                Code = "2",
                Subject = "Electo-Dynamics",
                Category = 0, // physics ?
                Hours = 50
            });

            _course.Add(new Course() {
                id = "3",
                Code = "03",
                Subject = "Basic Chemistry",
                Category = 1, // Chemistry
                Hours = 50
            });

            _course.Add(new Course() {
                id = "4",
                Code = "04",
                Subject = "Financial II",
                Category = 2, // Financial
                Hours = 50
            });

            _course.Add(new Course() {
                id = "5",
                Code = "5",
                Subject = "Mathematics I",
                Category = 3, // Mathematics
                Hours = 50
            });

            Students.Add(new Student() {
                id = Guid.NewGuid(),
                name = "Fotis",
                surname = "Chrysoulas",
                Registration_Number = "1234",
                CAN_LEARN = new List<CoursesCategoryEnum>() { CoursesCategoryEnum.Chemistry, CoursesCategoryEnum.Financial }
            });


            Students.Add(new Student() {
                id = Guid.NewGuid(),
                name = "Dimitris",
                surname = "Raptodimos",
                Registration_Number = "1235",
                CAN_LEARN = new List<CoursesCategoryEnum>() { CoursesCategoryEnum.Physics, CoursesCategoryEnum.Financial } 
            });

            prof_list.Add(new Professor() {
                Name = "Maria",
                Surname = "Papadopoulou",
                Rank = "1"
            });


        }

        public List<Course> _course { get; set; }
    }

    public class Student {
        public Guid id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string Registration_Number { get; set; }
        public int _Age { get; set; }
        public List<CoursesCategoryEnum> CAN_LEARN { get; set; }
    }

    public class Professor {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public short Age { get; set; }
        public string Rank { get; set; }
        public List<CoursesCategoryEnum> CAN_TEACH { get; set; }

        public Professor() {
            ID = Guid.NewGuid();
        }
    }

    public class Course  {
        public string id { get; set; }
        public string Code { get; set; }
        public string Subject { get; set; }
        public int Hours { get; set; }
        public int Category { get; set; }

        public Course() {
                
        }

    }

    public class Schedule {

        public string ID { get; set; }
        public string Student { get; set; }
        public string Professor { get; set; }
        public string Course { get; set; }
        public DateTime Calendar { get; set; }

        ~Schedule() {
                
        }
    }

   
    public enum CoursesCategoryEnum {
        Physics,
        Mathematics,
        Chemistry,
        Financial
    }

}

