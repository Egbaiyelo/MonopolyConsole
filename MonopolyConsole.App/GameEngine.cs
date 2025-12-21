using MonopolyConsole.App.Prompters;
using MonopolyConsole.Core.BoardComponents;
using MonopolyConsole.Core.Interfaces;
using MonopolyConsole.Core.Models;
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
    /// <summary>
    /// 
    /// </summary>
    /// 

    //+ Might split into bank board and runner maybe
    //+ if player roll double
    //+ join moveplayer and moveplayerto
    internal class GameEngine : IGameEngine
    {
        public bool GameEnded = false;

        DiceRoller DiceRoller;
        Board Board;
        GameDataService GameDataService;

        public GameEngine()
        {
            DiceRoller = new DiceRoller();
            GameDataService = new GameDataService();

        }


        public void SetupGame()
        {
            // Board setup
            var tiles = GameDataService.LoadTiles().ToArray();
            Board = new Board(tiles, 
                GameDataService.LoadChanceCards(), 
                GameDataService.LoadCommunityChestCards());
        }

        public void ProcessTurn(Player player)
        {
            int roll = DiceRoller.Roll();

            if (player.InJail)
            {
                player.Prompter.Notify(player, "You are in Jail");
                //HandleJail();
                return;
            }

            player.Prompter.Notify(player, $"You rolled {roll}");

            MovePlayer(player, roll);

            // Ask player what they want to do next?
        }

        public void ProcessLanding(Player player, Tile tile)
        {
            switch (tile.Type) {
                case Tile.TileType.Tax:
                    break;
                case Tile.TileType.Property:
                    //if (tile.Deed.Owner != null)
                    //{
                    //    //HandlePayment(player, tile.Deed.Owner, tile.Deed.Rent);
                    //}
                    //else if (CanBuyProperty(player, tile.Deed))
                    //{
                    //    //
                    //}
                    break;
                case Tile.TileType.Card:
                    if (tile.Group == Tile.TileGroup.Chance)
                    {

                    }
                    else if(tile.Group == Tile.TileGroup.CommunityChest)
                    {

                    }
                    break;

                // Jail (Just visiting), free parking, Go, 
                case Tile.TileType.Corner:
                    if (tile.Group == Tile.TileGroup.GoToJail)
                    {
                        player.Prompter.Notify(player, "Go to Jail!!!");
                        //+ Get location dynamically later
                        //+ Jail player somehow
                        player.InJail = true;
                        MovePlayerTo(player, 10);

                    }
                    else if (tile.Group == Tile.TileGroup.FreeParking)
                    {
                        player.Prompter.Notify(player, "Free parking");
                    }
                    else if (tile.Group == Tile.TileGroup.Go)
                    {
                        player.Prompter.Notify(player, "You landed on Go");
                    }
                    break;

                // Default
                default:
                    // ??
                    if (tile.Group == Tile.TileGroup.Jail)
                    {
                        if (player.InJail)
                        {
                            player.Prompter.Notify(player, "Just visiting Jail");
                        }
                        else
                        {
                            player.Prompter.Notify(player, "You are in Jail");
                        }
                    }
                    break;
            }
        }

        public void HandleGameActions(GameAction g, Player player)
        {
            switch (g)
            {
                case PayTax p:
                    HandlePayment(player, null, p.Amount);
                    break;

                case PayRent p:
                    HandlePayment(player, p.Property.Owner, p.Property.Rent);
                    break;

                case DrawChance:
                    Card chance = Board.ChanceDeck.Dequeue();
                    chance.Effect?.Invoke(player, this);
                    Board.ChanceDeck.Enqueue(chance);
                    break;

                case DrawChest:
                    Card chest = Board.CommunityChestDeck.Dequeue();
                    chest.Effect?.Invoke(player, this);
                    Board.CommunityChestDeck.Enqueue(chest);
                    break;

                case AskBuy a:
                    break;

                case GoToJail:
                    
                    break;

                case Notify n:
                    player.Prompter.Notify(player, n.Message);
                    break;

                case Nothing n:
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
            
            return true;    
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
            if (recipient == null) // bank
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
                player.Prompter.Notify(player, $"You get $200!");
            }

            // Update Player position
            player.Position += steps;
            player.Position %= Board.BoardSize;
            player.Prompter.Notify(player, $"You have landed on {Board[player.Position].Name}");

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

        public void CollectFromAllPlayers(Player receiver, int amount)
        {
            throw new NotImplementedException();
        }

        public void PayPropertyRepairCosts(Player receiver, int houseCost, int hotelCost)
        {
            throw new NotImplementedException();
        }

    }
}
