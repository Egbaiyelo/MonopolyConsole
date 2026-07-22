using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyConsole.Core.Interfaces;
using MonopolyConsole.Core.Models;
using MonopolyConsole.Data; 
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyConsole.Test
{
    [TestClass]
    public class CardTests
    {
        private Mock<IGameEngine> _mockEngine = null!;
        private GameDataService _dataService = null!;
        private Player _testPlayer = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockEngine = new Mock<IGameEngine>();
            _dataService = new GameDataService();
            _testPlayer = new Player("TestPlayer") { Balance = 1000, NoJailFreeCards = 0 };
        }

        #region Card Core Tests

        [TestMethod]
        public void Card_Constructor_AssignsPropertiesCorrectly()
        {
            // Arrange & Act
            Action<Player, IGameEngine> effect = (p, g) => p.Balance += 100;
            var card = new Card(Card.CardType.Chance, "Collect $100", effect, true);

            // Assert
            Assert.AreEqual("Collect $100", card.Description);
            Assert.IsTrue(card.IsImmediate);
            Assert.IsNotNull(card.Effect);
        }

        [TestMethod]
        public void Activate_ExecutesCardEffect()
        {
            // Arrange
            var card = new Card(Card.CardType.Chance, "Collect $100", (p, g) => p.Balance += 100, true);

            // Act
            card.Activate(_testPlayer, _mockEngine.Object);

            // Assert
            Assert.AreEqual(1100, _testPlayer.Balance);
        }

        #endregion

        #region Community Chest Card Tests

        [TestMethod]
        public void CommunityChest_DirectBalanceCard_ModifiesPlayerBalance()
        {
            // Arrange
            List<Card> cards = _dataService.LoadCommunityChestCards();
            Card inheritCard = cards.First(c => c.Description.Contains("inherit $100"));

            // Act
            inheritCard.Activate(_testPlayer, _mockEngine.Object);

            // Assert
            Assert.AreEqual(1100, _testPlayer.Balance);
        }

        [TestMethod]
        public void CommunityChest_GetOutOfJailFree_IncrementsPlayerInventory()
        {
            // Arrange
            List<Card> cards = _dataService.LoadCommunityChestCards();
            Card jailCard = cards.First(c => c.Description == "Get out of Jail Free");

            // Act
            jailCard.Activate(_testPlayer, _mockEngine.Object);

            // Assert
            Assert.AreEqual(1, _testPlayer.NoJailFreeCards);
            Assert.IsFalse(jailCard.IsImmediate); // Should be false so player can hold it
        }

        [TestMethod]
        public void CommunityChest_MoveToGO_TriggersEngineMovePlayerTo()
        {
            // Arrange
            List<Card> cards = _dataService.LoadCommunityChestCards();
            Card goCard = cards.First(c => c.Description.Contains("Advance to GO"));

            // Act
            goCard.Activate(_testPlayer, _mockEngine.Object);

            // Assert
            _mockEngine.Verify(g => g.MovePlayerTo(_testPlayer, 0), Times.Once);
        }

        [TestMethod]
        public void CommunityChest_DoctorFees_TriggersEngineHandlePayment()
        {
            // Arrange
            List<Card> cards = _dataService.LoadCommunityChestCards();
            Card feeCard = cards.First(c => c.Description.Contains("Doctor’s fees"));

            // Act
            feeCard.Activate(_testPlayer, _mockEngine.Object);

            // Assert
            _mockEngine.Verify(g => g.HandlePayment(_testPlayer, null, 50), Times.Once);
        }

        [TestMethod]
        public void CommunityChest_GrandOpera_TriggersCollectFromAllPlayers()
        {
            // Arrange
            List<Card> cards = _dataService.LoadCommunityChestCards();
            Card operaCard = cards.First(c => c.Description.Contains("Grand Opera Night"));

            // Act
            operaCard.Activate(_testPlayer, _mockEngine.Object);

            // Assert
            _mockEngine.Verify(g => g.CollectFromAllPlayers(_testPlayer, 50), Times.Once);
        }

        [TestMethod]
        public void CommunityChest_StreetRepairs_TriggersPayPropertyRepairCosts()
        {
            // Arrange
            List<Card> cards = _dataService.LoadCommunityChestCards();
            Card repairCard = cards.First(c => c.Description.Contains("street repairs"));

            // Act
            repairCard.Activate(_testPlayer, _mockEngine.Object);

            // Assert
            _mockEngine.Verify(g => g.PayPropertyRepairCosts(_testPlayer, 40, 115), Times.Once);
        }

        #endregion

        #region Chance Card Tests

        [TestMethod]
        public void Chance_GoToJail_QueriesIndexAndMovesPlayer()
        {
            // Arrange
            _mockEngine.Setup(e => e.GetTileIndex("jail")).Returns(10);
            List<Card> cards = _dataService.LoadChanceCards();
            Card jailCard = cards.First(c => c.Description == "Go to jail");

            // Act
            jailCard.Activate(_testPlayer, _mockEngine.Object);

            // Assert
            _mockEngine.Verify(e => e.GetTileIndex("jail"), Times.Once);
            _mockEngine.Verify(e => e.MovePlayerTo(_testPlayer, 10), Times.Once);
        }

        [TestMethod]
        public void Chance_GoThreeStepsBack_TriggersRelativeMove()
        {
            // Arrange
            List<Card> cards = _dataService.LoadChanceCards();
            Card backCard = cards.First(c => c.Description.Contains("three steps back"));

            // Act
            backCard.Activate(_testPlayer, _mockEngine.Object);

            // Assert
            _mockEngine.Verify(e => e.MovePlayer(_testPlayer, -3), Times.Once);
        }

        #endregion
    }
}
