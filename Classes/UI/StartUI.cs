using System;

namespace gradebookprogram.Classes.UI
{
    public static class StartUI
    {
        public static bool close = false;

        public static void CommandPrompt()
        {
            while (close == false)
            {
            Console.WriteLine("What would you like to do? Enter 'Help' for options.");
            var command = Console.ReadLine().ToLower();
            CommandRouter(command);
            }
        }

        public static void CommandRouter(string command)
        {
            if (command.StartsWith("newgradebook"))
            {
                NewGradebookCommand(command);
            }
            else if (command.StartsWith("open"))
            {
                OpenCommand(command);
            }
            else if (command == "help")
            {
                HelpCommand();                
            }
            else if (command == "close")
            {
                close = true;
            }
            else
            {
                Console.WriteLine("{0} was not recognized, please try again.", command);
            }
        }

            //creates new Gradebook and sends it to GradebookUI.CommandPrompt()
        public static void NewGradebookCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 3)
            {
                Console.WriteLine("Command not valid, Create requires a name, and class period.");
                return;
            }
            var className = parts[1];
            int period = Convert.ToInt32(parts[2]);
            Gradebook gradebook = new Gradebook(className, period);
            Console.WriteLine("Created gradebook {0}.", className);
            GradebookUI.CommandPrompt(gradebook);
        }

        public static void HelpCommand()
        {  
            Console.WriteLine("GradeBook accepts the following commands:");
            Console.WriteLine();
            Console.WriteLine("NewGradebook 'Name' 'Period' - Creates a new gradebook where 'Name' is the name of the gradebook.");
            Console.WriteLine();
            Console.WriteLine("Open 'Name' - Loads the gradebook with the provided 'Name'.");
            Console.WriteLine();
            Console.WriteLine("Help - Displays all commands.");
            Console.WriteLine();
            Console.WriteLine("Close - Exits the application");  
        }

        //opens gradebook by calling Gradebook.Load() and sends it to GradebookUI.CommandPrompt()
        public static void OpenCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 2)
            {
                Console.WriteLine("Invalid Command, requires a name.");
                return;
            }
            var name = parts[1];
            var gradeBook = Gradebook.Load(name);

            if (gradeBook == null)
                return;

            GradebookUI.CommandPrompt(gradeBook);
        }
    }
}
