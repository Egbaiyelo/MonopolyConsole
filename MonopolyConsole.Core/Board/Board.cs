using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MonopolyConsole.Core.Board.Tile;

namespace MonopolyConsole.Core.Board
{
    internal class Board
    {
        private readonly Tile[] tiles;
        private readonly int BoardSize;
        public Dictionary<string, int> Map = new Dictionary<string, int>();

        public Board(Tile[] tiles) 
        { 
            this.tiles = tiles;
            BoardSize = tiles.Length;
            for (int i = 0; i < BoardSize; i++)
            {
                Map.Add(this[i].Name, i);
            }
        }

        public Tile this[int index] { get { return tiles[index]; } }

        public Tile this[string key]
        {
            get
            {
                string normalizedKey = key.Trim().ToLower();
                return tiles.FirstOrDefault(t => t.Name.Equals(key, StringComparison.OrdinalIgnoreCase));
            }
        }

        public int GetNearest(int currentPosition, TileGroup targetGroup)
        {
            int startIndex = currentPosition + 1;

            for (int i = 0; i < BoardSize; i++)
            {
                int boardIndex = (startIndex + i) % BoardSize;

                if (tiles[boardIndex].Group == targetGroup)
                {
                    return boardIndex;
                }
            }

            return -1;
        }

        // Use map
        //public int GetIndex(string tileName)
        //{
        //    return 
        //}
    }
}
