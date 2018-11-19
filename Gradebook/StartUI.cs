using System;

namespace gradebookprogram.Gradebook
{
    public static class StartUI
    {
        public static bool close = false;

        public static void CommandPrompt()
        {
            while (close == false)
            {
            Console.WriteLine("GradeBook accepts the following commands:");
            Console.WriteLine();
            Console.WriteLine("New 'Name' 'Type' 'Weighted' - Creates a new gradebook where 'Name' is the name of the gradebook.");
            Console.WriteLine();
            Console.WriteLine("Open 'Name' - Loads the gradebook with the provided 'Name'.");
            Console.WriteLine();
            Console.WriteLine("Help - Displays all accepted commands.");
            Console.WriteLine();
            Console.WriteLine("Close - Exits the application");
            Console.WriteLine("What would you like to do?");
                var command = Console.ReadLine().ToLower();
                CommandRouter(command);
            }
        }

            public static void CommandRouter(string command)
            {
                if (command.StartsWith("new"))
                    NewCommand(command);
                else if (command.StartsWith("open"))
                    OpenCommand(command);
                else if (command == "help")
                    HelpCommand();
                else if (command == "close)
                    close = true;
                else
                    Console.WriteLine("{0} was not recognized, please try again.", command);
            }

            public static void NewCommand(string command)
            {
                var parts = command.Split(' ');
                if (parts.Length != 2)
                {
                    Console.WriteLine("Command not valid, Create requires a name, type of gradebook, if it's weighted (true / false).");
                    return;
                }
                var name = parts[1];
                Gradebook gradebook;
           
                Console.WriteLine("Created gradebook {0}.", name);
                GradebookUI.CommandPrompt(gradebook);
        }

        



    }
}
