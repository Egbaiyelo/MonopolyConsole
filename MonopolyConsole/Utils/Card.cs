using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MonopolyConsole.Utils.Card;

namespace MonopolyConsole.Utils
{
    interface ICard
    {
        //public string Description { get; } //- issue
        CardType Type { get; }
    }

    //- plan
    //interface IDeedCard : ICard
    //{
    //    int Cost { get; }
    //    int Rent { get; }
    //    string ColorGroup { get; }
    //}

    //- Actually action card now (mostly)
    internal class Card : ICard
    {
        public enum CardType { CommunityChest, Chance, Deed };

        public CardType Type { get; set; }
        public string Description;
        public bool IsImmediate;
        public Action<Player, Game>? Effect;


        public Card(CardType type, string description, bool ImmediateUse, Action<Player, Game>? effect)
        {
            Type = type;
            Description = description;
            IsImmediate = ImmediateUse;
            Effect = effect;            
        }

        public void Activate(Player player, Game game)
        {
            Effect?.Invoke(player, game);
        }

        public void ReadDescription()
        {
            Console.WriteLine(Description);
        }
    }
}
