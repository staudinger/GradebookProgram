using System;
using System.Collections.Generic;
using System.Linq;

namespace gradebookprogram.Classes
{
    public class Assignment
    {
        public string Name{get;set;}
        public int Weight{get;set;}

        public int Points{get;set;}

        public string Description{get;set;}
    

    public Assignment(string name, int weight, int points, string description)
        {
           Name = name;
           Points = points;
           Description = description;
           Weight = weight;

        }
    }
}