using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole
{
    //-rename to deed maybe
    public enum PropertyGroup { Brown, LightBlue, Purple, Orange, Red, Yellow, Green, DarkBlue };
    internal class Property
    {
        public string Name { get; set; }
        //public int Value { get; set; }
        public int Rent;
        public int Cost;
        public PropertyGroup PropertyGroup;
        public Player? Owner;
        public Property(string name, int cost, int rent, PropertyGroup group = PropertyGroup.Brown)
        {
            Name = name;
            Rent = rent;
            Cost = cost;
            PropertyGroup = group;
        }

        public virtual int CalculateRent()
        {
            return Rent;
        }

        //- forgot why I called game ...(Game game)
        public virtual int CalculateCost()
        {
            //-
            //return game.Board[Name].CalculateRent();
            return Cost;
        }
    }

}
