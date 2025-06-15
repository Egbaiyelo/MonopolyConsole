using MonopolyConsole.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole
{
    internal class Board
    {
        public Tile[] Tiles;

        public Board()
        {
            Tiles = new Tile[40];
            Tiles[0] = new PropertyTile(new Property());

        }

    }
}
