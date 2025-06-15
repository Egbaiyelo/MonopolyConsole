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
            Tiles = new Tile[21];

            Tiles[0] = new EdgeTile("Go", (Player p, Game g) => { });

            Tiles[1] = new PropertyTile(new Property("Mediterranean Avenue", 60, 6));
            Tiles[2] = new ActionTile(CardType.CommunityChest, cardFactory.GetCard(CardType.CommunityChest));
            Tiles[3] = new PropertyTile(new Property("Baltic avenue", 60, 6));
            Tiles[4] = new TaxTile(200);
            Tiles[5] = new PropertyTile(new Property("Reading Station", 200, 20));

            Tiles[6] = new PropertyTile(new Property("Oriental Avenue", 100, 100));
            Tiles[7] = new ActionTile(CardType.Chance, cardFactory.GetCard(CardType.Chance));
            Tiles[8] = new PropertyTile(new Property("Vermont Avenue", 100, 10));
            Tiles[9] = new PropertyTile(new Property("Conneticut Avenue", 120, 12));

            Tiles[10] = new EdgeTile("Jail", (Player p, Game g) => { });

            Tiles[11] = new PropertyTile(new Property("St. Charles Place", 140, 14));
            Tiles[12] = new PropertyTile(new Property("Holddddfuwfnwfwe", 30, 30));
            Tiles[13] = new PropertyTile(new Property("States Avenue", 140, 14));
            Tiles[14] = new PropertyTile(new Property("Virginia Avenue", 160, 16));
            Tiles[15] = new PropertyTile(new Property("Pensylvania Station", 200, 20));

            Tiles[16] = new PropertyTile(new Property("St. James Place", 180, 18));
            Tiles[17] = new ActionTile(CardType.CommunityChest, cardFactory.GetCard(CardType.CommunityChest));
            Tiles[18] = new PropertyTile(new Property("Pensylvania Station", 200, 20));
            Tiles[19] = new PropertyTile(new Property("Pensylvania Station", 200, 20));
            Tiles[20] = new EdgeTile("Free Parking", (Player p, Game g) => { });

        }

    }
}
