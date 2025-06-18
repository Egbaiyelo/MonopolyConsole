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
        public int Cost;
        public int Rent;
        public Property Property;
        public Player? Owner;

        private int Houses;
        private bool Hotel;
        protected bool Mortgaged;
        //- maybe add a rate that determines how much house and hotel can cost like 0.25-0.5 of cost

        /// <summary>
        /// Tile that holds property
        /// </summary>
        /// <param name="property"></param>
        public PropertyTile(Property property)
        {
            Property = property;
            Name = property.Name + " Tile"; //- Do I need to add 'tile'
            Cost = property.Cost;
            Rent = property.Rent;
            TileType = TileType.Property;

            if (Property.Owner != null)
            {
                Owner = Property.Owner;
            }
        }

        public override void LandOn(Player p, Game g)
        {
            Console.WriteLine($"{p.Name} lands on {Property.Name}");
            int cost = CalculateCost();
            if (Owner is null && p.Balance >= cost)
            {
                //- Maybe get data from property and tile is just wrapper class
                Console.WriteLine("Do you wish to buy {0} for {1} (Y/N)", Property.Name, cost);
                string response = Console.ReadLine();
                if (response.ToUpper().Contains('Y'))
                {
                    p.Balance -= cost;
                    p.Properties.Add(Property);
                    Property.Owner = p;
                    Owner = p;
                    Console.WriteLine($"{p.Name} has purchased {Property.Name}");
                }
            }
            else if (Owner is not null && Owner != p)
            {
                int rent = Property.CalculateRent();
                p.Balance -= rent;
                Owner.Balance += rent;
                Console.WriteLine($"{p.Name} pays ${rent} rent to {Owner.Name}.");
            }
            // else just do nothing
        }

        public virtual int CalculateRent()
        {
            int rent = Property.CalculateRent();
            if (Hotel) return (int)(1.20 * rent);
            return rent + (int)(0.20 * rent * Houses);
        }

        public virtual int CalculateCost()
        {
            return Property.CalculateCost();
        }

        public void Upgrade()
        {
            if (Property.Owner == null) return;
            if (Hotel) return;

            if (Houses < 4) { Houses++; Property.Owner.Balance -= 50; }
            if (Houses == 4) { Houses = 0; Hotel = true;  Property.Owner.Balance -= 100; }
            Console.WriteLine($"{Property.Name} has been upgraded");
        }
    }

    internal class ActionTile : Tile
    {
        public override string Name { get; internal set; }
        public CardType CardType;

        private CardFactory CardFactory;

        //public ActionCard ActionCard; //- Need to link to factory instead

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

        public override int CalculateRent()
        {
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
