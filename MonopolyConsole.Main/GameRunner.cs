using MonopolyConsole.App.Prompters;
using MonopolyConsole.Core.Interfaces;
using MonopolyConsole.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.App
{
    internal class GameRunner
    {
        //load game, save game, 

        // ask for player number, names and setup game engine and game

        // deals with players, engine deals with game

        Player[] Players;
        IPrompter HumanPrompter;
        IPrompter BotPrompter;

        internal void StartGame()
        {
            int intResponse;

            // How many players, names
            Console.WriteLine("How many players are in the game => ");
            intResponse = int.Parse(Console.ReadLine());
            Players = new Player[intResponse];

            // How many bots - none for now
            //Console.WriteLine("How many bots are in the game => ");
            //intResponse = int.Parse(Console.ReadLine());
            //Players = new Player[intResponse];

            HumanPrompter = new HumanConsolePrompter();

            // Introduce themselves, if bots, then bots get names too later in different loop
            for (int i = 0; i < Players.Length; i++) 
            {
                // ask name
                Console.WriteLine($"Player {i}, What is your name => ");
                Players[i] = new Player();
                Players[i].Name = 
            }
        }

        // acknowledge players, loop turns
        void run()
        {

        }
    }
}
