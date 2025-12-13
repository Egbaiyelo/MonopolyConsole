using MonopolyConsole.Core.Board;
using MonopolyConsole.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Core.Interfaces
{
    public interface IGameEngine
    {

        void SetupGame();
        void ProcessTurn(Player player);
        void ProcessLanding(Player player, Tile tile);

        // Move player and handle landing
        void MovePlayerTo(Player player, int tileIndex);

        void MovePlayer(Player player, int steps);

        Player GetPlayer(string name);
        Player GetCurrentPlayer();


        bool CanAffordProperty(Player player, Property property);
        void BuyProperty(Player player, Property property);
        void EndTurn();

        //Maybe make trading system?
        //Mortgage, sell, buy, build, demolish
        void HandlePayment(Player payer, Player? recipient, int amount);
        void HandleTrade(Player payer, Player? recipient, int amount);
        bool IsBankrupt(Player player);



        int GetTileIndex(string tileName);

    }
}
