using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Base;

namespace WindowsFormsApp1.Impl {
    public class Student :Person{
        public int RegistrationNumber { get; set; }
        

        public Student() : base() {
            
        }
        public Student(string name, string surname, int age,int registrationNumber) : base(name,surname,age) {
            RegistrationNumber = registrationNumber;
           

        }
    }
   
}
