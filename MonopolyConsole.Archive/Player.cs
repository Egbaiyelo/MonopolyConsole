using MonopolyConsole.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole
{
    internal abstract class Participant
    {
        public Game Game;
        public string Name;

        protected int balance;
        public int Balance { get; set; }
        public List<Property> Properties = new();
        public int Position = 0;

        internal int StationsOwned;
        internal int UtilitiesOwned;

        public bool InJail = false;

        public abstract void Play();
        public abstract void Move(int steps);
        public abstract void MoveTo(int pos);
        public override string ToString() => Name;
    }


    internal class Player : Participant
    {
        //internal Game Game;

        //public string Name;
        //private int balance;
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
        private int Position = 0;
        public List<Property> Properties = new List<Property>();

        internal int StationsOwned;
        internal int UtilitiesOwned;

        public bool InJail = false;
        
        public Player(Game game, string name, int startingBalance)
        {
            Game = game;
            Name = name;
            Balance = startingBalance;
        }

        public override string ToString()
        {
            return Name;
        }

        public override void Move(int tiles)
        {
            // Cash from Go
            int toTile = Position + tiles;
            if (toTile >= Game.Board.Tiles.Length) { Balance += 200; }

            Position = (toTile) % Game.Board.Tiles.Length;
            if (Position < 0)
                Position += Game.Board.Tiles.Length; // backwards wrap
            Game.Board.Tiles[Position].LandOn(this, Game);
        }

        public override void MoveTo(int posMove)
        {
            Position = posMove % Game.Board.Tiles.Length;
            Game.Board.Tiles[Position].LandOn(this, Game);
        }

        // Roll
        // Trade, Mortgage, unmortgage, quit, sendmoney
        // Play ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        /// <summary>
        /// Presents a menu of actions for the player during their turn.
        /// Actions: Trade [placeholder], liquidate, Unmortgage, 
        /// Send Money [placeholder], Quit, and End Turn.
        /// </summary>
        public override void Play()
        {
            // Loop until the player chooses to end their turn or quit the game.
            while (true)
            {
                Console.WriteLine($"Balance: {Balance}, Net Worth: {NetWorth}");
                Console.WriteLine("Choose an action:");

                List<string> actions = new List<string>()
                {
                    //- trim options
                    "Trade",            
                    "Liquidate Assets",         
                    "Unmortgage",       
                    "Send Money",       
                    "Quit (Forfeit)",   
                    "End Turn"
                    //"Info"
                };

                ConsoleQuery query = new ConsoleQuery("Enter the number of the action you want", actions);
                int input = query.RunQuery();

                switch (input)
                {
                    case 0:
                        //Trade();
                        break;
                    case 1:
                        Liquidate();
                        break;
                    case 2:
                        //Unmortgage();
                        break;
                    case 3:
                        //SendMoney();
                        break;
                    case 4:
                        Quit(Game);
                        return; 
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void Unmortgage()
        {
            var mortgagedAssets = Properties.Where(p => p.Mortgaged).ToList();

            if (mortgagedAssets.Count == 0)
            {
                Console.WriteLine("You have no mortgaged properties.");
                return;
            }

            List<string> options = new List<string>();
            foreach (var asset in mortgagedAssets)
            {
                options.Add($"{asset.Name} [Mortgage Value: {asset.CalculateCost() / 2}]");
            }

            ConsoleQuery unmortgageQuery = new ConsoleQuery("Select a property to unmortgage", options, multiline: true);
            int choice = unmortgageQuery.RunQuery();

            Property selectedAsset = mortgagedAssets[choice - 1];
            int unmortgageCost = selectedAsset.CalculateCost() / 2 ; 

            if (Balance >= unmortgageCost)
            {
                selectedAsset.UnMortgage();
                Console.WriteLine($"{selectedAsset.Name} is now unmortgaged.");
            }
            else
            {
                Console.WriteLine("Insufficient balance to unmortgage the property.");
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
            balance = value;
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

        /// <summary>
        /// Returns a string list of assests owned by the player
        /// </summary>
        /// <param name="cost">Whether or not to include costs</param>
        /// <param name="developments">Whether or not to include developments</param>
        /// <returns></returns>
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
