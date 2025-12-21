using MonopolyConsole.Core.BoardComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Core.Models
{
    public abstract record GameAction;

    // Maybe remove player from arguements
    public record PayTax(int Amount) : GameAction;
    public record PayRent(Property Property) : GameAction;
    public record DrawChance() : GameAction;
    public record DrawChest() : GameAction;
    public record AskBuy(Property Property) : GameAction;
    public record GoToJail() : GameAction;
    public record Notify(string Message) : GameAction;
    public record Nothing() : GameAction;
    
}
