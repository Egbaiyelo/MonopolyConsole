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
        IEnumerable<Player> Players;

        public GameEngine(IEnumerable<Player> players)
        {
            DiceRoller = new DiceRoller();
            GameDataService = new GameDataService();
            Players = players;
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
            if ( player.IsBankrupt)
            {
                player.Prompter.Notify(player, $"You have an outstanding debt of {Math.Abs(player.Balance)}");
            }

            player.Prompter.Notify(player, $"You rolled {roll}");

            MovePlayer(player, roll);

            player.Prompter.Notify(player, $"Your balance is now {player.Balance}");

            // Ask player what they want to do next?
            player.Prompter.
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

        public void HandleGameActions(GameAction gameAction, Player player)
        {
            switch (gameAction)
            {
                case PayTax p:
                    HandlePayment(player, null, p.Amount);
                    break;

                case PayRent p:
                    HandlePayment(player, p.Property.Owner, p.Property.Rent);
                    break;

                case DrawChance:
                    Card chance = Board.ChanceDeck.Dequeue();
                    player.Prompter.Notify(player, chance.Description);
                    chance.Effect?.Invoke(player, this);
                    Board.ChanceDeck.Enqueue(chance);
                    break;

                case DrawChest:
                    Card chest = Board.CommunityChestDeck.Dequeue();
                    player.Prompter.Notify(player, chest.Description);
                    chest.Effect?.Invoke(player, this);
                    Board.CommunityChestDeck.Enqueue(chest);
                    break;

                case AskBuy ask:
                    var prop = ask.Property;
                    int response = player.Prompter.ChooseOption(player, $"Do you want to buy {prop.Name} for {prop.Price}?", new List<string>() { "Yes", "No" });
                    HandlePayment(player, null, prop.Price);
                    prop.Owner = player;
                    break;

                case GoToJail:
                    player.InJail = true;
                    //+ set the board getter to get jail dynamically later
                    player.Position = 10;
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
            amount = Math.Abs(amount);
            payer.Balance -= amount;
            //+ Maybe throw amount to bank -> ensure not negative

            if (recipient != null) recipient.Balance += amount;

            if (payer.Balance < 0)
            {
                if (payer.NetWorth < 0)
                {
                    //IsBankrupt(payer);
                    payer.IsBankrupt = true;   
                    payer.Prompter.Notify(payer, $"You have an outstanding debt of {Math.Abs(payer.Balance)}, you are now bankrupt!");
                    // Remove player somehow
                }
                else
                {
                    //Later
                    payer.Prompter.HandleBankruptcy(payer);
                }
            }

        }

        

        public void HandleTrade(Player payer, Player? recipient, int amount)
        {
            throw new NotImplementedException();
        }

        public void MovePlayer(Player player, int steps)
        {
              MovePlayerTo(player, player.Position + steps);
        }

        public void MovePlayerTo(Player player, int tileIndex)
        {
            tileIndex %= Board.BoardSize;

            // If player passed go
            if (player.Position > tileIndex)
            {
                player.Balance += 200;
                player.Prompter.Notify(player, $"You get $200!");
            }

            // Update Player position
            player.Position = tileIndex;
            var landedTile = Board[player.Position];
            player.Prompter.Notify(player, $"You have landed on {landedTile.Name}");

            // Handle landing
            HandleGameActions(landedTile.OnLand(player), player);
        }


        // Chances/Chests
        public void CollectFromAllPlayers(Player receiver, int amount)
        {
            foreach(var player in Players)
            {
                if (player != receiver)
                {
                    HandlePayment(player, receiver, amount);
                }
            }
        }

        public void PayPropertyRepairCosts(Player payer, int houseCost, int hotelCost)
        {
            foreach (var prop in payer.Properties)
            {
                if (prop.Houses == 5)
                    HandlePayment(payer, null, hotelCost);
                if (prop.Houses > 0 && prop.Houses < 5)
                    HandlePayment(payer, null, houseCost);
            }
        }

    }
}
