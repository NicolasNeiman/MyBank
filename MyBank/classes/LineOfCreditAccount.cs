using System;
using System.Collections.Generic;
using System.Text;

namespace MyBank
{
    class LineOfCreditAccount : BankAccount
    {
        decimal CreditLimit { get; }
        public LineOfCreditAccount(string name, decimal initialAmount, decimal creditLimit) : base(name, initialAmount) 
        {
            this.CreditLimit = creditLimit;
        }


        public override void PerformMonthEndTransactions()
        {
            if (Balance < 0)
            {
                decimal interestAmount = -Balance * 0.05m;
                MakeWithdrawal(
                    amount: interestAmount,
                    note: "Interest fees covering your negative balance"
                );
            }
        }

        protected override Transaction? CheckWithdrawalLimit(bool isOverDrawn)
        {
            if( isOverDrawn )
            {
                return new Transaction(
                    amount: -20,
                    note: "Fees for transaction excedding your minimum balance",
                    date: DateTime.Now);
            }
            else
            {
                return default;
            }
        }
    }
}
