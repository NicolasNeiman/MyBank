using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public class BankAccount
    {
        public string AccountNumber { get; }

        public string Owner { get; set; }

        public decimal Balance 
        { 
            get
            {
                decimal tempBalance = 0M;
                foreach (var transaction in allTransactions)
                {
                    tempBalance += transaction.Amount;
                }
                return tempBalance; 
            }
        }

        public List<Transaction> allTransactions = new List<Transaction>();

        private static int seedBankAccountNumber = 1234567890;

        private readonly decimal minimumBalance;

        public BankAccount(string owner, decimal initialBalance) : this(owner, initialBalance, 0) { }
        public BankAccount(string owner, decimal initialBalance, decimal minimumBalance)
        {
            this.AccountNumber = seedBankAccountNumber.ToString();
            seedBankAccountNumber++;
            
            this.Owner = owner;
            this.minimumBalance = minimumBalance;

            if (initialBalance > 0)
            {
                MakeDeposit(
                    amount: initialBalance,
                    note: "Initial deposit at account creation"
                );
            }
        }

        public void MakeDeposit(decimal amount, string note = "")
        {
            if(amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount should be greater than zero");
            }

            string depositNote = note;
            if (depositNote == "")
            {
                depositNote = $"{amount} deposited on your account";
            }

            var deposit = new Transaction(
                amount: amount,
                date: DateTime.Now,
                note: depositNote
            );

            allTransactions.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount, string note = "")
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount should be greater than zero");
            }
            // if the amount exceed the authorized amount the next line will raise an InvalidoperationException
            var overdraftTransaction = CheckWithdrawalLimit(Balance - amount < minimumBalance);

            string withdrawNote = note;
            if (withdrawNote == "")
            {
                withdrawNote = $"{amount} withdrew from your account";
            }

            var withdrawal = new Transaction(
                amount: -amount,
                date: DateTime.Now,
                note: withdrawNote
            );

            allTransactions.Add(withdrawal);

            // This never happen in the normal BankAccount
            if (overdraftTransaction != null)
                allTransactions.Add(overdraftTransaction);
        }

        protected virtual Transaction CheckWithdrawalLimit(bool isOverDrawn)
        {
            if (isOverDrawn)
            {
                throw new InvalidOperationException("Can't proceed with this operation given your balance and minimum balance limit");
            }
            else
            {
                return default;
            }
        }

        public string GetAccountHistory()
        {
            var report = new StringBuilder();
            decimal balance = 0;
            report.AppendLine("Date\t\tAmount\tBalance\tNote");
            foreach (Transaction transaction in allTransactions)
            {
                balance += transaction.Amount;
                report.AppendLine($"{transaction.Date.ToShortDateString()}\t{transaction.Amount}\t{balance}\t{transaction.Note}");
            }
            return report.ToString();
        }

        public virtual void PerformMonthEndTransactions() { }
    }
}
