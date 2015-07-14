using Futbot.Models;
using Futbot.Repositories;
using FutbotUI.Command;
using FutbotUI.Utils;
using FutbotUI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace FutbotUI.ViewModels
{
    public class CardManagementViewModel : ValidationViewModelBase
    {
        #region variables

        private CardRepository cardRepository;

        #endregion 

        #region constructor

        public CardManagementViewModel()
        {
            cardRepository = new CardRepository();                    
            cards = new ObservableCollection<Card>(cardRepository.GetAllCards());
        }

        #endregion

        #region properties

        private ObservableCollection<Card> cards;
        public ObservableCollection<Card> Cards
        {
            get { return cards; }
            set
            {
                if (cards != value)
                {
                    cards = value;
                    base.OnPropertyChanged("Cards");
                }
            }
        }

        private Card selectedCard;
        public Card SelectedCard
        {
            get { return selectedCard; }
            set
            {
                if (selectedCard != value)
                {
                    selectedCard = value;                    
                }
            }
        }

        #endregion        

        #region commands

        private DelegateCommand openAddCardWindowCommand;
        public ICommand OpenAddCardWindowCommand
        {
            get
            {
                if (openAddCardWindowCommand == null)
                {
                    openAddCardWindowCommand = new DelegateCommand(OpenAddCardWindow);
                }
                return openAddCardWindowCommand;
            }
        }

        private DelegateCommand deleteAllCardsCommand;
        public ICommand DeleteAllCardsCommand
        {
            get
            {
                if (deleteAllCardsCommand == null)
                {
                    deleteAllCardsCommand = new DelegateCommand(DeleteAllCards);
                }
                return deleteAllCardsCommand;
            }
        }

        private DelegateCommand checkAllBuySellPricesCommand;
        public ICommand CheckAllBuySellPricesCommand
        {
            get
            {
                if (checkAllBuySellPricesCommand == null)
                {
                    checkAllBuySellPricesCommand = new DelegateCommand(CheckAllBuySellPrices);
                }
                return checkAllBuySellPricesCommand;
            }
        }

        private DelegateCommand checkAllMaxPricesCommand;
        public ICommand CheckAllMaxPricesCommand
        {
            get
            {
                if (checkAllMaxPricesCommand == null)
                {
                    checkAllMaxPricesCommand = new DelegateCommand(CheckAllMaxPrices);
                }
                return checkAllMaxPricesCommand;
            }
        }

        private DelegateCommand priceValidationCommand;
        public ICommand PriceValidationCommand
        {
            get
            {
                if (priceValidationCommand == null)
                {
                    priceValidationCommand = new DelegateCommand(PriceValidation);
                }
                return priceValidationCommand;
            }
        }

        private DelegateCommand importCardsCommand;
        public ICommand ImportCardsCommand
        {
            get
            {
                if (importCardsCommand == null)
                {
                    importCardsCommand = new DelegateCommand(ImportCards);
                }
                return importCardsCommand;
            }
        }

        private DelegateCommand exportCardsCommand;
        public ICommand ExportCardsCommand
        {
            get
            {
                if (exportCardsCommand == null)
                {
                    exportCardsCommand = new DelegateCommand(ExportCards);
                }
                return exportCardsCommand;
            }
        }

        private DelegateCommand openEditCardWindowCommand;
        public ICommand OpenEditCardWindowCommand
        {
            get
            {
                if (openEditCardWindowCommand == null)
                {
                    openEditCardWindowCommand = new DelegateCommand(OpenEditCardWindow);
                }
                return openEditCardWindowCommand;
            }
        }

        private DelegateCommand deleteCardCommand;
        public ICommand DeleteCardCommand
        {
            get
            {
                if (deleteCardCommand == null)
                {
                    deleteCardCommand = new DelegateCommand(DeleteCard);
                }
                return deleteCardCommand;
            }
        }

        private DelegateCommand checkBuySellPriceCommand;
        public ICommand CheckBuySellPriceCommand
        {
            get
            {
                if (checkBuySellPriceCommand == null)
                {
                    checkBuySellPriceCommand = new DelegateCommand(CheckBuySellPrice);
                }
                return checkBuySellPriceCommand;
            }
        }

        private DelegateCommand checkMaxPriceCommand;
        public ICommand CheckMaxPriceCommand
        {
            get
            {
                if (checkMaxPriceCommand == null)
                {
                    checkMaxPriceCommand = new DelegateCommand(CheckMaxPrice);
                }
                return checkMaxPriceCommand;
            }
        }

        #endregion
        
        #region helpers

        /// <summary>
        /// open the card detail window with empty card information
        /// </summary>
        private void OpenAddCardWindow()
        {
            CardDetailView cardDetailView = new CardDetailView();
            CardDetailViewModel cardDetailViewModel = new CardDetailViewModel(Types.DetialViewType.ADD);
            cardDetailView.DataContext = cardDetailViewModel;

            App.Current.MainWindow = cardDetailView;
            bool dialogResult = (bool)cardDetailView.ShowDialog();
            //if (dialogResult == true)
            //{
            //    cards.Add(cardDetailViewModel.SelectedCard);
            //}
        }

        /// <summary>
        /// delete all cards
        /// </summary>
        private void DeleteAllCards()
        {
            MessageBox.Show("DeleteAllCards: To be implemented.");
        }

        /// <summary>
        /// check all the Buy and Sell prices of the cards present in the database but not the maximum prices as that´s a different process
        /// </summary>
        private void CheckAllBuySellPrices()
        {
            MessageBox.Show("CheckAllBuySellPrices: To be implemented.");
        }

        /// <summary>
        /// calculate the maximum prices for all the cards in the database
        /// </summary>
        private void CheckAllMaxPrices()
        {
            MessageBox.Show("CheckAllMaxPrices: To be implemented.");
        }

        /// <summary>
        /// carry out two different checks and mark in red bold the whole row that don't pass them
        /// </summary>
        private void PriceValidation()
        {
            MessageBox.Show("PriceValidation: To be implemented.");
        }

        /// <summary>
        /// import cards
        /// </summary>
        private void ImportCards()
        {
            MessageBox.Show("ImportCards: To be implemented.");
        }

        /// <summary>
        /// export cards
        /// </summary>
        private void ExportCards()
        {
            MessageBox.Show("ExportCards: To be implemented.");
        }

        /// <summary>
        /// open the card detail window with the selected card information
        /// </summary>
        private void OpenEditCardWindow()
        {
            CardDetailView cardDetailView = new CardDetailView();
            CardDetailViewModel cardDetailViewModel = new CardDetailViewModel(Types.DetialViewType.EDIT, selectedCard);
            cardDetailView.DataContext = cardDetailViewModel;

            App.Current.MainWindow = cardDetailView;
            cardDetailView.ShowDialog();
        }

        /// <summary>
        /// delete the selected card
        /// </summary>
        private void DeleteCard()
        {
            if (MessageManager.ShowDeleteConfirmMessage() == MessageBoxResult.Yes)
            {
                cardRepository.DeleteCard(SelectedCard);
                cards.Remove(SelectedCard);
            }
        }

        /// <summary>
        /// check the price of the selected row
        /// </summary>
        private void CheckBuySellPrice()
        {
            MessageBox.Show("CheckBuySellPrice: To be implemented.");
        }

        /// <summary>
        /// calculate the maximum price for the selected row
        /// </summary>
        private void CheckMaxPrice()
        {
            MessageBox.Show("CheckMaxPrice: To be implemented.");
        }
        
        #endregion
    }
}
