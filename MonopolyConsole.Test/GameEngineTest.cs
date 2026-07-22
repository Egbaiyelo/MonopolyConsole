using MonopolyConsole.Core.BoardComponents;
using MonopolyConsole.Core.Interfaces;
using MonopolyConsole.Core.Models;
using MonopolyConsole.Core.Services;

using GameEngine = MonopolyConsole.App.GameEngine;

namespace MonopolyConsole.Test
{

    [TestClass]
    public sealed class GameEngineTest
    {
        MockPlayerList mockPlayerList = new MockPlayerList();

        [TestMethod]
        public void TestSetupGame()
        {
            GameEngine testEngine = new GameEngine( mockPlayerList.GetPlayers() ,new MockPrompter(),new MockDiceRoller(3,3));
            
        }

        public void BuyProperty(Player player, Property property)
        {
            throw new NotImplementedException();
        }

        public void CollectFromAllPlayers(Player receiver, int amount)
        {
            throw new NotImplementedException();
        }

        public Player GetCurrentPlayer()
        {
            throw new NotImplementedException();
        }

        public Player GetPlayer(string name)
        {
            throw new NotImplementedException();
        }

        public int GetTileIndex(string tileName)
        {
            throw new NotImplementedException();
        }

        public void HandlePayment(Player payer, Player? recipient, int amount)
        {
            throw new NotImplementedException();
        }

        public void HandleTrade(Player payer, Player? recipient, int amount)
        {
            throw new NotImplementedException();
        }

        public void MovePlayer(Player player, int steps)
        {
            throw new NotImplementedException();
        }

        public void MovePlayerTo(Player player, int tileIndex)
        {
            throw new NotImplementedException();
        }

        public void PayPropertyRepairCosts(Player receiver, int houseCost, int hotelCost)
        {
            throw new NotImplementedException();
        }

        public void ProcessLanding(Player player, Tile tile)
        {
            throw new NotImplementedException();
        }

        public void ProcessTurn(Player player)
        {
            throw new NotImplementedException();
        }


 
    }
}
