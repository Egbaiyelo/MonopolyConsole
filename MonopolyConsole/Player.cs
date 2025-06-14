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
        public int Balance;
        //public int NetWorth
        internal int Position;
        
        public Player(string name, int startingBalance)
        {
            Name = name;
            Balance = startingBalance;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
