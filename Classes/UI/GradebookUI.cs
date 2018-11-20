using System;

namespace gradebookprogram.Classes.UI
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
            {
                SaveCommand();
            }
             else if (command.StartsWith("addassignment"))
             {
                AddAssignmentCommand(command);
             }
            else if (command.StartsWith("removeassignment"))
            {
                RemoveAssignmentCommand(command);
            }
            else if (command.StartsWith("addgrade"))
            {
                AddGradeCommand(command);
            }
            else if (command.StartsWith("removegrade"))
            {
                RemoveGradeCommand(command);
            }
            else if (command.StartsWith("addstudent"))
            {
                AddStudentCommand(command);
            }
            else if (command.StartsWith("removestudent"))
            {
                RemoveStudentCommand(command);
            }
            else if (command == "listassignments")
            {
                ListAssignmentsCommand();
            }
            else if (command == "liststudents")
            {
                ListStudentsCommand();
            }
            else if (command.StartsWith( "averagegrade"))
            {
                AverageGradeCommand(command);
            }
            else if (command == "help")
            {
                HelpCommand();
            }
            else if (command == "close")
            {
                Close = true;
            }
            else
            {
                Console.WriteLine("{0} was not recognized, please try again.", command);
            }
        }

        //enumerates the Assignments list in Gradebook
        public static void ListAssignmentsCommand()
        {
            Gradebook.ListAssignments();
        }
        //enumerates the Students list in Gradebook
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
            Console.WriteLine("AddAssignment 'Name' 'Points''Description''Weight' - Adds a new Assignment to Gradebook. Weight is by percentage (ex. 100 is 100%)");
            Console.WriteLine();
            Console.WriteLine("RemoveAssignment 'Name' - Removes an Assignment from the Gradebook with the matching name.");
            Console.WriteLine();
            Console.WriteLine("AddStudentScore 'Name' 'Assignment' 'Score' - Adds a new grade to a student with the matching name, assignment name, and score.");
            Console.WriteLine();
            Console.WriteLine("RemoveStudentScore 'Name' 'Assignment' - Removes a grade to a student with the matching name and assignment.");
            Console.WriteLine();
            Console.WriteLine("RemoveStudent 'Name' - Removes the student with the provided name.");
            Console.WriteLine();
            Console.WriteLine("AverageGrade 'Name' - Calculates average grade for the student with the provided name.");
            Console.WriteLine();
            Console.WriteLine("Close - Closes the gradebook and takes you back to the starting command options.");
            Console.WriteLine();
            Console.WriteLine("Save - Saves the gradebook to the hard drive for later use.");
        }

        //calculates average grade of student
        public static void AverageGradeCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 2)
            {
                Console.WriteLine("Invalid Command, AverageGrade requires a student name.");
                return;
            }
            var name = parts[1];
            Gradebook.GetAverage(name);
        }
        //adds grade to assignment dictionary in student object
        public static void AddGradeCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 4)
            {
                Console.WriteLine("Invalid Command, AddGrade requires a name, assignment name, and score.");
                return;
            }
            var name = parts[1];
            var assignment = parts[2];
            var score = Double.Parse(parts[3]);
            Gradebook.AddGrade(name, assignment, score);
            Console.WriteLine("Added a score of {0} on {1}, to {2}'s grades", score,assignment, name);
        }

        //removes grade from assignment dictionary in student object
        public static void RemoveGradeCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 3)
            {
                Console.WriteLine("Invalid Command, RemoveGrade requires a name and assigment name.");
                return;
            }
            var name = parts[1];
            var assignment = parts[2];
            Gradebook.RemoveGrade(name, assignment);
            Console.WriteLine("Removed {0} assignment from {1}'s grades", assignment, name);
        }

        //runs Gradebook.Save() method
        public static void SaveCommand()
        {
            Gradebook.Save();
            Console.WriteLine("{0} has been saved.", Gradebook.ClassName);
        }
        
        //adds assignment to assignment list in Gradebook object
        public static void AddAssignmentCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 5)
            {
                Console.WriteLine("Invalid command, AddAssignment requires an assignment name, points, descripion, and weight.");
                return;
            }
            string assignmentName = parts[1];
            int assignmentPoints = Int32.Parse(parts[2]);
            string assignmentDescription = parts[3];
            int assignmentWeight = Int32.Parse(parts[4]);
            Assignment assignment = new Assignment(assignmentName, assignmentWeight, assignmentPoints, assignmentDescription);
            Gradebook.AddAssignment(assignment);
            Console.WriteLine("Added {0} assignment to the gradebook.", assignmentName);   
        }

        //adds student to student list in Gradebook object
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

        //removes student from student list in Gradebook object
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

        //removes assignment from assignment list in gradebook object
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