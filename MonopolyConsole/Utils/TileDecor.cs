using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Utils
{

    abstract class PropertyDecorator : PropertyTile
    {
        protected PropertyTile _tile;

        /// <summary>
        /// No longer in use.
        /// </summary>
        /// <param name="tile"></param>
        public PropertyDecorator(PropertyTile tile) : base(tile.Property)
        {
            _tile = tile;
        }

        public override int CalculateRent() => _tile.CalculateRent();
        public override void LandOn(Player player, Game game) => _tile.LandOn(player, game);
    }

    internal class MortgageDecorator : PropertyDecorator
    {
        public MortgageDecorator(PropertyTile tile) : base(tile) { }

        public override int CalculateRent() => 0; 

        public override void LandOn(Player player, Game game)
        {
            base.LandOn(player, game);
            Console.Write(" [MORTGAGED] No rent applied.");
        }
    }

    internal class HouseDecorator : PropertyDecorator
    {
        private int _houseCount;

        public HouseDecorator(PropertyTile tile, int houseCount) : base(tile)
        {
            _houseCount = houseCount;
        }

        public override int CalculateRent() => _tile.CalculateRent() + (_houseCount * 50);
    }

    internal class HotelDecorator : PropertyDecorator
    {
        public HotelDecorator(PropertyTile tile) : base(tile) { }

        public override int CalculateRent() => _tile.CalculateRent() + 100;
    }
}
