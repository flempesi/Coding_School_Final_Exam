using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Impl;

namespace WindowsFormsApp1.Methods {
    class InsertDataMethod {

        public void InsertDataToUniversity(University university) {
            //Courses
            // Physics,Mathematics,Chemistry,Financial,IT

            university.Courses.Add(new Course("010", "Math I", 2, CoursesCategoryEnum.Mathematics));
            university.Courses.Add(new Course("011", "Math II", 4, CoursesCategoryEnum.Mathematics));
            university.Courses.Add(new Course("012", "Math III", 2, CoursesCategoryEnum.Mathematics));
            university.Courses.Add(new Course("013", "Algebra", 3, CoursesCategoryEnum.Mathematics));
            university.Courses.Add(new Course("014", "Geometry", 2, CoursesCategoryEnum.Mathematics));

            university.Courses.Add(new Course("020", "Physics II", 3, CoursesCategoryEnum.Physics));
            university.Courses.Add(new Course("021", "Physics III", 3, CoursesCategoryEnum.Physics));
            university.Courses.Add(new Course("022", "Quantum mechanics", 3, CoursesCategoryEnum.Physics));

            university.Courses.Add(new Course("031", "organic", 2, CoursesCategoryEnum.Chemistry));
            university.Courses.Add(new Course("032", "inorganic", 3, CoursesCategoryEnum.Chemistry));
            university.Courses.Add(new Course("033", "analytical", 2, CoursesCategoryEnum.Chemistry));
            university.Courses.Add(new Course("034", "Algebra", 4, CoursesCategoryEnum.Chemistry));
            university.Courses.Add(new Course("035", "biochemistry", 2, CoursesCategoryEnum.Chemistry));


            university.Courses.Add(new Course("041", "Accounting", 3, CoursesCategoryEnum.Financial));
            university.Courses.Add(new Course("042", "Financial mathematics", 1, CoursesCategoryEnum.Financial));
            university.Courses.Add(new Course("043", "Financial management", 4, CoursesCategoryEnum.Chemistry));
            university.Courses.Add(new Course("044", "Algebra", 2, CoursesCategoryEnum.Chemistry));
            university.Courses.Add(new Course("045", "biochemistry", 2, CoursesCategoryEnum.Chemistry));

            university.Courses.Add(new Course("051", "C Shapr progr", 3, CoursesCategoryEnum.IT));
            university.Courses.Add(new Course("052", "Java progr", 3, CoursesCategoryEnum.IT));
            university.Courses.Add(new Course("053", "Php progr", 4, CoursesCategoryEnum.IT));
            university.Courses.Add(new Course("054", "WWW", 3, CoursesCategoryEnum.IT));
            university.Courses.Add(new Course("055", "Software engineering", 2, CoursesCategoryEnum.IT));


            //Students


            university.Students.Add(new Student("Sloane", "Shannon", 19, 100));
            university.Students.Add(new Student("Tanya", "Chandler", 22, 101));
            university.Students.Add(new Student("Sean", "Pacheco", 21, 102));
            university.Students.Add(new Student("Leila", "Jefferson", 18, 103));
            university.Students.Add(new Student("Fulton", "Davis", 20, 104));
            university.Students.Add(new Student("Oren", "Mercado", 21, 105));
            university.Students.Add(new Student("Veda", "Clark", 31, 106));
            university.Students.Add(new Student("Maggie", "Mcconnell", 19, 107));
            university.Students.Add(new Student("Kenneth", "Robertson", 20, 108));
            university.Students.Add(new Student("Angela", "Salas", 18, 109));
            university.Students.Add(new Student("Ocean", "Kelly", 21, 110));
            university.Students.Add(new Student("Holmes", "Haley", 19, 110));
            university.Students.Add(new Student("Brittany", "Lang", 22, 111));
            university.Students.Add(new Student("Timothy", "Calhoun", 21, 112));
            university.Students.Add(new Student("Carlos", "Ashley", 18, 113));
            university.Students.Add(new Student("Ivan", "Bean", 20, 114));
            university.Students.Add(new Student("Jerry", "Quinn", 21, 115));
            university.Students.Add(new Student("Wylie", "Benton", 31, 116));
            university.Students.Add(new Student("Gemma", "Hobbs", 19, 117));
            university.Students.Add(new Student("Timothy", "Benton", 20, 118));
            university.Students.Add(new Student("Latifah", "Weiss", 18, 119));
            university.Students.Add(new Student("Dana", "Mayer", 21, 120));

            //Professors
            //Instructor , Assistant Professor ,Associate Professor,Professor
            university.Professors.Add(new Professor("Todd", "Holt", 30, "Instructor"));
            university.Professors.Add(new Professor("Sydney", "Tran", 35, "Assistant Professor"));
            university.Professors.Add(new Professor("Zoe", "Mccarthy", 38, "Associate Professor"));
            university.Professors.Add(new Professor("Susan", "Moreno", 40, "Professor"));

            university.Professors.Add(new Professor("Shellie", "Stone", 33, "Instructor"));
            university.Professors.Add(new Professor("Teegan", "Huber", 31, "Assistant Professor"));
            university.Professors.Add(new Professor("Jacob", "Chase", 45, "Associate Professor"));
            university.Professors.Add(new Professor("Isadora", "Head", 50, "Professor"));

            university.Professors.Add(new Professor("George", "Parker", 39, "Instructor"));
            university.Professors.Add(new Professor("Lucas", "Moreno", 49, "Assistant Professor"));
            university.Professors.Add(new Professor("Stephen", "Bowman", 48, "Associate Professor"));
            university.Professors.Add(new Professor("Bernard", "Green", 40, "Professor"));

            university.Professors.Add(new Professor("Karen", "Moody", 29, "Instructor"));
            university.Professors.Add(new Professor("Gabriel", "Ferrell", 35, "Assistant Professor"));
            university.Professors.Add(new Professor("Medge", "Huff", 47, "Associate Professor"));
            university.Professors.Add(new Professor("Benjamin", "Douglas", 60, "Professor"));

            university.Professors.Add(new Professor("Emmanuel", "Mitchell", 29, "Instructor"));
            university.Professors.Add(new Professor("Shellie", "Castillo", 35, "Assistant Professor"));
            university.Professors.Add(new Professor("Erin", "Walker", 48, "Associate Professor"));
            university.Professors.Add(new Professor("Dennis", "Golden", 50, "Professor"));

        }
    }
}
