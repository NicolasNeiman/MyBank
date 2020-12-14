using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public class Transaction
    {
        public decimal Amount { get; }

        public string Note { get; }

        public DateTime Date { get; }

        public Transaction(decimal amount, DateTime date, string note)
        {
            this.Amount = amount;
            this.Note = note;
            this.Date = date;
        }
    }
}
