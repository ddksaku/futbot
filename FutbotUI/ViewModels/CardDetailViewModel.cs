using Futbot.Models;
using Futbot.Repositories;
using FutbotUI.Command;
using FutbotUI.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace FutbotUI.ViewModels
{
    public class CardDetailViewModel : ValidationViewModelBase
    {
        #region variables

        private Types.DetialViewType viewType;
        private CardRepository cardRepository;

        #endregion

        #region constructor

        public CardDetailViewModel(Types.DetialViewType viewType)
        {
            this.viewType = viewType;
            cardRepository = new CardRepository();
        }

        public CardDetailViewModel(Types.DetialViewType viewType, Card card)
            : this(viewType)
        {
            selectedCard = card;

            buyPrice = card.BuyPrice;
            sellPrice = card.SellPrice;
            maxPrice = card.MaxPrice;
            buyPercent = card.BuyPercent;
            sellPercent = card.SellPercent;
            excessivePercent = card.ExcessivePercent;
            maxPriceModifierPercent = card.MaxPriceModifierPercent;            

            //password = card.Password;
            //securityQuestion = card.SecurityQuestion;
        }

        #endregion

        #region properties

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

        private int? buyPrice;
        //[RegularExpression(@"^[+]?\d+([.]\d+)?$", ErrorMessage = "Invalid email address.")]
        [Range(0, Int32.MaxValue, ErrorMessage="Invalid number.")]
        public int? BuyPrice
        {
            get { return buyPrice; }
            set
            {
                if (buyPrice != value)
                {
                    buyPrice = value;
                    base.OnPropertyChanged("BuyPrice");
                }
            }
        }

        private int? sellPrice;
        [Range(0, Int32.MaxValue, ErrorMessage = "Invalid number.")]
        public int? SellPrice
        {
            get { return sellPrice; }
            set
            {
                if (sellPrice != value)
                {
                    sellPrice = value;
                    base.OnPropertyChanged("SellPrice");
                }
            }
        }

        private int? maxPrice;
        [Range(0, Int32.MaxValue, ErrorMessage = "Invalid number.")]
        public int? MaxPrice
        {
            get { return maxPrice; }
            set
            {
                if (maxPrice != value)
                {
                    maxPrice = value;
                    base.OnPropertyChanged("MaxPrice");
                }
            }
        }

        private int? buyPercent;
        [Range(0, 100, ErrorMessage = "Invalid number.")]
        public int? BuyPercent
        {
            get { return buyPercent; }
            set
            {
                if (buyPercent != value)
                {
                    buyPercent = value;
                    base.OnPropertyChanged("BuyPercent");
                }
            }
        }

        private int? sellPercent;
        [Range(0, 100, ErrorMessage = "Invalid number.")]
        public int? SellPercent
        {
            get { return sellPercent; }
            set
            {
                if (sellPercent != value)
                {
                    sellPercent = value;
                    base.OnPropertyChanged("SellPercent");
                }
            }
        }

        private int? excessivePercent;
        [Range(0, 100, ErrorMessage = "Invalid number.")]
        public int? ExcessivePercent
        {
            get { return excessivePercent; }
            set
            {
                if (excessivePercent != value)
                {
                    excessivePercent = value;
                    base.OnPropertyChanged("ExcessivePercent");
                }
            }
        }

        private int? maxPriceModifierPercent;
        [Range(0, 100, ErrorMessage = "Invalid number.")]
        public int? MaxPriceModifierPercent
        {
            get { return maxPriceModifierPercent; }
            set
            {
                if (maxPriceModifierPercent != value)
                {
                    maxPriceModifierPercent = value;
                    base.OnPropertyChanged("MaxPriceModifierPercent");
                }
            }
        }

        private string email;
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"\b[a-z0-9._%-]+@[a-z0-9.-]+\.[a-z]{2,4}\b", ErrorMessage = "Invalid email address.")]
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    base.OnPropertyChanged("Email");
                }
            }
        }

        private string password;
        [Required(ErrorMessage = "Password is required.")]
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    base.OnPropertyChanged("Password");
                }
            }
        }

        private string securityQuestion;
        [Required(ErrorMessage = "Security question is required.")]
        public string SecurityQuestion
        {
            get { return securityQuestion; }
            set
            {
                if (securityQuestion != value)
                {
                    securityQuestion = value;
                    base.OnPropertyChanged("SecurityQuestion");
                }
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                if (errorMessage != value)
                {
                    errorMessage = value;
                    base.OnPropertyChanged("ErrorMessage");
                }
            }
        }

        #endregion

        #region commands

        private DelegateCommand okButtonCommand;
        public ICommand OkButtonCommand
        {
            get
            {
                if (okButtonCommand == null)
                {
                    if (viewType == Types.DetialViewType.ADD)
                    {
                        okButtonCommand = new DelegateCommand(AddCard);
                    }
                    else if (viewType == Types.DetialViewType.EDIT)
                    {
                        okButtonCommand = new DelegateCommand(EditCard);
                    }
                }
                return okButtonCommand;
            }
        }

        #endregion

        #region helpers

        /// <summary>
        /// add an card
        /// </summary>
        private void AddCard()
        {
            PropertyChangedCompleted();

            // if all fields are valid
            if (this.IsValid == true)
            {
                //try
                //{
                //    // if already the card with the same email address exists
                //    if (cardRepository.IsExistCard(email) == true)
                //    {
                //        ErrorMessage = "Registered email address.";
                //    }
                //    else
                //    {
                //        selectedCard = new Card()
                //        {
                //            Email = email,
                //            Password = password,
                //            SecurityQuestion = securityQuestion
                //        };

                //        cardRepository.InsertCard(selectedCard);
                //        App.Current.MainWindow.DialogResult = true;
                //        App.Current.MainWindow.Close();
                //    }
                //}
                //catch
                //{
                //    ErrorMessage = "Occurred database error.";
                //}
            }
            else
            {
                ErrorMessage = "Invalid card information.";
            }
        }

        /// <summary>
        /// edit an card
        /// </summary>
        private void EditCard()
        {
            PropertyChangedCompleted();

            // if all fields are valid
            if (this.IsValid == true)
            {
                //try
                //{
                //    // if already the card with the same email address exists
                //    if (cardRepository.IsUsedEmailByElse(selectedCard.CardId, email) == true)
                //    {
                //        ErrorMessage = "Registered email address.";
                //    }
                //    else
                //    {
                //        selectedCard.Email = email;
                //        selectedCard.Password = password;
                //        selectedCard.SecurityQuestion = securityQuestion;

                //        cardRepository.UpdateCard(selectedCard);
                //        App.Current.MainWindow.DialogResult = true;
                //        App.Current.MainWindow.Close();
                //    }
                //}
                //catch
                //{
                //    ErrorMessage = "Occurred database error.";
                //}
            }
            else
            {
                ErrorMessage = "Invalid card information.";
            }
        }

        #endregion
    }

}
