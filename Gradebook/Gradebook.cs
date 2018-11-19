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

        public GradebookType Type{get;set;}

        public Gradebook(string className, int period)
        {   
            ClassName = className;
            Period = period;
            Students = new List<Student>();
            Type = GradebookType.Standard;
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
        public void AddAssignment(string name, Assignment assignment, double gradeScore)
        {
            var student = Students.FirstOrDefault(e=>e.Name == name);
            if (student == null)
            {
                Console.WriteLine("student {0} was not found, try again.", name);
                return;
            }
            student.AddAssignment(assignment, gradeScore);
        }
        public void RemoveAssignment(string studentName, Assignment assignment)
        {
            if (string.IsNullOrEmpty(studentName))
                throw new ArgumentException("A Name is required to remove a grade from a student.");
            var student = Students.FirstOrDefault(e => e.Name == studentName);
            if (student == null)
            {
                Console.WriteLine("student {0} was not found, try again.", studentName);
                return;
            }
            student.RemoveAssignment(assignment);
        }

        public void ListStudents()
        {
            foreach (var student in Students)
            {
                Console.WriteLine("{0} : {1} : {2}", student.Name, student.Year, student.Period);
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
            // Get GradeBookType from the GradeBook.Enums namespace
            var gradebookEnum = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                 from type in assembly.GetTypes()
                                 where type.FullName == "Gradebook.Enums.GradebookType"
                                 select type).FirstOrDefault();

            var jobject = JsonConvert.DeserializeObject<JObject>(json);
            var gradebookType = jobject.Property("Type")?.Value?.ToString();

            if ((from assembly in AppDomain.CurrentDomain.GetAssemblies()
                 from type in assembly.GetTypes()
                 where type.FullName == "Gradebook.Gradebook"
                 select type).FirstOrDefault() == null)
                 gradebookType = "Standard";
            else
            {
                if (string.IsNullOrEmpty(gradebookType))
                    gradebookType = "Standard";
                else
                    gradebookType = Enum.GetName(gradebookEnum, int.Parse(gradebookType));
            }

            // Get GradeBook from the GradeBook.GradeBooks namespace
            var gradebook = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                             from type in assembly.GetTypes()
                             where type.FullName == "Gradebook.Gradebook"
                             select type).FirstOrDefault();


            //protection code
            if (gradebook == null)
                gradebook = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                             from type in assembly.GetTypes()
                             where type.FullName == "GradeBook.Gradebook"
                             select type).FirstOrDefault();
            
            return JsonConvert.DeserializeObject(json, gradebook);
        }
    }
}
