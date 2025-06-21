using MonopolyConsole.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


namespace MonopolyConsole
{
    /// <summary>
    /// Monopoly Game object that manages gameplay
    /// </summary>
    internal class Game
    {
        public List<Player> Players;
        public Property[] Properties;
        public Board Board;
        public int StartingBalance = 2500;
        public int Turn = 0;

        private BotManager BotManager;

        // Flow
        int currentPlayer;
        public Random Random = new Random();

        public Game()
        {
            int numProperties = 10;
            Players = new List<Player>();
            Properties = new Property[numProperties];
            Board = new Board();

            currentPlayer = 0;

            BotManager = new BotManager();
        }

        /// <summary>
        /// Create the game and add players
        /// </summary>
        public void SetGame()
        {
            //- maybe introduce the consolePrint here
            int numPlayers;

            // Initialize game
            Console.WriteLine("How many players");
            numPlayers = Convert.ToInt32(Console.ReadLine()); //- handle errors

            // Create Players
            for (int i = 0; i < numPlayers; i++)
            {
                Players.Add(CreatePlayer(i));
            }
            RoleCall();
        }

        public Player CreatePlayer(int index)
        {
            Console.WriteLine("What is the name of Player {0}", index + 1);
            string name = Console.ReadLine();

            return new Player(this, name, StartingBalance); //- handle ref starting balance (uninit)
        }

        /// <summary>
        /// Remove player from game and update Win condition
        /// </summary>
        /// <param name="player">Player to remove</param>
        /// <param name="reason">Explanation like quit</param>
        public void RemovePlayer(Player player, string reason)
        {
            Console.WriteLine($"{player.Name} is out of the game because they {reason}");
            Players.Remove(player);

            if (Players.Count == 1)
            {
                Console.WriteLine($"{Players[0].Name} has won the game");
            } else RoleCall();
        }

        /// <summary>
        /// Says which players are in the game
        /// </summary>
        public void RoleCall()
        {
            Console.Write("Players ");
            foreach (Player p in Players)
            {
                Console.Write(p.Name + ", ");
            }
            Console.WriteLine("\b\b are in the game");
        }

        public void Start()
        {
            Console.WriteLine("Starting Game ...");
            for (int i = 0; i < 40; i++) //- while noone won
            {
                for (int j = 0; j < Players.Count; j++)
                {
                    Console.WriteLine($"{Players[j].Name}'s turn to play"); //- remove s if end in s
                    int diceRoll = RollDice(2);
                    Console.WriteLine($"{Players[j].Name} rolled a {diceRoll}");
                    Players[j].Move(diceRoll, this);
                    Players[j].Play();
                    Console.ReadLine();
                }
                Turn += 1;
            }
        }

        /// <summary>
        /// 
        /// Examples: Jail, nearest -> station, utility
        /// </summary>
        /// <param name="place"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int GetPosition(string place, int pos = 0) => Board.GetPosition(place, pos);

        public int RollDice(int numDice = 2)
        {
            //- for now
            //- add info methods to all
            return Random.Next(1, numDice * 6 + 1);
        }

        public List<Property> GetAllGameProperties()
        {
            var properties = Board.Tiles
                                .OfType<PropertyTile>()
                                .Select(pt => pt.Property)
                                .ToList();
            return properties;
        }

    }

    // Cool chances, roll 1 dice, 2, 3
    // No taxes or fines from friends for next one - how to invoke something next turn?
    // No hotels/houses for next one
}
