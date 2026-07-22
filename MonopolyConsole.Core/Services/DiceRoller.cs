using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Core.Services
{
    public interface IDiceRoller
    {
        DiceResult Roll();
    }

    public record DiceResult(int die1, int die2)
    {
        public int Total => die1 + die2;
        public bool IsDoubles => die1 == die2;
    }

    public class DiceRoller : IDiceRoller
    {
        public DiceRoller() { }

        private readonly Random _random = new();

        public DiceResult Roll()
        {
            return new DiceResult(_random.Next(1, 7), _random.Next(1, 7));
        }
    }
}
