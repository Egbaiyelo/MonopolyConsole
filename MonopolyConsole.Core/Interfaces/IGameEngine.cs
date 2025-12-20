using MonopolyConsole.Core.BoardComponents;
using MonopolyConsole.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Core.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGameEngine
    {

        void SetupGame(IPrompter prompter);

        // Logic for a player's turn (roll dice, process landing)
        void ProcessTurn(Player player);

        // Logic for landing on a tile, paying tax, buy property, pay rent
        void ProcessLanding(Player player, Tile tile);

        // Move player and handle landing
        void MovePlayerTo(Player player, int tileIndex);

        void MovePlayer(Player player, int steps);

        Player GetPlayer(string name);
        Player GetCurrentPlayer();


        bool CanBuyProperty(Player player, Property property);
        void BuyProperty(Player player, Property property);
        void EndTurn();

        //Maybe make trading system?
        //Mortgage, sell, buy, build, demolish
        void HandlePayment(Player payer, Player? recipient, int amount);
        void HandleTrade(Player payer, Player? recipient, int amount);
        bool IsBankrupt(Player player);



        int GetTileIndex(string tileName);




        void CollectFromAllPlayers(Player receiver, int amount);

        void PayPropertyRepairCosts(Player receiver, int houseCost, int hotelCost);

        bool GameEnded();
    }
}
