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
                Console.WriteLine("What would you like to do in " + Gradebook.ClassName + "? Enter 'Help' for options.");
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
            else if (command.StartsWith("addstudentscore"))
                AddStudentScoreCommand(command);
            else if (command.StartsWith("removestudentscore"))
                RemoveStudentScoreCommand(command);
            else if (command.StartsWith("add"))
                AddStudentCommand(command);
            else if (command.StartsWith("remove"))
                RemoveStudentCommand(command);
            else if (command == "listassignments")
                ListAssignmentsCommand();
            else if (command == "liststudents")
                ListStudentsCommand();
            else if (command == "help")
                HelpCommand();
            else if (command == "close")
                Close = true;
            else
                Console.WriteLine("{0} was not recognized, please try again.", command);
        }

        public static void ListAssignmentsCommand()
        {
            Gradebook.ListAssignments();
        }
        public static void ListStudentsCommand()
        {
            Gradebook.ListStudents();
        }

        public static void HelpCommand()
        {
            Console.WriteLine("While a gradebook is open you can use the following commands:");
            Console.WriteLine();
            Console.WriteLine("AddStudent 'Name' 'Year' 'Period' 'Honors' - Adds a new student to the gradebook with the provided name, class period, year of student (ex. 9th), 'Honors' is whether or not in Honors(true or false).");
            Console.WriteLine();
            Console.WriteLine("ListAssignments - Lists all assignments.");
            Console.WriteLine();
            Console.WriteLine("ListStudents - Lists all students.");
            Console.WriteLine();
            Console.WriteLine("AddAssignment 'Name' 'Weight' 'Points' 'Description' - Adds a new Assignment to Gradebook. Weight is by percentage (ex. 100 is 100%)");
            Console.WriteLine();
            Console.WriteLine("RemoveAssignment 'Name' - Removes an Assignment from the Gradebook with the matching name.");
            Console.WriteLine();
            Console.WriteLine("AddStudentScore 'Name' 'Assignment' 'Score' - Adds a new grade to a student with the matching name, assignment name, and score.");
            Console.WriteLine();
            Console.WriteLine("RemoveStudentScore 'Name' 'Assignment' - Removes a grade to a student with the matching name and assignment.");
            Console.WriteLine();
            Console.WriteLine("RemoveStudent 'Name' - Removes the student with the provided name.");
            Console.WriteLine();
            Console.WriteLine("Close - closes the gradebook and takes you back to the starting command options.");
            Console.WriteLine();
            Console.WriteLine("Save - saves the gradebook to the hard drive for later use.");
        }
        public static void AddStudentScoreCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 4)
            {
                Console.WriteLine("Invalid Command, AddStudentScore requires a name, assignment name, and score.");
                return;
            }
            var name = parts[1];
            var assignment = parts[2];
            var score = Double.Parse(parts[3]);
            Gradebook.AddStudentScore(name, assignment, score);
            Console.WriteLine("Added a score of {0} on, {1} to {2}'s grades", score,assignment, name);
        }
        public static void RemoveStudentScoreCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 3)
            {
                Console.WriteLine("Command not valid, RemoveGrade requires a name and assigment name.");
                return;
            }
            var name = parts[1];
            var assignment = parts[2];
            Gradebook.RemoveStudentScore(name, assignment);
            Console.WriteLine("Removed {0} assignment from {1}'s grades", assignment, name);
        }


        public static void SaveCommand()
        {
            Gradebook.Save();
            Console.WriteLine("{0} has been saved.", Gradebook.ClassName);
        }
        
        public static void AddAssignmentCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 5)
            {
                Console.WriteLine("Command not valid, AddAssignment requires an assignment name, weight, points, and description.");
                return;
            }
            string assignmentName = parts[1];
            int assignmentWeight = Int32.Parse(parts[2]);
            int assignmentPoints = Int32.Parse(parts[3]);
            string assignmentDescription = parts[4];
            Assignment assignment = new Assignment(assignmentName, assignmentWeight, assignmentPoints, assignmentDescription);
            Gradebook.AddAssignment(assignment);
            Console.WriteLine("Added {0} assignment to the gradebook.", assignmentName);   
        }

        public static void AddStudentCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 5)
            {
                Console.WriteLine("Invalid command, Add requires a name, Year, period, and if honors (true/false).");
                return;
            }
            var studentName = parts[1];

            var year = parts[2];
            var period = Int32.Parse(parts[3]);
            bool honors = Convert.ToBoolean(parts[4]);

            var student = new Student(studentName, honors, year, period);
            Gradebook.AddStudent(student);
            Console.WriteLine("Added {0} to the gradebook.", studentName);
        }
        public static void RemoveStudentCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 2)
            {
                Console.WriteLine("Invalid command, Remove requires a name.");
                return;
            }
            var name = parts[1];
            Gradebook.RemoveStudent(name);
            Console.WriteLine("{0} removed from the gradebook.", name);
        }

        public static void RemoveAssignmentCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 2)
            {
                Console.WriteLine("Invalid command, Remove requires a name.");
                return;
            }
            var name = parts[1];
            Gradebook.RemoveAssignment(name);
            Console.WriteLine("{0} assignment removed from the gradebook.", name);
        }
        
    }
}