using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public class InterestEarningAccount : BankAccount
    {
        public InterestEarningAccount(string name, decimal initialBalance) : base(name, initialBalance)
        {
        }

        public override void PerformMonthEndTransactions()
        {
            if (Balance > 0)
            {
                decimal interestEarningsAmount = Balance * 0.02m;
                MakeDeposit(
                    amount: interestEarningsAmount,
                    note: "Monthly interest earnings"
                );
            }
        }
    }
}
