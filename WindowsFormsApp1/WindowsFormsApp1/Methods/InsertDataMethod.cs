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

            university.Courses.Add(new Course("010T", "Math I", 2, CoursesCategoryEnum.Mathematics));
            university.Courses.Add(new Course("010L", "Math I LAB", 1, CoursesCategoryEnum.Mathematics));

            university.Courses.Add(new Course("011T", "Math II", 3, CoursesCategoryEnum.Mathematics));
            university.Courses.Add(new Course("011L", "Math II LAB", 1, CoursesCategoryEnum.Mathematics));

            university.Courses.Add(new Course("013T", "Algebra", 3, CoursesCategoryEnum.Mathematics));
            university.Courses.Add(new Course("013L", "Algebra LAB", 1, CoursesCategoryEnum.Mathematics));

            university.Courses.Add(new Course("014T", "Geometry", 2, CoursesCategoryEnum.Mathematics));
            university.Courses.Add(new Course("014L", "Geometry LAB", 1, CoursesCategoryEnum.Mathematics));

            university.Courses.Add(new Course("020T", "Physics II", 3, CoursesCategoryEnum.Physics));
            university.Courses.Add(new Course("020L", "Physics II LAB", 1, CoursesCategoryEnum.Physics));

            university.Courses.Add(new Course("021T", "Physics III", 3, CoursesCategoryEnum.Physics));
            university.Courses.Add(new Course("021L", "Physics III LAB", 1, CoursesCategoryEnum.Physics));

            university.Courses.Add(new Course("022T", "Quantum mechanics", 2, CoursesCategoryEnum.Physics));
            university.Courses.Add(new Course("022L", "Quantum mechanics LAB", 1, CoursesCategoryEnum.Physics));

            university.Courses.Add(new Course("031T", "Organic", 2, CoursesCategoryEnum.Chemistry));
            university.Courses.Add(new Course("031L", "Organic LAB", 1, CoursesCategoryEnum.Chemistry));

            university.Courses.Add(new Course("032T", "Inorganic", 2, CoursesCategoryEnum.Chemistry));
            university.Courses.Add(new Course("032L", "Inorganic LAB", 1, CoursesCategoryEnum.Chemistry));

            university.Courses.Add(new Course("033T", "Analytical", 2, CoursesCategoryEnum.Chemistry));
            university.Courses.Add(new Course("033L", "Analytical LAB", 1, CoursesCategoryEnum.Chemistry));

            university.Courses.Add(new Course("034T", "Algebra", 2, CoursesCategoryEnum.Chemistry));
            university.Courses.Add(new Course("034L", "Algebra LAB", 1, CoursesCategoryEnum.Chemistry));

            university.Courses.Add(new Course("035T", "Biochemistry", 2, CoursesCategoryEnum.Chemistry));
            university.Courses.Add(new Course("035L", "Biochemistry LAB", 1, CoursesCategoryEnum.Chemistry));



            university.Courses.Add(new Course("041T", "Accounting", 3, CoursesCategoryEnum.Financial));
            university.Courses.Add(new Course("041L", "Accounting LAB", 1, CoursesCategoryEnum.Financial));

            university.Courses.Add(new Course("042T", "Financial mathematics ", 1, CoursesCategoryEnum.Financial));
            university.Courses.Add(new Course("042L", "Financial mathematics LAB", 1, CoursesCategoryEnum.Financial));

            university.Courses.Add(new Course("043T", "Financial management", 2, CoursesCategoryEnum.Chemistry));
            university.Courses.Add(new Course("043L", "Financial management LAB", 1, CoursesCategoryEnum.Chemistry));

            university.Courses.Add(new Course("044T", "Agricultural", 2, CoursesCategoryEnum.Chemistry));
            university.Courses.Add(new Course("044L", "Agricultural LAB", 1, CoursesCategoryEnum.Chemistry));

            university.Courses.Add(new Course("045T", "Biochemistry", 2, CoursesCategoryEnum.Chemistry));
            university.Courses.Add(new Course("045L", "Biochemistry LAB", 1, CoursesCategoryEnum.Chemistry));


            university.Courses.Add(new Course("051T", "C Shapr progr", 3, CoursesCategoryEnum.IT));
            university.Courses.Add(new Course("051L", "C Shapr progr LAB", 1, CoursesCategoryEnum.IT));

            university.Courses.Add(new Course("052T", "Java progr", 2, CoursesCategoryEnum.IT));
            university.Courses.Add(new Course("052L", "Java progr LAB", 1, CoursesCategoryEnum.IT));

            university.Courses.Add(new Course("053T", "Php progr", 2, CoursesCategoryEnum.IT));
            university.Courses.Add(new Course("053L", "Php progr LAB", 1, CoursesCategoryEnum.IT));

            university.Courses.Add(new Course("054T", "WWW", 3, CoursesCategoryEnum.IT));
            university.Courses.Add(new Course("054L", "WWW LAB", 1, CoursesCategoryEnum.IT));

            university.Courses.Add(new Course("055T", "Software engineering", 2, CoursesCategoryEnum.IT));
            university.Courses.Add(new Course("055L", "Software engineering LAB", 1, CoursesCategoryEnum.IT));




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
            university.Students.Add(new Student("Holmes", "Haley", 19, 111));
            university.Students.Add(new Student("Brittany", "Lang", 22, 112));
            university.Students.Add(new Student("Timothy", "Calhoun", 21, 113));
            university.Students.Add(new Student("Carlos", "Ashley", 18, 114));
            university.Students.Add(new Student("Ivan", "Bean", 20, 115));
            university.Students.Add(new Student("Jerry", "Quinn", 21, 116));
            university.Students.Add(new Student("Wylie", "Benton", 31, 117));
            university.Students.Add(new Student("Gemma", "Hobbs", 19, 118));
            university.Students.Add(new Student("Timothy", "Benton", 20, 119));
            university.Students.Add(new Student("Latifah", "Weiss", 18, 120));
            university.Students.Add(new Student("Dana", "Mayer", 21, 121));

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
