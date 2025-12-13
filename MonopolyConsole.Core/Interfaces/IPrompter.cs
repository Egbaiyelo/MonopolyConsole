using MonopolyConsole.Core.Board;
using MonopolyConsole.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.App
{
    public interface IPrompter
    {
        public enum PlayerAction { Buy, Mortgage, Sell, EndTurn, None, Trade }


        // Confirm selection?
        bool Confirm(Player player, string message);
        int ChooseOption(Player player, string prompt, List<string> options);
        // Notify player about something
        void Notify(Player player, string message);



        // Money-related
        void HandleBankruptcy(Player player);
        void PromptSellOrMortgage(Player player, int amountNeeded);



        // Trading 
        //TradeProposal? PromptTrade(Player initiator);

        // The Core calls this method if the player lands on a property they can buy
        bool DecideToBuy(Property property, Player player);

        // The Core calls this method when a player needs to raise money (e.g., to pay rent).
        // Returns the property the player chooses to mortgage/sell, or null if they can't/won't.
        Property? GetPropertyToSellOrMortgage(Player player, decimal requiredCash);
    }

   
}
