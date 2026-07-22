using Bogus;
using MonopolyConsole.Core.BoardComponents;
using MonopolyConsole.Core.Interfaces;
using MonopolyConsole.Core.Models;
using MonopolyConsole.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Test
{
    public class MockDiceRoller : IDiceRoller
    {
        public int Roll1;
        public int Roll2;

        public MockDiceRoller(int roll1, int roll2)
        {
            Roll1 = roll1;
            Roll2 = roll2;
        }

        public DiceResult Roll() => new DiceResult(Roll1, Roll2);
    }

    public class MockPlayerList
    {
        public List<Player> GetPlayers(int num = 5)
        {
            var faker = new Faker<Player>()
                .RuleFor(p => p.Name, f => f.Name.FullName());

            var fakePlayers = faker.Generate(num);

            return fakePlayers;
        }
    }

    public class MockPrompter : IPrompter
    {
        public object NextDecision { get; set; } 

        public int ChooseOption(Player player, string prompt, List<string> options)
        {
            return (int) NextDecision;
        }

        public bool Confirm(Player player, string message)
        {
            return (bool) NextDecision;
        }

        public bool DecideToBuy(Player player, Property property)
        {
            return (bool) NextDecision;
        }

        public PlayerAction Decision(Player player)
        {
            return (PlayerAction) NextDecision;
        }

        public Property? GetPropertyToSellOrMortgage(Player player, decimal requiredCash)
        {
            return (Property?) NextDecision;
        }

        public void HandleBankruptcy(Player player)
        {
            throw new NotImplementedException();
        }

        public void Notify(Player player, string message)
        {
            // void
        }

        public void PromptSellOrMortgage(Player player, int amountNeeded)
        {
            throw new NotImplementedException();
        }
    }

    
}
