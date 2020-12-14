using System;

namespace MyBank
{
    class Program
    {
        static void Main(string[] args)
        {
            var NicoAccount = new BankAccount("Nicolas", 1000);
            Console.WriteLine($"Bank account Number : {NicoAccount.AccountNumber} belongs to : {NicoAccount.Owner} and current balance is {NicoAccount.Balance}");
            NicoAccount.MakeDeposit(100);
            Console.WriteLine($"{NicoAccount.Balance}");
            NicoAccount.MakeWithdrawal(200);
            Console.WriteLine($"{NicoAccount.Balance}");
            try
            {
                NicoAccount.MakeWithdrawal(1000);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine(NicoAccount.GetAccountHistory());
        }
    }
}
