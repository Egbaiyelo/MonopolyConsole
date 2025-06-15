using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Utils
{
    abstract class Tile
    {
        public virtual string Name { get; internal set; } = "Tile";

        public virtual void LandOn(Player player, Game game)
        {
            Console.WriteLine($"{player.Name} has landed on a {Name}");
        }
    }

    internal class PropertyTile : Tile
    {
        public override string Name { get; internal set; }
        public int Cost;
        public int Rent;
        public Property Property;
        public Player? Owner;

        public PropertyTile(Property property)
        {
            Property = property;
            Name = property.Name + " Tile"; //- Do I need this
            Cost = property.Cost;
            Rent = property.Rent;

            if (Property.Owner != null)
            {
                Owner = Property.Owner;
            }
        }

        public override void LandOn(Player p, Game g)
        {
            Console.WriteLine($"{p.Name} lands on {Property.Name}");
            if (Owner is null && p.Balance >= Cost)
            {
                //- Maybe get data from property and tile is just wrapper class
                Console.WriteLine("Do you wish to buy {0} for {1} (Y/N)", Property.Name, Cost);
                string response = Console.ReadLine();
                if (response.ToUpper().Contains('Y'))
                {
                    p.Balance -= Cost;
                    p.Properties.Add(Property);
                    Property.Owner = p;
                    Owner = p;
                    Console.WriteLine($"{p.Name} has purchased {Property.Name}");
                }
            }
            else if (Owner is not null && Owner != p)
            {
                p.Balance -= Rent;
                Owner.Balance += Rent;
                Console.WriteLine($"{p.Name} pays ${Rent} rent to {Owner.Name}.");
            }
            // else just do nothing
        }
    }

    internal class ActionTile : Tile
    {
        public override string Name { get; internal set; }
        public CardType CardType;
        public ActionCard ActionCard; //- Need to link to factory instead

        public ActionTile(CardType cardType, ActionCard card)
        {
            //if (cardType != CardType.CommunityChest || CardType.Chance)
            //    throw 
            CardType = cardType;
            ActionCard = card;
            Name = cardType == CardType.Chance ? "Chance Tile" : "CommunityChest Tile";
        }

        public override void LandOn(Player player, Game game)
        {
            Console.WriteLine($"{player.Name} lands on a {Name}");
            ActionCard.Effect.Invoke(player, game);
        }
    }

    //- to merge with action tile and action tile to be actioncard tile
    internal class EdgeTile : Tile
    {
        public override string Name { get; internal set; }
        public Action<Player, Game>? Effect;

        public EdgeTile(string name, Action<Player, Game>? effect)
        {
            Name = name;
            Effect = effect;
        }

        public override void LandOn(Player player, Game game)
        {
            Console.WriteLine($"{player.Name} lands on the {Name}");
            Effect.Invoke(player, game);
        }
    }



    internal class TaxTile : Tile
    {
        public override string Name { get; internal set; }
        private int Tax;
        public TaxTile(int tax)
        {
            Tax = tax;
            Name = "Tax Tile";
        }
        public override void LandOn(Player player, Game game)
        {
            Console.WriteLine($"{player.Name} lands on a {Name}");
            Console.WriteLine($"Pay {Tax}");
            player.Balance -= Tax;            
        }
    }

    //internal class StationTile
}
