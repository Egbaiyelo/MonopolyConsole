using MonopolyConsole.Core.Interfaces;
using MonopolyConsole.Core.Models;

namespace MonopolyConsole.Core.Services
{
    public class GameEngine : IGameEngine
    {
        public void StartGame()
        {
            throw new NotImplementedException();
        }

        public void EndTurn()
        {
            throw new NotImplementedException();
        }


        void IGameEngine.BuyProperty(Player player, Property property)
        {
            throw new NotImplementedException();
        }

        bool IGameEngine.CanAffordProperty(Player player, Property property)
        {
            throw new NotImplementedException();
        }

        Player IGameEngine.GetCurrentPlayer()
        {
            throw new NotImplementedException();
        }

        Player IGameEngine.GetPlayer()
        {
            throw new NotImplementedException();
        }

        void IGameEngine.ProcessLanding(Player player, Tile tile)
        {
            //If tile is tax tax player, action, then do that, property then get type
        }

        void IGameEngine.ProcessTurn(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
