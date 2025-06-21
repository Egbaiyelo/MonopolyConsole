using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static MonopolyConsole.Utils.Card;

namespace MonopolyConsole.Utils
{
    internal class CardFactory
    {
        private static Random Random = new Random();

        public ActionCard GetCard(CardType type)
        {
            //- Property card too uniqque;
            if (type == CardType.Chance)
                return GenerateRandomChanceCard();
            else return GenerateRandomChanceCard(); //- for now
        }

        /// <summary>
        /// Returns a chance card
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static ActionCard GenerateRandomChanceCard()
        {
            Func<string, bool, Action<Player, Game>, ActionCard> ChanceCard =
            (desc, isImmediate, effect) => new ActionCard(CardType.CommunityChest, desc, isImmediate, effect);

            int roll = Random.Next(7);
            return roll switch
            {
                //- Probably make fines more dynamic
                //- More intense chances like pay everyone and tax every house need larger methods
                //- weighted chances would be fun
                0 => ChanceCard("Advance to GO", true, (p, g) => p.MoveTo(0)),
                1 => ChanceCard("Speeding fine. Pay $15", true, (p, g) => p.Balance -= 15),
                2 => ChanceCard("Get out of Jail Free", false, (p, g) => p.InJail = false),
                3 => ChanceCard("Go to jail", true, (p, g) => p.MoveTo(g.GetPosition("jail"))),
                4 => ChanceCard("Bank error in your favour, collect 50", true, (p, g) => p.Balance += 50),
                5 => ChanceCard("Go three steps back", true, (p, g) => p.Move(-3)),
                6 => ChanceCard("Go to the nearest station", true, (p, g) => p.MoveTo(g.GetPosition("Nearest Station"))),
                _ => throw new Exception("Unexpected roll")
            };
        }

        /// <summary>
        /// Returns a community chest card
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static ActionCard GenerateRandomCommunityChestCard()
        {
            //- Same as Chance but more generous
            Func<string, bool, Action<Player, Game>, ActionCard> CommunityChestCard =
            (desc, isImmediate, effect) => new ActionCard(CardType.CommunityChest, desc, isImmediate, effect);

            int roll = Random.Next(7);
            return roll switch
            {
                //- Probably make fines more dynamic
                //- More intense chests like everyone pay 20
                0 => CommunityChestCard("Advance to GO", true, (p, g) => p.MoveTo(0)),
                1 => CommunityChestCard("It's your lucky day, you found $50", true, (p, g) => p.Balance += 50),
                2 => CommunityChestCard("Get out of Jail Free", false, (p, g) => p.InJail = false),
                3 => CommunityChestCard("Go to jail", true, (p, g) => p.MoveTo(g.GetPosition("jail"))),
                4 => CommunityChestCard("Bank error in your favour, collect $100", true, (p, g) => p.Balance += 100),
                5 => CommunityChestCard("Go three steps back", true, (p, g) => p.Move(-3)),
                6 => CommunityChestCard("Go to the nearest station", true, (p, g) => p.Move(-10)),
                _ => throw new Exception("Unexpected roll")
            };
        }
    }
}
