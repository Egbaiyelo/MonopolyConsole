using MonopolyConsole.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole
{
    internal class Player
    {
        private Game Game;

        public string Name;
        private int balance;
        public int Balance 
        {
            get { return balance; }
            set { setBalance(value);  } // Handle potential debt
        }
        public int NetWorth
        {
            get
            {
                // Helpful for bots and players too
                int propertyValue = Properties.Sum(p => p.CalculateCost());
                return Balance + propertyValue;
            }
        }
        int Position = 0;
        public List<Property> Properties = new List<Property>();

        internal int StationsOwned;
        internal int UtilitiesOwned;

        public bool InJail = false;
        
        public Player(Game game, string name, int startingBalance)
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
            int input = -1;
            while (input != 0)
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

        private void setBalance(int value)
        {
            balance += value;
            while (balance < 0)
            {
                //- player might not have property to pay back
                Console.WriteLine($"{Name} has a debt of {Math.Abs(balance)}");
                ConsoleQuery handleDebt = new ConsoleQuery(
                    "How would you like to handle your debt",
                    new List<string> {"liqudate assets", "trade", "quit"});
                int response = handleDebt.RunQuery();

                switch (response)
                {
                    case 1:
                        Liquidate();
                        break;
                    case 2:

                        break;
                    case 3:
                        Quit(Game);
                        break;
                    default:
                        setBalance(value);
                        break;
                }
            }
        }

        private async void Liquidate()
        {
            ConsoleQuery liquidate = new ConsoleQuery(
                "What asset do you wish to exchange for cash",
                ListAssets(), multiline: true);
            int response = liquidate.RunQuery();
            Property target = Properties[response - 1];

            List<string> options = new List<string>() { "once", "all developments", "till mortgage" };
            ConsoleQuery howfar = new ConsoleQuery(
                "How much do you wish to liquidate",
                options, multiline: true);
            int depth = target.Hotel || target.Houses > 0 ? 3 : howfar.RunQuery();

            switch (depth)
            {
                case 1:
                    target.DownGrade();
                    break;
                case 2:
                    target.DownGrade(true, false);
                    break;
                case 3:
                default:
                    target.DownGrade(true, true);
                    break;
            }
        }

        private List<string> ListAssets(bool cost = true, bool developments = true)
        {
            List<string> assets = new List<string>();
            string hold;
            foreach (Property asset in Properties)
            { //- padding maybe, taking into account length of longest name? put cost first?
                hold = asset.Name;
                if (developments)
                {
                    if (asset.Hotel) hold += " <Hotel>";
                    if (asset.Houses > 0) hold += $" <{asset.Houses} Houses>";
                }
                if (cost)
                {
                    hold += $" [{asset.CalculateCost()}]";
                }
                assets.Add(hold);
            }
            return assets;
        }
    }
}
