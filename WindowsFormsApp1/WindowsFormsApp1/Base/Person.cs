using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Impl;

namespace WindowsFormsApp1.Base {
    public class Person : Entity {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public List<Course> Courses { get; set; }
        public Person() :base(){
            Courses = new List<Course>();
        }
        public Person(string name, string surname, int age) : base() {
            Name = name;
            Surname = surname;
            Age = age;
            Courses = new List<Course>();
        }

    }
}
