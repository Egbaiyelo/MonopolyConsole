using MonopolyConsole.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Core.BoardComponents
{
    /// <summary>
    /// Monopoly Tiles
    /// </summary>
    public abstract class Tile
    {
        public enum TileGroup
        {
            // Action Tiles
            Go,
            GoToJail,
            Jail,
            FreeParking,

            // Tax Tiles
            IncomeTax,
            LuxuryTax,

            // Card Tiles
            Chance,
            CommunityChest,

            // Property Tiles 
            BrownStreet,
            LightBlueStreet,
            PinkStreet,
            OrangeStreet,
            RedStreet,
            YellowStreet,
            GreenStreet,
            DarkBlueStreet,
            Station,
            Utility
        }

        public enum TileType
        {
            // tax tiles
            Tax, 

            // Community chest & Chance 
            Card,

            // Property and special properties (Utilities and stations)
            Property, 

            // go to jail, go, free parking
            Corner, 

            // Jail
            Jail
        }

        public int Index;
        public string Name { get; set; }

        private readonly Action<Player> _specialAction;

        // The group the tile belongs to, the type is infered from the group
        public TileGroup Group;

        //+ Maybe cache this later
        public TileType Type
        {
            get
            {
                return Group switch
                {
                    // Action Tiles
                    TileGroup.Go => TileType.Corner,
                    TileGroup.GoToJail => TileType.Corner,
                    TileGroup.FreeParking => TileType.Corner,

                    // Jail (special Action)
                    TileGroup.Jail => TileType.Jail,

                    // Tax Tiles
                    TileGroup.IncomeTax => TileType.Tax,
                    TileGroup.LuxuryTax => TileType.Tax,

                    // Card Tiles
                    TileGroup.Chance => TileType.Card,
                    TileGroup.CommunityChest => TileType.Card,

                    // Property Tiles (Streets, Stations, Utilities)
                    TileGroup.BrownStreet or
                    TileGroup.LightBlueStreet or
                    TileGroup.PinkStreet or
                    TileGroup.OrangeStreet or
                    TileGroup.RedStreet or
                    TileGroup.YellowStreet or
                    TileGroup.GreenStreet or
                    TileGroup.DarkBlueStreet or
                    TileGroup.Station or
                    TileGroup.Utility => TileType.Property,

                    _ => throw new InvalidOperationException($"No TileType mapping defined for {Group}"),
                };
            }
        }

        public Tile(int index, string name, TileGroup group)
        {
            Index = index;
            Name = name;
            Group = group;
        }

        public abstract void OnLand(Player playerdude);
    }

    public class PropertyTile : Tile
    {

        public Property? Deed;


        public PropertyTile(int index, string name, TileGroup group, Property deed) : base(index, name, group)
        {
            Deed = deed;
        }

        public override void OnLand(Player player)
        {
            // pay rent or buy property

            if (Deed.Owner != null)
            {
                // Pay rent
            }
            else
            {
                // Buy
                if (Deed.Price < player.Balance)
                {

                }
                //+ No auction maybe
            }
        }

        public bool CanBuyProperty(Player player)
        {
            if (Deed.Owner != null)
                return false;

            if (Deed.Price > player.Balance)
                return false;

            return true;
        }
    }

    public class ActionTile : Tile
    {
        private readonly Action<Player> _specialAction;

        public ActionTile(int index, string name, TileGroup group, Action<Player> action) : base(index, name, group)
        {
            _specialAction = action;
        }

        public override void OnLand(Player player)
        {
            // Invoke action on player
        }
    }
}
