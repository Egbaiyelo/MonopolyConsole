using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole
{
    internal class Property
    {
        public string Name { get; set; }
        //public int Value { get; set; }
        public int Rent;
        public int Cost;
        public Color PropertyGroup;
        public Player? Owner;
        public Property(string name, int cost, int rent)
        {
            Name = name;
            Rent = rent;
            Cost = cost;
        }
    }
}
