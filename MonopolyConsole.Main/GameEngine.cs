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
        IPrompter ConsolePrompter;
        Board Board;
        GameDataService GameDataService;

        public GameEngine()
        {
            //SetupGame();
        }


        public void SetupGame(IPrompter prompter)
        {
            DiceRoller = new DiceRoller();
            ConsolePrompter = prompter;
            GameDataService = new GameDataService();

            // Board setup
            var tiles = GameDataService.LoadTiles().ToArray();
            Board = new Board(tiles);
        }

        public void ProcessTurn(Player player)
        {
            int roll = DiceRoller.Roll();
            ConsolePrompter.Notify(player, $"You rolled a {roll}");

            MovePlayer(player, roll);

            // Ask player what they want to do next?
        }

        public void ProcessLanding(Player player, Tile tile)
        {
            switch (tile.Type) {
                case Tile.TileType.Tax:
                    break;
                case Tile.TileType.Property:
                    if (tile.Deed.Owner != null)
                    {
                        HandlePayment(player, tile.Deed.Owner, tile.Deed.Rent);
                    }
                    else if (CanBuyProperty(player, tile.Deed))
                    {
                        
                    }
                    break;
                case Tile.TileType.Card:
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

        public bool CanBuyProperty(Player player, Property property)
        {
            if (property == null)
                return false;

            if (property.Owner != null)
                return false;

            if (property.Price > player.Balance)
                return false;
            
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
            // If player passed go
            if (player.Position + steps > Board.BoardSize)
            {
                player.Balance += 200;
                ConsolePrompter.Notify(player, $"You get $200!");
            }

            // Update Player position
            player.Position += steps;
            player.Position %= Board.BoardSize;
            ConsolePrompter.Notify(player, $"You have landed on {Board[player.Position].Name}");

            // Handle landing
            ProcessLanding(player, Board[player.Position]);
        }

        public void MovePlayerTo(Player player, int tileIndex)
        {
            // If player passed go
            if (player.Position > tileIndex)
                player.Balance += 200;

            // Update Player position
            player.Position = tileIndex;

            // Handle landing
            ProcessLanding(player, Board[player.Position]);
        }


    }
}
