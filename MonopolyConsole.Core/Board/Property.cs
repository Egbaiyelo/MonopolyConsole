using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonopolyConsole.Core.Models;

namespace MonopolyConsole.Core.Board
{
    public class Property
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int BaseRent { get; set; }

        public int Houses;

        public Player? Owner;

        public Tile.TileType Group;
    }
}
