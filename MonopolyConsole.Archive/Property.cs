using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole
{
    //-rename to deed maybe
    public enum PropertyGroup { Brown, LightBlue, Purple, Orange, Red, Yellow, Green, DarkBlue };
    internal class Property
    {
        public string Name { get; set; }
        public int Rent; //- priv
        public int Cost;    //- priv
        public PropertyGroup PropertyGroup;
        public Player? Owner;

        public int Houses;
        public bool Hotel;
        public bool Mortgaged;

        public Property(string name, int cost, int rent, PropertyGroup group = PropertyGroup.Brown)
        {
            Name = name;
            Rent = rent;
            Cost = cost;
            PropertyGroup = group;

            Houses = 0;
            Hotel = Mortgaged = false;
        }

        public virtual int CalculateRent()
        {
            if (Mortgaged) { return 0; }

            int rent = Rent;
            if (OwnsFullColorGroup(PropertyGroup, Owner.Game.GetAllGameProperties())) rent *= 2;
            if (Hotel) return (int)(1.20 * rent);
            return rent + (int)(0.20 * rent * Houses);
        }

        /// <summary>
        /// Calculate the resell cost of the property with developments into account
        /// </summary>
        /// <returns></returns>
        public virtual int CalculateCost()
        {
            // hotel for now is resold at 75
            int hotel = (Hotel) ? 75 + (4 * 50): 0;
            int cost = (Owner == null) ? Cost : Cost / 2;
            return cost + (50 * Houses) + hotel;
        }

        public void UnMortgage()
        {
            if (!Mortgaged) { return; }
            if (Owner.Balance > Cost / 2)
            {
                Owner.Balance -= Cost / 2;
                Mortgaged = false;
            }
            else
            {
                Console.WriteLine("Insufficient funds, need {0} but has {1}", Cost / 2, Owner.Balance);
            }
        }

        public void Upgrade()
        {
            if (Owner == null) return;
            if (Hotel) return;

            if (Houses < 4) { Houses++; Owner.Balance -= 50; }
            if (Houses == 4) { Houses = 0; Hotel = true; Owner.Balance -= 100; }
            Console.WriteLine($"{Name} has been upgraded");
        }

        public void DownGrade(bool developments = false, bool mortgage = false)
        {
            int reimburse = 0;

            if (Owner == null) return;
            if (Hotel)
            {
                Hotel = false;
                Houses = 4;
                reimburse += 75;
                Console.WriteLine("Hotel Sold");
            }
            if (Houses > 0 && (reimburse == 0 || developments)) {

                while (Houses > 0 && (reimburse == 0 || developments))
                {
                    Houses -= 1;
                    reimburse += 50;
                }
                Console.WriteLine("House(s) sold");
            }
            if (reimburse == 0 || mortgage)
            {
                Mortgaged = true;
                reimburse += Cost / 2;
                Console.WriteLine($"{Name} has been mortgaged to the bank");
            }
            Owner.Balance += reimburse;
        }

        public bool OwnsFullColorGroup(PropertyGroup group, IEnumerable<Property> allProperties)
        {
            var groupProperties = allProperties.Where(p => p.PropertyGroup == group);
            return groupProperties.All(p => Owner.Properties.Contains(p) && p.Owner == Owner);
        }

    }

}
