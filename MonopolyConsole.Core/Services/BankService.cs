using MonopolyConsole.Core.Board;
using MonopolyConsole.Core.Interfaces;
using MonopolyConsole.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Core.Services
{
    internal class BankService
    {

        private readonly IGameEngine _engine;
        private readonly IPrompter _prompter;

        public BankService(IGameEngine engine, IPrompter prompter)
        {
            _engine = engine;
            _prompter = prompter;
        }

        // Money
        public void HandlePayment(Player payer, Player? recipient, int amount) 
        { 
        }
        public void CollectFromAllPlayers(Player receiver, int amount) 
        {
        }
        public void DeclareBankruptcy(Player payer, Player? creditor) 
        { 
        }

        // Properties
        public void BuyProperty(Player player, Property property) 
        { 
        }
        public void MortgageProperty(Player player, Property property) 
        { 
        }
        public void UnmortgageProperty(Player player, Property property) 
        { 
        }

        // Trading
        public void InitiateTrade(Player proposer) 
        { 
        }
    }
}
