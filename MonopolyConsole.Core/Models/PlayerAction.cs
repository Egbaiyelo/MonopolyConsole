using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonopolyConsole.Core.BoardComponents;

namespace MonopolyConsole.Core.Models
{
    public abstract record PlayerAction;

    public record Buy(Property p) : PlayerAction;
    public record Mortgage(Property p) : PlayerAction;
    public record Sell(Property p) : PlayerAction;
    public record EndTurn(Property p) : PlayerAction;
    public record Trade(Player p) : PlayerAction;


    internal class PlayerActionStrategy
    {
        public enum PlayerAction { Buy, Mortgage, Sell, EndTurn, Trade }

        public interface IPlayerStrategy
        {
            // 1. The Core calls this method to ask the player what to do.
            PlayerAction GetAction(Player player, Board board);

            // 2. The Core calls this method if the player lands on a property they can buy.
            bool DecideToBuy(Property property, Player player);

            // 3. The Core calls this method when a player needs to raise money (e.g., to pay rent).
            // Returns the property the player chooses to mortgage/sell, or null if they can't/won't.
            Property? GetPropertyToSellOrMortgage(Player player, decimal requiredCash);
        }

    }
}
