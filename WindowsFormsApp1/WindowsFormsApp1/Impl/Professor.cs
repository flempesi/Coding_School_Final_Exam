using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Base;

namespace WindowsFormsApp1.Impl {
    public class Professor : Person{
        public string Rank { get; set; }
       
        public Professor() : base() {
            
        }
        public Professor(string name, string surname, int age, string rank) : base(name,surname,age) {
            Rank = rank;
            
        }
    }
}
