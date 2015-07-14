using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Futbot.Models;
using Futbot.Repositories;
using FutbotUI.Command;
using FutbotUI.Utils;
using FutbotUI.Views;
using Microsoft.Win32;

namespace FutbotUI.ViewModels
{
    public class AccountManagementViewModel : ValidationViewModelBase
    {
        #region variables

        private AccountRepository accountRepository;

        #endregion 

        #region constructor

        public AccountManagementViewModel()
        {
            accountRepository = new AccountRepository();            
            accounts = new ObservableCollection<Account>(accountRepository.GetAllAccounts());
        }

        #endregion

        #region properties

        private ObservableCollection<Account> accounts;
        public ObservableCollection<Account> Accounts
        {
            get { return accounts; }
            set
            {
                if (accounts != value)
                {
                    accounts = value;
                    base.OnPropertyChanged("Accounts");
                }
            }
        }

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

        #endregion        

        #region commands

        private DelegateCommand openAddAccountWindowCommand;
        public ICommand OpenAddAccountWindowCommand
        {
            get
            {
                if (openAddAccountWindowCommand == null)
                {
                    openAddAccountWindowCommand = new DelegateCommand(OpenAddAccountWindow);
                }
                return openAddAccountWindowCommand;
            }
        }

        private DelegateCommand bulkUploadAccountsCommand;
        public ICommand BulkUploadAccountsCommand
        {
            get
            { 
                if (bulkUploadAccountsCommand == null)
                {
                    bulkUploadAccountsCommand = new DelegateCommand(BulkUploadAccounts);
                }
                return bulkUploadAccountsCommand;
            }
        }

        private DelegateCommand testAllAccountsCommand;
        public ICommand TestAllAccountsCommand
        {
            get
            {
                if (testAllAccountsCommand == null)
                {
                    testAllAccountsCommand = new DelegateCommand(TestAllAccounts);
                }
                return testAllAccountsCommand;
            }
        }

        private DelegateCommand openEditAccountWindowCommand;
        public ICommand OpenEditAccountWindowCommand
        {
            get
            {
                if (openEditAccountWindowCommand == null)
                {
                    openEditAccountWindowCommand = new DelegateCommand(OpenEditAccountWindow);
                }
                return openEditAccountWindowCommand;
            }
        }

        private DelegateCommand deleteAccountCommand;
        public ICommand DeleteAccountCommand
        {
            get
            {
                if (deleteAccountCommand == null)
                {
                    deleteAccountCommand = new DelegateCommand(DeleteAccount);
                }
                return deleteAccountCommand;
            }
        }

        private DelegateCommand testAccountCommand;
        public ICommand TestAccountCommand
        {
            get
            {
                if (testAccountCommand == null)
                {
                    testAccountCommand = new DelegateCommand(TestAccount);
                }
                return testAccountCommand;
            }
        }

        #endregion
        
        #region helpers

        /// <summary>
        /// open the account detail window with empty account information
        /// </summary>
        private void OpenAddAccountWindow()
        {
            AccountDetailView accountDetailView = new AccountDetailView();
            AccountDetailViewModel accountDetailViewModel = new AccountDetailViewModel(Types.DetialViewType.ADD);
            accountDetailView.DataContext = accountDetailViewModel;     
  
            App.Current.MainWindow = accountDetailView;            
            bool dialogResult = (bool)accountDetailView.ShowDialog();
            if (dialogResult == true)            
            {
                accounts.Add(accountDetailViewModel.SelectedAccount);          
            }
        }

        /// <summary>
        /// import accounts from file
        /// </summary>
        private void BulkUploadAccounts()
        {            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Csv Files (*.csv)|*.csv";
            
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                IList<Account> accounts = accountRepository.GetAccounts(filePath);
                int importedCount = accountRepository.UploadAccounts(accounts);

                // refresh the accounts
                Accounts = new ObservableCollection<Account>(accountRepository.GetAllAccounts());                

                MessageBox.Show(string.Format("{0} of {1} accounts imported.", importedCount.ToString(), accounts.Count.ToString()),
                    "Import accounts", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// test connection, search and coin status of all accounts
        /// </summary>
        private void TestAllAccounts()
        {
            MessageBox.Show("TestAllAccounts: To be implmented");
        }

        /// <summary>
        /// open the account detail window with the selected+ account information
        /// </summary>
        private void OpenEditAccountWindow()
        {
            AccountDetailView accountDetailView = new AccountDetailView();
            AccountDetailViewModel accountDetailViewModel = new AccountDetailViewModel(Types.DetialViewType.EDIT, selectedAccount);
            accountDetailView.DataContext = accountDetailViewModel;    
        
            App.Current.MainWindow = accountDetailView;
            accountDetailView.ShowDialog();            
        }

        /// <summary>
        /// delete the selected account
        /// </summary>
        private void DeleteAccount()
        {
            if (MessageManager.ShowDeleteConfirmMessage() == MessageBoxResult.Yes)
            {
                accountRepository.DeleteAccount(SelectedAccount);
                accounts.Remove(SelectedAccount);                
            }
        }

        /// <summary>
        /// test connection, search and coin status of the selected account
        /// </summary>
        private void TestAccount()
        {
            MessageBox.Show("TestAccount: To be implmented");            
        }
        #endregion
    }
}
