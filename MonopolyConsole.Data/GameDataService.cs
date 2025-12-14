using MonopolyConsole.Core.Interfaces;
using MonopolyConsole.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static MonopolyConsole.Core.Models.Card;
using static MonopolyConsole.Core.Board.Tile;
using MonopolyConsole.Core.Board;

namespace MonopolyConsole.Data
{
    // Contains static property lists,
    // handles saving/loading game state from files.
    // setup data

    public  class GameDataService : IGameDataService
    {
        public GameDataService() { }


        Tile CreateActionTile(int index, string name, TileGroup group)
        {
            return new Tile(index, name, group, null); 
        }
        Tile CreateStreet(int index, string name, int price, int baseRent, TileGroup group)
        {
            var deed = new Property(name, price, baseRent, group);
            return new Tile(index, name, group, deed);
        }
        Tile CreateSpecialProperty(int index, string name, int price, int baseRent, TileGroup group)
        {
            var deed = new Property(name, price, baseRent, group);
            return new Tile(index, name, group, deed);
        }

        public List<Tile> LoadTiles()
        {
            var tiles = new List<Tile>();

            // --- Side 1 (Indices 0 - 10) ---
            tiles.Add(CreateActionTile(0, "GO", TileGroup.Go));
            tiles.Add(CreateStreet(1, "Mediterranean Avenue", 60, 2, TileGroup.BrownStreet));
            tiles.Add(CreateActionTile(2, "Community Chest", TileGroup.CommunityChest));
            tiles.Add(CreateStreet(3, "Baltic Avenue", 60, 4, TileGroup.BrownStreet));
            tiles.Add(CreateActionTile(4, "Income Tax", TileGroup.IncomeTax));
            tiles.Add(CreateSpecialProperty(5, "Reading Railroad", 200, 25, TileGroup.Station));
            tiles.Add(CreateStreet(6, "Oriental Avenue", 100, 6, TileGroup.LightBlueStreet));
            tiles.Add(CreateActionTile(7, "Chance", TileGroup.Chance));
            tiles.Add(CreateStreet(8, "Vermont Avenue", 100, 6, TileGroup.LightBlueStreet));
            tiles.Add(CreateStreet(9, "Connecticut Avenue", 120, 8, TileGroup.LightBlueStreet));
            tiles.Add(CreateActionTile(10, "Jail", TileGroup.Jail)); 

            // --- Side 2 (Indices 11 - 20) ---
            tiles.Add(CreateStreet(11, "St. Charles Place", 140, 10, TileGroup.PinkStreet));
            tiles.Add(CreateSpecialProperty(12, "Electric Company", 150, 4, TileGroup.Utility));
            tiles.Add(CreateStreet(13, "States Avenue", 140, 10, TileGroup.PinkStreet));
            tiles.Add(CreateStreet(14, "Virginia Avenue", 160, 12, TileGroup.PinkStreet));
            tiles.Add(CreateSpecialProperty(15, "Pennsylvania Railroad", 200, 25, TileGroup.Station));
            tiles.Add(CreateStreet(16, "St. James Place", 180, 14, TileGroup.OrangeStreet));
            tiles.Add(CreateActionTile(17, "Community Chest", TileGroup.CommunityChest));
            tiles.Add(CreateStreet(18, "Tennessee Avenue", 180, 14, TileGroup.OrangeStreet));
            tiles.Add(CreateStreet(19, "New York Avenue", 200, 16, TileGroup.OrangeStreet));
            tiles.Add(CreateActionTile(20, "Free Parking", TileGroup.FreeParking));

            // --- Side 3 (Indices 21 - 30) ---
            tiles.Add(CreateStreet(21, "Kentucky Avenue", 220, 18, TileGroup.RedStreet));
            tiles.Add(CreateActionTile(22, "Chance", TileGroup.Chance));
            tiles.Add(CreateStreet(23, "Indiana Avenue", 220, 18, TileGroup.RedStreet));
            tiles.Add(CreateStreet(24, "Illinois Avenue", 240, 20, TileGroup.RedStreet));
            tiles.Add(CreateSpecialProperty(25, "B. & O. Railroad", 200, 25, TileGroup.Station));
            tiles.Add(CreateStreet(26, "Atlantic Avenue", 260, 22, TileGroup.YellowStreet));
            tiles.Add(CreateStreet(27, "Ventnor Avenue", 260, 22, TileGroup.YellowStreet));
            tiles.Add(CreateSpecialProperty(28, "Water Works", 150, 4, TileGroup.Utility));
            tiles.Add(CreateStreet(29, "Marvin Gardens", 280, 24, TileGroup.YellowStreet));
            tiles.Add(CreateActionTile(30, "Go To Jail", TileGroup.GoToJail));

            // --- Side 4 (Indices 31 - 39) ---
            tiles.Add(CreateStreet(31, "Pacific Avenue", 300, 26, TileGroup.GreenStreet));
            tiles.Add(CreateStreet(32, "North Carolina Avenue", 300, 26, TileGroup.GreenStreet));
            tiles.Add(CreateActionTile(33, "Community Chest", TileGroup.CommunityChest));
            tiles.Add(CreateStreet(34, "Pennsylvania Avenue", 320, 28, TileGroup.GreenStreet));
            tiles.Add(CreateSpecialProperty(35, "Short Line", 200, 25, TileGroup.Station));
            tiles.Add(CreateActionTile(36, "Chance", TileGroup.Chance));
            tiles.Add(CreateStreet(37, "Park Place", 350, 35, TileGroup.DarkBlueStreet));
            tiles.Add(CreateActionTile(38, "Luxury Tax", TileGroup.LuxuryTax));
            tiles.Add(CreateStreet(39, "Boardwalk", 400, 50, TileGroup.DarkBlueStreet));

            return tiles;
        }


        private Card ChanceCard(string desc, bool isImmediate, Action<Player, IGameEngine> effect)
        {
            return new Card(CardType.Chance, desc, effect, isImmediate);
        }

        public List<Card> LoadChanceCards()
        {
            // Helper function for creating Chance cards
            //Func<string, bool, Action<Player, IGameEngine>, Card> ChanceCard =
            //    (desc, isImmediate, effect) => new Card(CardType.Chance, desc, effect, isImmediate);

            return new List<Card>
            {
                ChanceCard("Advance to GO", true, (p, g) => g.MovePlayerTo(p, 0)),
                ChanceCard("Speeding fine. Pay $15", true, (p, g) => g.HandlePayment(p, null, 15)),
                ChanceCard("Get out of Jail Free", false, (p, g) => p.NoJailFreeCards += 1),
                ChanceCard("Go to jail", true, (p, g) => g.MovePlayerTo(p, g.GetTileIndex("jail"))),
                ChanceCard("Bank error in your favour, collect 50", true, (p, g) => p.Balance += 50),
                ChanceCard("Go three steps back", true, (p, g) => g.MovePlayer(p, -3)),
                ChanceCard("Go to the nearest station", true, (p, g) => g.MovePlayerTo(p, g.GetTileIndex("Nearest Station")))
            };
        }

        public static Card GenerateRandomChanceCard()
        {
            Random random = new Random();

            Func<string, bool, Action<Player, IGameEngine>, Card> ChanceCard =
            (desc, isImmediate, effect) => new Card(CardType.Chance, desc, effect, isImmediate);

            int roll = random.Next(7);
            return roll switch
            {
                //- Probably make fines more dynamic
                //- More intense chances like pay everyone and tax every house need larger methods
                //- weighted chances would be fun
                0 => ChanceCard("Advance to GO", true, (p, g) => g.MovePlayerTo(p, 0)),
                1 => ChanceCard("Speeding fine. Pay $15", true, (p, g) => g.HandlePayment(p, null, 15)),
                2 => ChanceCard("Get out of Jail Free", false, (p, g) => p.NoJailFreeCards += 1),
                3 => ChanceCard("Go to jail", true, (p, g) => g.MovePlayerTo(p, g.GetTileIndex("jail"))),
                4 => ChanceCard("Bank error in your favour, collect 50", true, (p, g) => p.Balance += 50),
                5 => ChanceCard("Go three steps back", true, (p, g) => g.MovePlayer(p, -3)),
                6 => ChanceCard("Go to the nearest station", true, (p, g) => g.MovePlayerTo(p, g.GetTileIndex("Nearest Station"))),
                _ => throw new Exception("Unexpected roll")
            };
        }


        private Card CommunityChestCard(string desc, bool isImmediate, Action<Player, IGameEngine> effect)
        {
            return new Card(CardType.CommunityChest, desc, effect, isImmediate);
        }

        public List<Card> LoadCommunityChestCards()
        {
            return new List<Card>
            {
                CommunityChestCard("Advance to GO (Collect $200)", true, (p, g) => g.MovePlayerTo(p, 0)),
                CommunityChestCard("Bank error in your favour. Collect $200", true, (p, g) => p.Balance += 200),
                CommunityChestCard("Doctor’s fees. Pay $50", true, (p, g) => g.HandlePayment(p, null, 50)),
                CommunityChestCard("From sale of stock you get $50", true, (p, g) => p.Balance += 50),
                CommunityChestCard("Get out of Jail Free", false, (p, g) => p.NoJailFreeCards += 1),
                CommunityChestCard("Go to Jail. Do not pass GO. Do not collect $200", true, (p, g) => g.MovePlayerTo(p, g.GetTileIndex("jail"))),
                CommunityChestCard("Grand Opera Night. Collect $50 from every player", true, (p, g) => g.CollectFromAllPlayers(p, 50)),
                CommunityChestCard("Income tax refund. Collect $20", true, (p, g) => p.Balance += 20),
                CommunityChestCard("Life insurance matures. Collect $100", true, (p, g) => p.Balance += 100),
                CommunityChestCard("Pay hospital fees of $100", true, (p, g) => g.HandlePayment(p, null, 100)),
                CommunityChestCard("Pay school fees of $150", true, (p, g) => g.HandlePayment(p, null, 150)),
                CommunityChestCard("Receive $25 consultancy fee", true, (p, g) => p.Balance += 25),
                CommunityChestCard("You are assessed for street repairs. Pay $40 per house and $115 per hotel", true, (p, g) => g.PayPropertyRepairCosts(p, 40, 115)),
                CommunityChestCard("You have won second prize in a beauty contest. Collect $10", true, (p, g) => p.Balance += 10),
                CommunityChestCard("You inherit $100", true, (p, g) => p.Balance += 100)
            };
        }

        //public static Card GenerateCommunityChestCard()
        //{

        //}
    }
}
