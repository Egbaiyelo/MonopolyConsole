using MonopolyConsole.Core.Board;
using MonopolyConsole.Core.Interfaces;
using MonopolyConsole.Core.Models;

namespace MonopolyConsole.Core.Services
{
    public class GameEngine : IGameEngine
    {

        DiceRoller DiceRoller;

        

        public GameEngine() { }

        public void SetupGame()
        {
            //Tiles, cards
        }

        public Tile getTile()
        {

        }

        public void HandlePayment(Player payer, Player? recipient, int amount)
        {
            if (payer.Balance >= amount)
            {
                payer.Balance -= amount;
                recipient?.Balance += amount;
                return;
            }

            // Player cannot afford it
            _prompter.Notify(payer, $"You need ${amount}, but you only have ${payer.Balance}.");

            while (payer.Balance < amount)
            {
                _prompter.PromptSellOrMortgage(payer, amount - payer.Balance);

                if (payer.Balance >= amount)
                    break;

                if (_prompter.Confirm(payer, "Declare bankruptcy?"))
                {
                    HandleBankruptcy(payer, recipient);
                    return;
                }
            }

            // Now they can pay
            payer.Balance -= amount;
            recipient?.Balance += amount;
        }


        public void CollectFromAllPlayers(Player receiver, int amount)
        {
            foreach (var other in _players.Where(p => p != receiver))
            {
                HandlePayment(other, receiver, amount);
            }
        }




    }
}
