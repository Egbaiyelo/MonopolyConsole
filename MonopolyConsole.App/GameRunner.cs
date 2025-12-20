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
        GameEngine GameEngine;
        IPrompter HumanPrompter;
        IPrompter BotPrompter;

        internal void SetupGame()
        {
            GameEngine = new GameEngine();
            HumanPrompter = new HumanConsolePrompter();

            int intResponse;

            // How many players, names
            Console.WriteLine("How many players are in the game => ");
            intResponse = int.Parse(Console.ReadLine());
            Players = new Player[intResponse];

            // How many bots - none for now
            //Console.WriteLine("How many bots are in the game => ");
            //intResponse = int.Parse(Console.ReadLine());
            //Players = new Player[intResponse];

            // Introduce themselves, if bots, then bots get names too later in different loop
            for (int i = 0; i < Players.Length; i++) 
            {
                // ask name
                Console.WriteLine($"Player {i + 1}, What is your name => ");
                Players[i] = new Player();
                Players[i].Name = Console.ReadLine();
            }

            GameEngine.SetupGame(HumanPrompter);

        }

        // acknowledge players, loop turns
        public void Run()
        {
            // Acknowledge players
            for (int i = 0; i < Players.Length; i++)
            {
                Console.WriteLine($"Welcome Player {i + 1}, {Players[i].Name}");
            }

            while (!GameEngine.GameEnded())
            {
                for (int i = 0; i < Players.Length;i++)
                {
                    GameEngine.ProcessTurn(Players[i]);
                    Console.WriteLine("Press Enter to move on");
                    Console.ReadLine();
                }
            }
        }
    }
}
