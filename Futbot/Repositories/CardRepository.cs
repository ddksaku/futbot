using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Futbot.Models;
using Futbot.Utils;

namespace Futbot.Repositories
{
    public class CardRepository
    {
        private readonly FutbotEntities context;

        /// <summary>
        /// init and open connection
        /// </summary>
        public CardRepository()
        {
            context = new FutbotEntities();
        }

        /// <summary>
        /// get all cards
        /// </summary>
        /// <returns></returns>
        public IList<Card> GetAllCards()
        {
            return context.Cards.ToList<Card>();
        }

        /// <summary>
        /// get all card types
        /// </summary>
        /// <returns></returns>
        public IList<CardType> GetAllCardTypes()
        {
            return context.CardTypes.ToList<CardType>();
        }

        /// <summary>
        /// get all chemistry mods
        /// </summary>
        /// <returns></returns>
        public IList<CardItem> GetAllChemistryMods()
        {
            return context.CardItems.Where(ci => ci.CardTypeId == (int)Types.CardType.CHEMISTRY_MOD).ToList();            
        }

        /// <summary>
        /// get all positions
        /// </summary>
        /// <returns></returns>
        public IList<CardItem> GetAllPositions()
        {
            return context.CardItems.Where(ci => ci.CardTypeId == (int)Types.CardType.POSITION).ToList();
        }

        /// <summary>
        /// get card by id
        /// </summary>
        /// <returns></returns>
        public Card GetCard(int cardId)
        {
            return context.Cards.First(c => c.CardId == cardId);
        }
        
        /// <summary>
        /// insert card
        /// </summary>
        /// <param name="card"></param>
        public void InsertCard(Card card)
        {
            context.Cards.AddObject(card);
            context.SaveChanges();
        }

        /// <summary>
        /// update card
        /// </summary>
        /// <param name="card"></param>
        public void UpdateCard(Card card)
        {
            var matchedCard = context.Cards.First(c => c.CardId == card.CardId);
            matchedCard = card;
            context.SaveChanges();
        }

        /// <summary>
        /// delete card
        /// </summary>
        /// <param name="card"></param>
        public void DeleteCard(Card card)
        {
            var matchedCard = context.Cards.First(c => c.CardId == card.CardId);
            if (matchedCard != null)
            {
                context.Cards.DeleteObject(card);
                context.SaveChanges();
            }
        }                
    }
}
