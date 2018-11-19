using System;
using System.Collections.Generic;
using System.Linq;

namespace gradebookprogram.Gradebook
{
    public class Student
    {
        public int Period{get;set;}
        public string Year {get; set;}
        public string Name {get;set;}

        public bool Honors {get;set;}

        public Dictionary<Assignment,double> Assignments {get;set;}

        public char LetterGrade {get;set;}

        public Student(string name, bool honors, string year, int period)
        {
            Year = year;
            Period = period;
            Name = name;
            Honors = honors;
            Assignments = new Dictionary<Assignment, double>();
        }

        public void AddAssignment(Assignment assignment, double grade)
        {
            Assignments.Add(assignment, grade);
        }

        public void RemoveAssignment(Assignment assignment)
        {
            Assignments.Remove(assignment);
        }

        public double GradeAverage()
        {
            double sum = Assignments.Sum(e => e.Value);
            return sum/Assignments.Count;
        }



    }
}