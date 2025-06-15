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
        //public string Name;
        public abstract void LandOn(Player player, Game game);
    }

    internal class PropertyTile : Tile
    {
        public string Name;
        public int Cost;
        public int Rent;
        public Property Property;
        public Player? Owner;

        public PropertyTile(Property property)
        {
            Name = property.Name;
            Cost = property.Cost;
            Rent = property.Rent;
            Property = property;

            if (Property.Owner != null)
            {
                Owner = Property.Owner;
            }
        }

        public override void LandOn(Player p, Game g)
        {
            if (Owner is null && p.Balance >= Cost)
            {
                //- Maybe get data from property and tile is just wrapper class
                Console.WriteLine("Do you wish to buy {0} for {1} (Y/N)", Name, Cost);
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

    //- Might merge the two
    internal class ChanceTile : Tile
    {
        public string Name = "";
        public ActionCard Chance;

        public ChanceTile(ActionCard card)
        {
            if (card.Type != CardType.Chance)
                throw new ArgumentException("Card name:{0}, must be of type Chance ", nameof(card));
            Chance = card;
        }

        public override void LandOn(Player player, Game game)
        {
            Chance.Effect.Invoke(player, game);
        }
    }

    internal class CommunityChestTile : Tile
    {
        public string Name = "";
        public ActionCard Chest;

        public CommunityChestTile(ActionCard card)
        {
            if (card.Type != CardType.CommunityChest)
                throw new ArgumentException("Card must be of type CommunityChest", nameof(card));
            Chest = card;
        }

        public override void LandOn(Player player, Game game)
        {
            Chest.Effect.Invoke(player, game);
        }
    }

    internal class EdgeTile : Tile
    {
        public override void LandOn(Player player, Game game)
        {

        }
    }
}
