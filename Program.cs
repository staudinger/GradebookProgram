using System;
using gradebookprogram.Classes.UI;

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

            Console.WriteLine("Closing GradeBook!");
            Console.Read();
        }
    }
}
