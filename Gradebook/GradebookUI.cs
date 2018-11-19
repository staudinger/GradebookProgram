using System;

namespace gradebookprogram.Gradebook
{
    public static class GradebookUI
    {
        public static Gradebook Gradebook;

        public static bool Close = false;

        public static void CommandPrompt(Gradebook gradebook)
        {
            Gradebook = gradebook;
            Close = false;

            Console.WriteLine("#=======================#");
            Console.WriteLine(Gradebook.ClassName);
            Console.WriteLine("#=======================#");
            Console.WriteLine(string.Empty);

            while(Close == false)
            {
                Console.WriteLine("What would you like to do? Enter 'Help' for options.");
                var command = Console.ReadLine().ToLower();
                CommandRouter(command);
            }


        }

        public static void CommandRouter(string command)
        {
            if (command == "save")
                SaveCommand();
            else if (command.StartsWith("addassignment"))
                AddAssignmentCommand(command);
            else if (command.StartsWith("removeassignment"))
                RemoveAssignmentCommand(command);
            else if (command.StartsWith("add"))
                AddStudentCommand(command);
            else if (command.StartsWith("remove"))
                RemoveStudentCommand(command);
            else if (command == "list")
                ListCommand();
            else if (command == "help")
                HelpCommand();
            else if (command == "close")
                Close = true;
            else
                Console.WriteLine("{0} was not recognized, please try again.", command);
        }

        public static void HelpCommand()
        {
            Console.WriteLine("While a gradebook is open you can use the following commands:");
            Console.WriteLine();
            Console.WriteLine("Add 'Name' 'Student Type' 'Enrollment Type' - Adds a new student to the gradebook with the provided name, type of student, and type of enrollment.");
            Console.WriteLine();
            Console.WriteLine("List - Lists all students.");
            Console.WriteLine();
            Console.WriteLine("AddGrade 'Name' 'Score' - Adds a new grade to a student with the matching name of the provided score.");
            Console.WriteLine();
            Console.WriteLine("RemoveGrade 'Name' 'Score' - Removes a grade to a student with the matching name and score.");
            Console.WriteLine();
            Console.WriteLine("Remove 'Name' - Removes the student with the provided name.");
            Console.WriteLine();
            Console.WriteLine("Close - closes the gradebook and takes you back to the starting command options.");
            Console.WriteLine();
            Console.WriteLine("Save - saves the gradebook to the hard drive for later use.");
    }
}