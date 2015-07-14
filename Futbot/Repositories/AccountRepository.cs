using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Futbot.Models;
using Futbot.Utils;

namespace Futbot.Repositories
{
    public class AccountRepository
    {
        private readonly FutbotEntities context;

        /// <summary>
        /// init and open connection
        /// </summary>
        public AccountRepository()
        {
            context = new FutbotEntities();
        }

        /// <summary>
        /// get all accounts
        /// </summary>
        /// <returns></returns>
        public IList<Account> GetAllAccounts()
        {
            return context.Accounts.ToList<Account>();
        }

        /// <summary>
        /// get an account by id
        /// </summary>
        /// <returns></returns>
        public Account GetAccount(int accountId)
        {
            return context.Accounts.First(a => a.AccountId == accountId);
        }

        /// <summary>
        /// get the first account matched to the email address
        /// </summary>
        /// <returns></returns>
        public Account GetAccount(string emailAddress)
        {
            return context.Accounts.FirstOrDefault(a => a.Email.ToUpper() == emailAddress.ToUpper());            
        }

        /// <summary>
        /// insert an account
        /// </summary>
        /// <param name="account"></param>
        public void InsertAccount(Account account)
        {            
            context.Accounts.AddObject(account);
            context.SaveChanges();            
        }

        /// <summary>
        /// update an account
        /// </summary>
        /// <param name="account"></param>
        public void UpdateAccount(Account account)
        {            
            var matchedAccount = context.Accounts.First(a => a.AccountId == account.AccountId);
            matchedAccount = account;            
            context.SaveChanges();
        }

        /// <summary>
        /// delete an account
        /// </summary>
        /// <param name="account"></param>
        public void DeleteAccount(Account account)
        {
            var matchedAccount = context.Accounts.First(a => a.AccountId == account.AccountId);
            if (matchedAccount != null)
            {
                context.Accounts.DeleteObject(account);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// check if an account already exists with the same email address
        /// </summary>
        /// <returns></returns>
        public bool IsExistAccount(string emailAddress)
        {
            var matchedAccount = context.Accounts.FirstOrDefault(a => a.Email.ToUpper() == emailAddress.ToUpper());
            return matchedAccount == null ? false : true;
        }

        /// <summary>
        /// check if the email address is used by someone else
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public bool IsUsedEmailByElse(int accountId, string emailAddress)
        {
            var matchedAccount = context.Accounts.FirstOrDefault(a => a.AccountId != accountId && a.Email.ToUpper() == emailAddress.ToUpper());
            return matchedAccount == null ? false : true;
        }

        /// <summary>
        /// read accounts from csv file
        /// csv file's structure is following
        /// Email, Password, SecurityQuestion
        /// </summary>
        /// <param name="csvFilePath"></param>
        /// <returns></returns>
        public IList<Account> GetAccounts(string csvFilePath)
        {
            IList<Account> accounts = new List<Account>();
            CSVHelper csvHelper = new CSVHelper(csvFilePath);
            foreach (string[] line in csvHelper)
            {
                Account account = new Account()
                {                    
                    Email = line[0],
                    Password = line[1],
                    SecurityQuestion = line[2]
                };
                accounts.Add(account);
            }
            return accounts;
        }

        /// <summary>
        /// upload accounts to database 
        /// </summary>
        /// <param name="accounts"></param>
        /// <returns></returns>
        public int UploadAccounts(IList<Account> accounts)
        {
            int importedCount = 0;
            foreach (Account account in accounts)
            {
                try
                {
                    if (IsExistAccount(account.Email) == true) // the same email address already exists, need to update
                    {
                        Account updateAccount = GetAccount(account.Email);
                        updateAccount.Password = account.Password;
                        updateAccount.SecurityQuestion = account.SecurityQuestion;

                        UpdateAccount(updateAccount);
                    }
                    else // need to insert
                    {
                        InsertAccount(account);
                    }

                    importedCount++;
                }
                catch
                {
                }
            }
            return importedCount;
        }
    }
}
