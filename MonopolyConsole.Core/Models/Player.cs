using MonopolyConsole.Core.BoardComponents;
using MonopolyConsole.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Core.Models
{
    /// <summary>
    /// Player data class, //- maybe rename player data
    /// </summary>
    public class Player
    {
        public enum PlayerType { Human, Bot }
        public PlayerType Type { get; set; }
        public string Name;
        public int Balance;
        public int NetWorth
        {
            get
            {
                var assets = Properties.Sum(p => p.Price / 2 + 25 * p.Houses);
                return Balance + assets;
            }
        }
        public int Position;

        public int NoStationsOwned;
        public int NoUtilitiesOwned;
 
        public bool InJail = false;
        public bool IsBankrupt = false;

        public int NoJailFreeCards = 0;
        public int doubleRolls = 0;
        public int rollOutOfJailAttempts = 0;

        public List<Property> Properties;

        // Maybe move prompter here somehow, just some methods
        public Player(int balance = 1500)
        {
            Balance = balance;
            Properties = new List<Property>();

            //- why not
            Position = 0;
        }

    }
}
