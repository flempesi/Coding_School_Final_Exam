using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Base;

namespace WindowsFormsApp1.Impl {
    public class Professor : Person{
        public string Rank { get; set; }
        public List<Course> Courses { get; set; }
        public Professor() {

        }
    }
}
