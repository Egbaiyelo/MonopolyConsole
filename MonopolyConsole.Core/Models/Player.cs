using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Core.Models
{
    public class Player
    {
        public enum PlayerType { Human, Bot }

        public PlayerType Type { get; set; }
        public string Name;
        public int Balance;
        public int NetWorth;
        public int Position;

        public int NoStationsOwned;
        public int NoUtilitiesOwned;
 
        public bool InJail = false;
        public bool IsBankrupt = false;

        public int NoJailFreeCards = 0;

        public Player()
        {

        }


    }
}
