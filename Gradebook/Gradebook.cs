using System;
using System.Collections.Generic;
using System.Linq;
//dotnet add package Newtonsoft.Json --version 11.0.2	
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using Gradebook.Enums;

namespace gradebookprogram.Gradebook

{
    public class Gradebook
    {
        public string ClassName {get;set;}
        public int Period{get;set;}

        public List<Student> Students {get;set;}
        public List<Assignment> Assignments {get;set;}
        public GradebookType Type{get;set;}

        public Gradebook(string className, int period)
        {   
            ClassName = className;
            Period = period;
            Students = new List<Student>();
            Assignments = new List<Assignment>();
            Type = GradebookType.Standard;
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
        public void AddStudentScore(string name, string assignmentName, double gradeScore)
        {
            var student = Students.FirstOrDefault(e=>e.Name == name);
            var assignment = Assignments.FirstOrDefault(e=>e.Name == assignmentName);
            if (student == null)
            {
                Console.WriteLine("student {0} was not found, try again.", name);
                return;
            }
            student.AddAssignment(assignment, gradeScore);
        }
        public void RemoveStudentScore(string studentName, string assignmentName)
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
            student.RemoveAssignment(assignment);
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
                Console.WriteLine("Gradebook could not be found.");
                return null;
            }

            using (var file = new FileStream(name + ".grdbk", FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(file))
                {
                    var json = reader.ReadToEnd();
                    return ConvertToGradeBook(json);
                }
            }
        }

        public static dynamic ConvertToGradeBook(string json)
        {
           Gradebook gradebook = JsonConvert.DeserializeObject<Gradebook>(json);
           return gradebook;
        }
    }
}
