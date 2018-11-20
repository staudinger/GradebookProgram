using System;
using System.Collections.Generic;
using System.Linq;
//dotnet add package Newtonsoft.Json --version 11.0.2	
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace gradebookprogram.Classes

{
    public class Gradebook
    {
        public string ClassName {get;set;}
        public int Period{get;set;}

        public List<Student> Students {get;set;}
        public List<Assignment> Assignments {get;set;}
        public Gradebook(string className, int period)
        {   
            ClassName = className;
            Period = period;
            Students = new List<Student>();
            Assignments = new List<Assignment>();
        }


        public void AddAssignment(Assignment assignment)
        {
            Assignments.Add(assignment);
        }

        public void RemoveAssignment(string name)
        {
            var assignment = Assignments.FirstOrDefault(e=> e.Name == name);
            Assignments.Remove(assignment);
        }

        public void GetAverage(string name)
        {
            var student = Students.FirstOrDefault(e=> e.Name == name);
            Console.WriteLine(name + "'s total average grade is " + student.AverageGrade());
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


        public void Save()
        {
            using (var newFile = new FileStream(ClassName + ".grdbk", FileMode.Create, FileAccess.Write))
            {
                using(var writer = new StreamWriter(newFile))
                {
                    var json = JsonConvert.SerializeObject(this);
                 writer.Write(json);
                }
            }
        }
        public void AddGrade(string name, string assignmentName, double gradeScore)
        {
            var student = Students.FirstOrDefault(e=>e.Name == name);
            if (student == null)
            {
                Console.WriteLine("student {0} was not found, try again.", name);
                return;
            }
            student.AddAssignment(assignmentName, gradeScore);
        }
        public void RemoveGrade(string studentName, string assignmentName)
        {
            if (string.IsNullOrEmpty(studentName))
                throw new ArgumentException("A Name is required to remove a grade from a student.");
            var student = Students.FirstOrDefault(e => e.Name == studentName);
            if (student == null)
            {
                Console.WriteLine("student {0} was not found, try again.", studentName);
                return;
            }
            var assignment = Assignments.FirstOrDefault(e=>e.Name == assignmentName);
            student.RemoveAssignment(assignment.Name);
        }

        public void ListStudents()
        {
            foreach (var student in Students)
            {
                Console.WriteLine("{0} : {1} : {2}", student.Name, student.Year, student.Period);
            }
        }
        public void ListAssignments()
        {
            foreach (var assignment in Assignments)
            {
                Console.WriteLine("{0} : {1} : {2}", assignment.Name, assignment.Points, assignment.Weight, assignment.Description);
            }
        }

        public static Gradebook Load(string name)
        {
            if (!File.Exists(name + ".grdbk"))
            {
                Console.WriteLine("Gradebook does not exist.");
                return null;
            }

            using (var file = new FileStream(name + ".grdbk", FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(file))
                {
                    var json = reader.ReadToEnd();
                    Gradebook gradebook = JsonConvert.DeserializeObject<Gradebook>(json);
                    return gradebook;
                }
            }
        }
    }
}
