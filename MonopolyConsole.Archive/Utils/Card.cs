using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static MonopolyConsole.Utils.Card;

namespace MonopolyConsole.Utils
{
    public enum CardType { CommunityChest, Chance, Deed };

    interface ICard
    {
        //public string Description { get; } //- issue
        CardType Type { get; }
    }
    //- Make IGameEvent I guess for effects

    //- plan
    //interface IDeedCard : ICard
    //{
    //    int Cost { get; }
    //    int Rent { get; }
    //    string ColorGroup { get; }
    //}

    //- Actually action card now (mostly)
    internal class ActionCard : ICard
    {
        public CardType Type { get; set; }
        public string Description;
        public bool IsImmediate;
        public Action<Player, Game>? Effect;


        public ActionCard(CardType type, string description, bool ImmediateUse, Action<Player, Game>? effect)
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
