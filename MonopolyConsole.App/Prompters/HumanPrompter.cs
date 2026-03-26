using MonopolyConsole.Core.BoardComponents;
using MonopolyConsole.Core.Interfaces;
using MonopolyConsole.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.App.Prompters
{
    internal class HumanConsolePrompter : IPrompter
    {
        // Confirm player action
        public bool Confirm(Player player, string message)
        {
            Console.WriteLine($"{player.Name}, {message} (y/n)?");
            return Console.ReadLine()?.ToLower() == "y";
        }

        public int ChooseOption(Player player, string prompt, List<string> options)
        {
            Console.WriteLine(prompt);
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i} => " + options[i]);
            }

            int choice = 0;
            do
            {
                Console.WriteLine($"What do you choose {0} - {options.Count - 1} =>");
                string input = Console.ReadLine();
                choice = int.Parse(input);
            } while (choice < 0 || choice > options.Count());

            return choice;
        }

        public string ReturnOption(Player player, string prompt, List<string> options)
        {
            return options[ ChooseOption(player, prompt, options) ];
        }


        // Notify player of something
        public void Notify(Player player, string message)
        {
            Console.WriteLine($"{player.Name}: {message}");
        }

        public void HandleBankruptcy(Player player)
        {

        }

        public void PromptSellOrMortgage(Player player, int amountNeeded)
        {
            throw new NotImplementedException();
        }

        public bool DecideToBuy(Player player, Property property)
        {
            return false;
        }

        public Property? GetPropertyToSellOrMortgage(Player player, decimal requiredCash)
        {
            throw new NotImplementedException();
        }

        //+ could technically return list of actions but need to process them first so nothing bad happens (state is always updated)
        public PlayerAction Decision(Player player)
        {
            // Options
            const string endTurn = "End Turn";
            const string trade = "Trade with a player";
            const string manageProperty = "Manage property";


            var options = new List<string>()
            {
                endTurn,
                trade,
            };
            string response = "";

   
            if (player.Properties.Count > 0)
                options.Add(manageProperty);

            response = ReturnOption(player, "What will you like to do?", options);

            switch (response)
            {
                case endTurn:
                    return new EndTurn();
                    break;

                case trade:
                    //+ Implement
                    return new EndTurn();
                    break;

                case manageProperty:
                    return GetPropertyToManage(player);
                    break;
                default:
                    return new EndTurn();
            }

        }

        public PlayerAction GetPropertyToManage(Player player)
        {
            // Options
            const string mortgage = "Mortgage";
            const string unMortgage = "unMorgage";
            const string buildHouse = "Build House";
            const string sellHouse = "Sell House";

            if (player.Properties.Count < 1)
            {
                Notify(player, $"You have no properties to manage");
                return new EndTurn();
            }

            var options = player.Properties.Select(p => p.Name).ToList();
            int response = ChooseOption(player, "Which property do you want to manage?", options);

            Property subj = player.Properties[response];
            var manageoptions = new List<string>() {};

            if (subj.Houses < 5 && player.Balance > 50)
                manageoptions.Add(buildHouse);
            if (subj.Houses > 0)
                manageoptions.Add(sellHouse);

            if (subj.IsMortgaged && player.Balance > (int)(subj.Price / 2) + 10)
                manageoptions.Add(unMortgage);
            if (!subj.IsMortgaged && subj.Houses == 0)
                manageoptions.Add(mortgage);

            string manageResponse = ReturnOption(player, $"What do you want to do to {subj.Name} => ", manageoptions);
            switch (manageResponse)
            {
                case mortgage:
                    return new Mortgage(subj);
                    break;
                case unMortgage:
                    return new UnMortgage(subj);
                    break;
                case buildHouse:
                    return new BuildHouse(subj);
                    break;
                case sellHouse:
                    return new SellHouse(subj);
                    break;
            }

            return new EndTurn();
        }
    }
}
