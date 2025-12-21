using MonopolyConsole.Core.BoardComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Core.Models.GameActions
{
    public abstract record GameAction;

    public record PayTax(Player Player, int Amount) : GameAction;
    public record PayRent(Player Player, Property Property) : GameAction;
    public record DrawChance(Player Player) : GameAction;
    public record DrawChest(Player Player) : GameAction;
    public record AskBuy(Player Player, Property Property) : GameAction;
    public record GoToJail(int Player) : GameAction;
    public record Notify(Player Player, string Message) : GameAction;
    public record Nothing() : GameAction;
    
}
