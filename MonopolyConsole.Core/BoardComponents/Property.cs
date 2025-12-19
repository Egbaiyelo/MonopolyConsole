using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonopolyConsole.Core.Models;

namespace MonopolyConsole.Core.BoardComponents
{
    public class Property
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int BaseRent { get; set; }

        private int _rent;
        public int Rent 
        {
            get => BaseRent + (int)(Houses * (Price/10));
            set => _rent = value;
        }

        public int Houses;

        public Player? Owner;

        public Tile.TileGroup Group;


        public Property(string name, int price, int baseRent, Tile.TileGroup group)
        {
            Name = name;
            Price = price;
            BaseRent = baseRent;
            Group = group;

            Houses = 0;
            Owner = null;
        }
    }
}
