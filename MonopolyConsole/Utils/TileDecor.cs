using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Utils
{
    abstract class TileDecorator : Tile
    {
        Tile _tile;

        public TileDecorator(Tile tile)
        {
            _tile = tile;
        }

        public virtual string Name => _tile.Name;

        public override void LandOn(Player player, Game game) => _tile.LandOn(player, game);

    }

    internal class HouseDecorator : TileDecorator
    {
        private int _houseCount;

        public HouseDecorator(Tile tile, int houseCount) : base(tile)
        {
            _houseCount = houseCount;
        }

    }

    internal class HotelDecorator : TileDecorator
    {
        public HotelDecorator(Tile tile) : base(tile) { }


    }

    internal class MortgageDecorator : TileDecorator
    {
        public MortgageDecorator(Tile tile) : base(tile) { }

        public override void LandOn(Player player, Game game)
        {
            base.LandOn(player, game);
            Console.Write(" [MORTGAGED]");
        }
    }
}
