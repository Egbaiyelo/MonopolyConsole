using MonopolyConsole.Core.Interfaces;
using MonopolyConsole.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static MonopolyConsole.Core.Models.Card;
using static MonopolyConsole.Core.Models.Tile;

namespace MonopolyConsole.Data
{
    // Contains static property lists,
    // handles saving/loading game state from files.
    // setup data

    public interface IGameDataService
    {
        List<Tile> LoadTiles();

        List<Card> LoadChanceCards();

        List<Card> LoadCommunityChestCards();
    }

    public  class GameDataService : IGameDataService
    {
        public GameDataService() { }

        public List<Tile> LoadTiles()
        {
            return new List<Tile>
            {
                new Tile(0, "GO", TileType.Go, null),
                new Tile(1, "Mediterranean Avenue", TileType.BrownStreet,
                    new Property { Name = "Mediterranean Avenue", Price = 60, BaseRent = 2, Group = TileType.BrownStreet }),
                new Tile(2, "Community Chest", TileType.CommunityChest),
            };
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
