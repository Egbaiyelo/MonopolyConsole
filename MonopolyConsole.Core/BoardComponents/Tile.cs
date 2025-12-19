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
    public class Tile
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

            // Community chest, etc
            Card,

            // Property and special properties (Utilities and stations)
            Property, 

            // go to jail, go, 
            Action, 

            // Jail
            Jail
        }

        public int Index;
        public string Name { get; set; }

        // The group the tile belongs to, the type is infered from the group
        public TileGroup Group;
        public Property? Deed;
        public Card? Card;

        public TileType Type
        {
            get
            {
                return Group switch
                {
                    // Action Tiles
                    TileGroup.Go => TileType.Action,
                    TileGroup.GoToJail => TileType.Action,
                    TileGroup.FreeParking => TileType.Action,

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


        public Tile(int index, string name, TileGroup group, Property? deed)
        {
            Index = index;
            Name = name;
            Group = group;
            Deed = deed;
        }
    }
}
