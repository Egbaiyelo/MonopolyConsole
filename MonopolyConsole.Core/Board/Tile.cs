using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Core.Board
{
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
            //
            Tax, Card, Property, Action, Jail
        }

        public int Index;
        public string Name { get; set; }
        public TileGroup Group;
        Property? Deed;

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
