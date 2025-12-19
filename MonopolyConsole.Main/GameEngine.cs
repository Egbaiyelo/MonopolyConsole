using MonopolyConsole.App.Prompters;
using MonopolyConsole.Core.Board;
using MonopolyConsole.Core.Interfaces;
using MonopolyConsole.Core.Models;
using MonopolyConsole.Core.Services;
using MonopolyConsole.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.App
{
    internal class GameEngine : IGameEngine
    {
        DiceRoller DiceRoller;
        //IPrompter ConsolePrompter;
        Board Board;
        GameDataService GameDataService;

        public GameEngine()
        {
            SetupGame();
        }


        public void SetupGame()
        {
            DiceRoller = new DiceRoller();
            //ConsolePrompter = new HumanConsolePrompter();
            GameDataService = new GameDataService();

            // Board setup
            var tiles = GameDataService.LoadTiles().ToArray();
            Board = new Board(tiles);
        }

        public void ProcessTurn(Player player)
        {
            int roll = DiceRoller.Roll();

            MovePlayer(player, roll);   
        }   
        
        public void ProcessLanding(Player player, Tile tile)
        {
            switch (tile.Type) {
                case Tile.TileType.Tax:
                    break;
                case Tile.TileType.Property:
                    break;

                // Jail (Just visiting), free parking
                default: 
                    break;

            }
        }





        public void BuyProperty(Player player, Property property)
        {
            throw new NotImplementedException();
        }

        public bool CanAffordProperty(Player player, Property property)
        {
            throw new NotImplementedException();
        }

        public void EndTurn()
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

        public bool IsBankrupt(Player player)
        {
            throw new NotImplementedException();
        }

        public void MovePlayer(Player player, int steps)
        {
            // Update Player position
            player.Position += steps;
            player.Position %= Board.BoardSize;

            // Handle landing
            ProcessLanding(player, Board[player.Position]);
        }

        public void MovePlayerTo(Player player, int tileIndex)
        {
            throw new NotImplementedException();
        }


    }
}
