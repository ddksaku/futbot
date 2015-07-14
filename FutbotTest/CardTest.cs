using System;
using Futbot.Models;
using Futbot.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FutbotTest
{
    [TestClass]
    public class CardTest
    {
        private CardRepository cardRepository;

        public CardTest()
        {
            cardRepository = new CardRepository();
        }

        [TestMethod]
        public void TestGetAllCards()
        {
            int cardCount = cardRepository.GetAllCards().Count;
            Assert.AreEqual(cardCount, 1);
        }

        [TestMethod]
        public void TestGetAllChemistryMods()
        {
            int cardCount = cardRepository.GetAllChemistryMods().Count;
            Assert.AreEqual(cardCount, 2);
        }

        [TestMethod]
        public void TestInsertCard()
        {
            Card card = new Card
            {
                Name = "TestCard"                
            };

            cardRepository.InsertCard(card);
        }

        [TestMethod]
        public void TestUpdateCard()
        {
            Card card = cardRepository.GetCard(3);
            card.Name = "UpdateTestCard";
            cardRepository.UpdateCard(card);
        }

        [TestMethod]
        public void TestDeleteCard()
        {
            Card card = cardRepository.GetCard(2);
            cardRepository.DeleteCard(card);
        }        
    }
}
