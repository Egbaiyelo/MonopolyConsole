using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole
{
    internal class Player
    {
        public string Name;
        private int balance;
        public int Balance 
        {
            get { return balance; }
            set { balance = value;  } //- Bankruptcy prompt
        }
        int Position = 0;
        public List<Property> Properties = new List<Property>();

        internal int StationsOwned;
        internal int UtilitiesOwned;

        public int NetWorth
        {
            get
            {
                //-
                //int propertyValue = Properties.Sum(p => p.CalculateCost());
                //return Balance + propertyValue;
                return 1;
            }
        }


        public bool InJail = false;
        
        public Player(string name, int startingBalance)
        {
            Name = name;
            Balance = startingBalance;
        }

        public override string ToString()
        {
            return Name;
        }

        public void Move(int tiles, Game game)
        {
            // Cash from Go
            int toTile = Position + tiles;
            if (toTile >= game.Board.Tiles.Length) { Balance += 200; }

            Position = (toTile) % game.Board.Tiles.Length;
            if (Position < 0)
                Position += game.Board.Tiles.Length; // backwards wrap
            game.Board.Tiles[Position].LandOn(this, game);
        }

        public void MoveTo(int posMove, Game game)
        {
            Position = posMove % game.Board.Tiles.Length;
            game.Board.Tiles[Position].LandOn(this, game);
        }

        // Roll
        // Trade, Mortgage, unmortgage, quit, sendmoney
        // Play ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        public void Play()
        {
            string input = "";
            while (true && input != "Q")
            {

            }
        }

        public void Quit(Game game)
        {
            Console.WriteLine("Are you sure you want to quit (Y/N)");
            string input = Console.ReadLine();
            if (input == "Y")
            {
                Console.WriteLine("This action cannot be undone, do you wish to finalize forfeit (Y/N)");
                input = Console.ReadLine();
                if (input == "Y")
                    game.RemovePlayer(this, "quit");
            }
        }
    }
}
