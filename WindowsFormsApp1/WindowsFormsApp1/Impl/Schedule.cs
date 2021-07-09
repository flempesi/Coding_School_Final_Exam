using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Base;

namespace WindowsFormsApp1.Impl {
    public class Schedule : Entity {
        public Guid ProfessorID { get; set; }
        public Guid CourseID { get; set; }
        public DateTime DateTimeSchedule { get; set; }
        //public List<Student> Students { get; set; }

        public Schedule() :base(){

        }
        public Schedule(Guid professorID, Guid courseID, DateTime dateTimeSchedule) : base() {
            ProfessorID = professorID;
            CourseID = courseID;
            DateTimeSchedule = dateTimeSchedule;
        }

    }
}
