using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole
{
    internal class Game
    {
        public List<Player> Players;
        public Property[] Properties;
        public Board Board;
        public int StartingBalance = 2000;

        // Flow
        int currentPlayer;
        public Random Random = new Random();

        public Game(int numPlayers)
        {
            int numProperties = 10;
            Players = new List<Player>();
            Properties = new Property[numProperties];
            Board = new Board();

            // Initialize game
            Console.WriteLine("What is the starting balance of this game");
            StartingBalance = Convert.ToInt32(Console.ReadLine()); //- handle errors

            // Create Players
            for (int i = 0; i < numPlayers; i++)
            {
                Players.Add(CreatePlayer(i));
            }
            RoleCall();
            Start();
        }

        public Player CreatePlayer(int index)
        {
            Console.WriteLine("What is the name of Player {0}", index + 1);
            string name = Console.ReadLine();

            return new Player(name, StartingBalance); //- handle ref starting balance (uninit)
        }

        public void RemovePlayer(Player player, string reason)
        {
            Console.WriteLine($"{player.Name} is out of the game because they {reason}");
            Players.Remove(player);

            if (Players.Count == 1)
            {
                Console.WriteLine($"{Players[0].Name} has won the game");
            } else RoleCall();
        }

        public void RoleCall()
        {
            Console.Write("Players ");
            foreach (Player p in Players)
            {
                Console.Write(p.Name + ", ");
                //- No comma for last dude
            }
            Console.WriteLine("\b\b are in the game");
        }

        public void Start()
        {
            for (int i = 0; i < 40; i++)
                for (int j = 0; j < Players.Count; j++)
                {
                    Console.WriteLine($"{Players[j]}'s turn to play"); //- remove s if end in s
                    int diceRoll = RollDice(2);
                    Players[j].Move(diceRoll, this);
                    Console.ReadLine();
                }
        }

        public int GetPosition(string place, int pos = 0)
        {
            return place.ToLower() switch
            {
                "jail" => 10,
                //- More advanced oneslike nearest station
                _ => -1
            };
        }

        public int RollDice(int numDice = 2)
        {
            //- for now
            //- add info methods to all
            return Random.Next(0, numDice * 6);
        }
    }

    // Cool chances, roll 1 dice, 2, 3
    // No taxes or fines from friends for next one - how to invoke something next turn?
    // No hotels/houses for next one
}
