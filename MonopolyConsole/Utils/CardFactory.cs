using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MonopolyConsole.Utils.Card;

namespace MonopolyConsole.Utils
{
    internal class CardFactory
    {
        private static Random Random = new Random();

        public Card GetCard(CardType type)
        {
            if (type == CardType.Chance)
                return GenerateRandomChanceCard();
            else return GenerateRandomChanceCard(); //- for now
        }

        public static Card GenerateRandomChanceCard()
        {
            Func<string, bool, Action<Player, Game>, Card> ChanceCard =
            (desc, isImmediate, effect) => new Card(CardType.CommunityChest, desc, isImmediate, effect);

            int roll = Random.Next(7);
            return roll switch
            {
                //- Probably make fines more dynamic
                //- More intense chances like pay everyone and tax every house need larger methods
                0 => ChanceCard("Advance to GO", true, (p, g) => p.Position = 0),
                1 => ChanceCard("Speeding fine. Pay $15", true, (p, g) => p.Balance -= 15),
                2 => ChanceCard("Get out of Jail Free", false, null),
                3 => ChanceCard("Go to jail", true, (p, g) => p.Position = g.GetPosition("jail")),
                4 => ChanceCard("Bank error in your favour, collect $100", true, (p, g) => p.Balance += 100),
                5 => ChanceCard("Go three steps back", true, (p, g) => p.Position -= 3),
                6 => ChanceCard("Go to the nearest station", true, (p, g) => p.Position -= 15),
                _ => throw new Exception("Unexpected roll")
            };
        }

        public static void GenerateRandomCommunityChestCard()
        {
            //- Same as Chance but more generous
        }
    }
}
