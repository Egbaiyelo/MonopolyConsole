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
            CardFactory cardFactory = new CardFactory();

            //- Colors
            //- station tile also action tile cause can transfer with train pass card from comm chest
            //- but station gives free card
            Tiles = new Tile[40];

            Tiles[0] = new EdgeTile("Go", (Player p, Game g) => { });

            //Brown
            Tiles[1] = new PropertyTile(new Property("Mediterranean Avenue", 60, 6));
            Tiles[2] = new ActionTile(CardType.CommunityChest);
            Tiles[3] = new PropertyTile(new Property("Baltic avenue", 60, 6));
            Tiles[4] = new TaxTile(200);
            Tiles[5] = new PropertyTile(new Property("Reading Station", 200, 20)); //Station

            //Blue
            Tiles[6] = new PropertyTile(new Property("Oriental Avenue", 100, 100));
            Tiles[7] = new ActionTile(CardType.Chance);
            Tiles[8] = new PropertyTile(new Property("Vermont Avenue", 100, 10));
            Tiles[9] = new PropertyTile(new Property("Conneticut Avenue", 120, 12));

            Tiles[10] = new EdgeTile("Jail", (Player p, Game g) => { });

            //Purple
            Tiles[11] = new PropertyTile(new Property("St. Charles Place", 140, 14));
            Tiles[12] = new PropertyTile(new Property("Electric company", 30, 30)); //Utility
            Tiles[13] = new PropertyTile(new Property("States Avenue", 140, 14));
            Tiles[14] = new PropertyTile(new Property("Virginia Avenue", 160, 16));
            Tiles[15] = new PropertyTile(new Property("Pensylvania Station", 200, 20)); //Station

            //Orange
            Tiles[16] = new PropertyTile(new Property("St. James Place", 180, 18));
            Tiles[17] = new ActionTile(CardType.CommunityChest);
            Tiles[18] = new PropertyTile(new Property("Pensylvania Station", 200, 20));
            Tiles[19] = new PropertyTile(new Property("Pensylvania Station", 200, 20));
            Tiles[20] = new EdgeTile("Free Parking", (Player p, Game g) => { });

            //Red
            Tiles[21] = new PropertyTile(new Property("Kentucky Avenue", 220, 22));
            Tiles[22] = new ActionTile(CardType.Chance);
            Tiles[23] = new PropertyTile(new Property("Indiana Avenue", 220, 22));
            Tiles[24] = new PropertyTile(new Property("Illinois Avenue", 240, 24));
            Tiles[25] = new PropertyTile(new Property("B&O Station", 200, 20)); //Station

            //Yellow
            Tiles[26] = new PropertyTile(new Property("Atlantic Avenue", 260, 26));
            Tiles[27] = new PropertyTile(new Property("Ventnor Avenue", 260, 26));
            Tiles[28] = new PropertyTile(new Property("Water Works", 150, 15)); // Utility
            Tiles[29] = new PropertyTile(new Property("Marvin Gardens", 280, 28));

            Tiles[30] = new EdgeTile("Go to Jail", (Player p, Game g) => p.MoveTo(g.GetPosition("jail")) );

            //Green
            Tiles[31] = new PropertyTile(new Property("Pacific Avenue", 300, 30));
            Tiles[32] = new PropertyTile(new Property("North Carolina Avenue", 300, 30));
            Tiles[33] = new ActionTile(CardType.CommunityChest);
            Tiles[34] = new PropertyTile(new Property("Pennsylvania Avenue", 320, 32));
            Tiles[35] = new PropertyTile(new Property("Short Line Station", 200, 20)); //Station

            //DarkBlue
            Tiles[36] = new ActionTile(CardType.Chance);
            Tiles[37] = new PropertyTile(new Property("Park Place", 350, 35));
            Tiles[38] = new TaxTile(200);
            Tiles[39] = new PropertyTile(new Property("Boardwalk", 400, 40));

        }

        // Indexers
        public Tile this[int i]
        {
            get => Tiles[i];
        }
        public Tile this[string name]
        {
            get => Tiles.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public int GetPosition(string place, int pos = 0)
        {
            return place.ToLower() switch
            {
                "go" => 0,
                "jail" => 10,
                "free parking" => 20,
                "go to jail" => 30,

                // Nearest types
                "nearest station" => FindNearest(pos, TileType.Station),
                "nearest utility" => FindNearest(pos, TileType.Utility),
                "nearest chest" => FindNearest(pos, TileType.CommunityChest),
                "nearest chance" => FindNearest(pos, TileType.Chance),

                _ => -1 // Default: Not found
            };
        }

        private int FindNearest(int currentPos, TileType type)
        {
            return Tiles
                .Select((tile, index) => new { Tile = tile, Index = index }) 
                .Where(t => t.Tile.TileType == type) 
                .OrderBy(t => (t.Index - currentPos + Tiles.Length) % Tiles.Length) 
                .First().Index;     
        }
    }
}
