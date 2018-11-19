using System;
using gradebookprogram.Gradebook;

namespace gradebookprogram
{
    class Program
    {
        static void Main(string[] args)
        {
           Console.WriteLine("#=======================#");
            Console.WriteLine("# Welcome to GradeBook! #");
            Console.WriteLine("#=======================#");
            Console.WriteLine();

            StartUI.CommandPrompt();

            Console.WriteLine("Thank you for using GradeBook!");
            Console.WriteLine("Have a nice day!");
            Console.Read();
        }
    }
}
