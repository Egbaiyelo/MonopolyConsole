using MonopolyConsole.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MonopolyConsole.Core.BoardComponents.Tile;

namespace MonopolyConsole.Core.BoardComponents
{
    public class Board
    {
        private readonly Tile[] tiles;
        public readonly int BoardSize;

        // Cards
        public Queue<Card> ChanceDeck;
        public Queue<Card> CommunityChestDeck;

        public Dictionary<string, int> Map = new Dictionary<string, int>();

        public Board(Tile[] tiles, List<Card> chances, List<Card> communityChests) 
        { 
            this.tiles = tiles;
            BoardSize = tiles.Length;

            // Cards
            var shuffledChances = chances.OrderBy(x => Random.Shared.Next()).ToList();
            ChanceDeck = new Queue<Card>(shuffledChances);

            var shuffledCommunityChests = communityChests.OrderBy(x => Random.Shared.Next()).ToList();
            CommunityChestDeck = new Queue<Card>(shuffledCommunityChests);


            //for (int i = 0; i < BoardSize; i++)
            //{
            //    // Handle same key
            //    //Map.Add(this[i].Name, i);
            //}
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
