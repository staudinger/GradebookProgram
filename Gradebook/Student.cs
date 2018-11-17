using System;
using System.Collections.Generic;
using System.Linq;

namespace gradebookprogram.Gradebook
{
    public class Student
    {
        public int Period{get;set;}
        public int Year {get; set;}
        public string Name {get;set;}

        public bool Honors {get;set;}

        public List<double> Grades {get;set;}

        public double GradeAverage
        {
            get{
                return Grades.Average();
            }
        }

        public char LetterGrade {get;set;}

        public Student(string name, bool honors, int year, int period)
        {
            Year = year;
            Period = period;
            Name = name;
            Honors = honors;
            Grades = new List<double>();
        }

        public void AddGrade(double grade)
        {
            Grades.Add(grade);
        }

        public void RemoveGrade(double grade)
        {
            Grades.Remove(grade);
        }





    }
}