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
            return 0;
        }


        // Notify player of something
        public void Notify(Player player, string message)
        {
            Console.WriteLine($"{player.Name}: {message}");
        }
    }
}
