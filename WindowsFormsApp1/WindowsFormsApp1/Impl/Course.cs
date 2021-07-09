using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Base;

namespace WindowsFormsApp1.Impl {
    public class Course : Entity {
        public string Code { get; set; }
        public string Subject { get; set; }
        public int Hours { get; set; }
        public CoursesCategoryEnum Category { get; set; }
        public Course() {

        }
        public Course(string code, string subject, int hours, CoursesCategoryEnum category) : base(){
             
            Code = code;
            Subject = subject;
            Hours = hours;
            Category = category;
        }

    }
}
