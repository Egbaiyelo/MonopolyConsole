using MonopolyConsole.Core.Interfaces;
using MonopolyConsole.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.App.Prompters
{
    internal class HumanConsolePrompter : IPrompter
    {
        // Confirm player action
        public bool Confirm(Player player, string message)
        {
            Console.WriteLine($"{player.Name}, {message} (y/n)?");
            return Console.ReadLine()?.ToLower() == "y";
        }

        public int ChooseOption(Player player, string prompt, List<string> options)
        {
            Console.WriteLine(prompt);
            foreach (string option in options)
            {
                Console.WriteLine(option);
            }

            int choice = 0;
            do
            {
                Console.WriteLine("What do you choose =>");
                string input = Console.ReadLine();
                choice = int.Parse(input);
            } while (choice < 0 || choice > options.Count());

            return choice;
        }


        // Notify player of something
        public void Notify(Player player, string message)
        {
            Console.WriteLine($"{player.Name}: {message}");
        }


    }
}
