using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CO2NET_Sample
{

    public class Student
    {
        public static List<Student> StudentList = new List<Student>() {
            new Student(1,"Mark",21),
            new Student(2,"John",16),
            new Student(3,"Kuke",18)
        };


        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Student(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }
}
