using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Utils
{
    public enum TileType { Property, Station, Utility, CommunityChest, Chance, Edge, Tax };
    abstract class Tile
    {
        public virtual string Name { get; internal set; } = "Tile";
        public TileType TileType { get; internal set; }

        public virtual void LandOn(Player player, Game game)
        {
            Console.WriteLine($"{player.Name} has landed on a {Name}");
        }
    }

    //- no more decor

    /// <summary>
    /// 
    /// </summary>
    internal class PropertyTile : Tile
    {
        public override string Name { get; internal set; }
        public Property Property;
        //public Player? Owner;
        //- many need get internal set
        //public int Houses;
        //private bool Hotel;
        //public bool Mortgaged;
        //- maybe add a rate that determines how much house and hotel can cost like 0.25-0.5 of cost

        /// <summary>
        /// Board tile that holds property
        /// </summary>
        /// <param name="property">The property</param>
        public PropertyTile(Property property)
        {
            Property = property;
            Name = property.Name + " Tile"; //- Do I need to add 'tile'
            TileType = TileType.Property;
        }

        public override void LandOn(Player p, Game g)
        {
            Console.WriteLine($"{p.Name} lands on {Property.Name}");
            int cost = getCost();
            if (Property.Owner is null && p.Balance >= cost)
            {
                //- Maybe get data from property and tile is just wrapper class
                Console.WriteLine("Do you wish to buy {0} for {1} (Y/N)", Property.Name, cost);
                string response = Console.ReadLine();
                if (response.ToUpper().Contains('Y'))
                {
                    p.Balance -= cost;
                    p.Properties.Add(Property);
                    Property.Owner = p;
                    Property.Owner = p;
                    Console.WriteLine($"{p.Name} has purchased {Property.Name}");
                }
            }

            else if (Property.Owner is not null && Property.Owner != p)
            {
                int rent = Property.CalculateRent();
                p.Balance -= rent;
                Property.Owner.Balance += rent;
                Console.WriteLine($"{p.Name} pays ${rent} rent to {Property.Owner.Name}.");
            }
            // else just do nothing
        }

        public virtual int getRent() => Property.CalculateRent();
        public virtual int getCost() => Property.CalculateCost();
    }

    internal class ActionTile : Tile
    {
        public override string Name { get; internal set; }
        public CardType CardType;

        private CardFactory CardFactory;

        public ActionTile(CardType cardType)
        {
            //if (cardType != CardType.CommunityChest || CardType.Chance)
            //    throw 
            CardType = cardType;
            CardFactory = new CardFactory();
            Name = cardType == CardType.Chance ? "Chance Tile" : "CommunityChest Tile";
            TileType = cardType == CardType.Chance ? TileType.Chance : TileType.CommunityChest;
        }

        public override void LandOn(Player player, Game game)
        {
            Console.WriteLine($"{player.Name} lands on a {Name}");
            ActionCard card = CardFactory.GetCard(CardType);
            Console.WriteLine(card.Description);
            card.Effect.Invoke(player, game);
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
            TileType = TileType.Edge;
        }

        public override void LandOn(Player player, Game game)
        {
            Console.WriteLine($"{player.Name} lands on the {Name}");
            Effect.Invoke(player, game);
        }
    }


    /// <summary>
    /// Charges player a certain amount
    /// </summary>
    internal class TaxTile : Tile
    {
        public override string Name { get; internal set; }
        private int Tax;

        public TaxTile(int tax, string name = "Tax Tile")
        {
            Tax = tax;
            Name = name;
            TileType = TileType.Tax;
        }
        public override void LandOn(Player player, Game game)
        {
            Console.WriteLine($"{player.Name} lands on a {Name}");
            Console.WriteLine($"Pay {Tax}");
            player.Balance -= Tax;            
        }
    }


    internal class UtilityTile : PropertyTile
    {
        public UtilityTile(Property property) : base(property) { TileType = TileType.Utility; }

        
        public int CalculateRent(int diceRoll) => Property.Rent * diceRoll;
    }


    internal class StationTile : PropertyTile
    {
        //- turn stations to property deed too
        private static List<StationTile> _instances = new List<StationTile>();

        /// <summary>
        /// Monopoly Station tile
        /// </summary>
        /// <param name="property"></param>
        /// <seealso cref="PropertyTile"/>
        public StationTile(Property property) : base(property) { 
            TileType = TileType.Station; 
            _instances.Add(this);
        }

        public override int getRent()
        {
            //- stations owned get set manage
            switch (Property.Owner.StationsOwned)
            {
                case 1:
                    return 25;
                case 2:
                    return 50;
                case 3:
                    return 100;
                case 4:
                    return 200;
                default:
                    return 25;
            }
            //+ or return Math.Pow(2, Property.Owner.StationsOwned) * 25 just so the 25 can be set easy
        }
    }

}
