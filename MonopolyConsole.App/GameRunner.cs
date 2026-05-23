using MonopolyConsole.App.Prompters;
using MonopolyConsole.Core.Interfaces;
using MonopolyConsole.Core.Models;
using MonopolyConsole.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.App
{
    // 
    internal class GameRunner
    {
        //load game, save game, 

        // ask for player number, names and setup game engine and game

        // deals with players, engine deals with game

        List<Player> Players;
        GameEngine GameEngine;
        int CurrentPlayer = 0;
        bool IsSetup = false;

        internal void SetupRunner()
        {
            var humanPrompter = new HumanConsolePrompter();

            int intResponse;
            int noPlayers;

            // How many players, names
            do
            {
                // Do while players between 2 and 8 and no errors
                Console.Write("How many players are in the game => ");
                string input = Console.ReadLine();

                // Check if it's a valid integer AND within the range (2 to 8)
                if (!int.TryParse(input, out intResponse) || intResponse < 2 || intResponse > 8)
                {
                    Console.WriteLine("Invalid input! Please enter a whole number between 2 and 8.\n");
                }

            } while (intResponse < 2 || intResponse > 8) ;

            // Initialize the list after valid input is captured
            Players = new List<Player>();
            noPlayers = intResponse;

            // How many bots - none for now
            do
            {
                Console.Write("How many bots are in the game => ");
                string input = Console.ReadLine();

                // Check if it's a valid integer AND within the range (0 to noPlayers)
                if (!int.TryParse(input, out intResponse) || intResponse < 0 || intResponse > noPlayers)
                {
                    Console.WriteLine($"\nInvalid input! Please enter a whole number between 0 and {noPlayers}.");
                }

            } while (intResponse < 0 || intResponse > noPlayers);

            // Introduce themselves, if bots, then bots get names too later in different loop
            //- need to differentiate bot name loop
            for (int i = 0; i < noPlayers; i++) 
            {
                // ask name
                Console.WriteLine($"Player {i + 1}, What is your name => ");
                var player = new Player();
                player.Name = Console.ReadLine();
                Players.Add(player);
            }
            //- from here it clears screen and then show GUI.

            GameEngine = new GameEngine(Players, new HumanConsolePrompter(), new DiceRoller());
            GameEngine.SetupGame();
            IsSetup = true;
        }

        // acknowledge players, loop turns
        public void Run()
        {
            if (!IsSetup)
            {
                SetupRunner();
            }


            // Acknowledge players
            for (int i = 0; i < Players.Count; i++)
            {
                //- for a distributed game, the bots name themselves and it just shows player 4 x(bot) enteres the game
                //  welcome player 7 y(bot), then for people it just shows their names - welcome player 3 x
                Console.WriteLine($"Welcome Player {i + 1}, {Players[i].Name}");
            }
            Console.WriteLine();

            while (!GameEngine.GameEnded)
            {
                for (int i = 0; i < Players.Count;i++)
                {
                    GameEngine.ProcessTurn(Players[i]);
                    Console.WriteLine("Press Enter to move on");
                    Console.ReadLine();
                }
            }
        }
    }
}
