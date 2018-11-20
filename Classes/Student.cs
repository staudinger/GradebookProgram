using System;
using System.Collections.Generic;
using System.Linq;

namespace gradebookprogram.Classes
{
    public class Student
    {
        public int Period{get;set;}
        public string Year {get; set;}
        public string Name {get;set;}
        public bool Honors {get;set;}
        public Dictionary<string,double> Assignments {get;set;}

        public Student(string name, bool honors, string year, int period)
        {
            Year = year;
            Period = period;
            Name = name;
            Honors = honors;
            Assignments = new Dictionary<string, double>();
        }

        public void AddAssignment(string assignment, double grade)
        {
            Assignments.Add(assignment, grade);
        }

        public void RemoveAssignment(string assignment)
        {
            Assignments.Remove(assignment);
        }

        public double AverageGrade()
        {
            double sum = Assignments.Sum(e => e.Value);
            return sum/Assignments.Count;
        }



    }
}