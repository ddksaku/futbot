using Futbot.Models;
using Futbot.Repositories;
using FutbotUI.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Windows.Input;
using FutbotUI.Utils;

namespace FutbotUI.ViewModels
{
    public class AccountDetailViewModel : ValidationViewModelBase
    {                
        #region variables

        private Types.DetialViewType viewType;        
        private AccountRepository accountRepository;

        #endregion 

        #region constructor

        public AccountDetailViewModel(Types.DetialViewType viewType)
        {
            this.viewType = viewType;
            accountRepository = new AccountRepository();            
        }

        public AccountDetailViewModel(Types.DetialViewType viewType, Account account) : this(viewType)
        {
            selectedAccount = account;

            email = account.Email;
            password = account.Password;
            securityQuestion = account.SecurityQuestion;
        }

        #endregion

        #region properties                        

        private Account selectedAccount;
        public Account SelectedAccount
        {
            get { return selectedAccount; }
            set
            {
                if (selectedAccount != value)
                {
                    selectedAccount = value;                    
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
                        okButtonCommand = new DelegateCommand(AddAccount);
                    }
                    else if (viewType == Types.DetialViewType.EDIT)
                    {
                        okButtonCommand = new DelegateCommand(EditAccount);
                    }
                }
                return okButtonCommand;
            }
        }        

        #endregion

        #region helpers

        /// <summary>
        /// add an account
        /// </summary>
        private void AddAccount()
        {
            PropertyChangedCompleted();

            // if all fields are valid
            if (this.IsValid == true) 
            {
                try
                {
                    // if already the account with the same email address exists
                    if (accountRepository.IsExistAccount(email) == true)
                    {
                        ErrorMessage = "Registered email address.";
                    }
                    else
                    {
                        selectedAccount = new Account()
                        {
                            Email = email,
                            Password = password,
                            SecurityQuestion = securityQuestion
                        };

                        accountRepository.InsertAccount(selectedAccount);
                        App.Current.MainWindow.DialogResult = true;
                        App.Current.MainWindow.Close();
                    }
                }
                catch
                {
                    ErrorMessage = "Occurred database error.";
                }
            }
            else
            {
                ErrorMessage = "Invalid account information.";
            }
        }

        /// <summary>
        /// edit an account
        /// </summary>
        private void EditAccount()
        {
            PropertyChangedCompleted();

            // if all fields are valid
            if (this.IsValid == true)
            {
                try
                {                                        
                    // if already the account with the same email address exists
                    if (accountRepository.IsUsedEmailByElse(selectedAccount.AccountId, email) == true)
                    {
                        ErrorMessage = "Registered email address.";
                    }
                    else
                    {
                        selectedAccount.Email = email;
                        selectedAccount.Password = password;
                        selectedAccount.SecurityQuestion = securityQuestion;

                        accountRepository.UpdateAccount(selectedAccount);
                        App.Current.MainWindow.DialogResult = true;
                        App.Current.MainWindow.Close();
                    }
                }
                catch
                {
                    ErrorMessage = "Occurred database error.";
                }
            }
            else
            {
                ErrorMessage = "Invalid account information.";
            }
        }

        #endregion
    }
}
