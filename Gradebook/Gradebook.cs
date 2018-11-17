using System;
using System.Collections.Generic;
using System.Linq;

namespace gradebookprogram.Gradebook
{
    public class Gradebook
    {
        public string ClassName {get;set;}

        public List<Student> Students {get;set;}

        public Gradebook(string className)
        {   
            ClassName = className;
            Students = new List<Student>();
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        public void RemoveStudent(string name)
        {
            var student = Students.FirstOrDefault(e=> e.Name == name);
            Students.Remove(student);
        }
    }
}
