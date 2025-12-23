using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Core.Services
{
    public class DiceRoller
    {
        Random _random = new Random();

        int Val1;
        int Val2;
        public int Total { get => Val1 + Val2; }
        bool IsDoubles { get => Val1 == Val2; }

        public DiceRoller() { }

        public int Roll()
        {
            Val1 = _random.Next(1,6);
            Val2 = _random.Next(1,6);
            return Total;
        }
    }
}
