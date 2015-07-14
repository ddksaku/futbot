using System;
using Futbot.Models;
using Futbot.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FutbotTest
{
    [TestClass]
    public class AccountTest
    {
        private AccountRepository accountRepository;

        public AccountTest()
        {            
            accountRepository = new AccountRepository();
        }

        [TestMethod]
        public void TestGetAllAccounts()
        {
            int accountCount = accountRepository.GetAllAccounts().Count;
            Assert.AreEqual(accountCount, 1);
        }
        
        [TestMethod]
        public void TestInsertAccount()
        {
            Account account = new Account
            {
                Email = "insertTest@gmail.com",
                Password = "insertTest",
                SecurityQuestion = "Insert"
            };

            accountRepository.InsertAccount(account);
        }

        [TestMethod]
        public void TestUpdateAccount()
        {
            Account account = accountRepository.GetAccount(3);
            account.Email = "updateTest@gmail.com";
            accountRepository.UpdateAccount(account);
        }        

        [TestMethod]
        public void TestDeleteAccount()
        {
            Account account = accountRepository.GetAccount(2);
            accountRepository.DeleteAccount(account);
        }

        [TestMethod]
        public void TestIsExistAccount()
        {
            bool isExistAccount = accountRepository.IsExistAccount("ddksaku@gmail.com");
            Assert.AreEqual(isExistAccount, true);
        }        

        [TestMethod]
        public void TestGetAccounts()
        {
            IList<Account> accounts = accountRepository.GetAccounts(@"d:\accounts.csv");
            int accountCount = accounts.Count;
            Assert.AreEqual(accountCount, 2);            
        }                
    }
}
