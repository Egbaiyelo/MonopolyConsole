using MonopolyConsole.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Core.Models
{
    public class Card
    {
        public enum CardType { Chance, CommunityChest };

        public CardType Type { get; }
        public string Description { get; }
        public Action<Player, IGameEngine>? Effect { get; }

        public bool IsImmediate;

        public Card(CardType cardType, string desc, Action<Player, IGameEngine> effect, bool isImmediate)
        {
            Type = CardType.Chance;
            Description = desc;
            Effect = effect;
            IsImmediate = isImmediate;
        }


        public void Activate(Player player, IGameEngine game)
        {
            Effect?.Invoke(player, game);
        }
    }
}
