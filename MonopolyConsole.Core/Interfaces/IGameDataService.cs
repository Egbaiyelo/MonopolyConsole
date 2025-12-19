using MonopolyConsole.Core.BoardComponents;
using MonopolyConsole.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Core.Interfaces
{
    public interface IGameDataService
    {

        List<Tile> LoadTiles();

        List<Card> LoadChanceCards();

        List<Card> LoadCommunityChestCards();
        
    }
}
