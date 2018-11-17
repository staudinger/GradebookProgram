using System;

namespace gradebookprogram.Gradebook
{
    public static class UICommands
    {
        public static bool end = false;

        public static void CommandPrompt()
        {
            while (end == false)
            {
                Console.WriteLine("Do Something");
            }
        }
    }
}
