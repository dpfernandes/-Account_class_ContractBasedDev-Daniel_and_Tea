using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;


namespace contractbyCode
{ 
    // order of collection of accounts
    [ContractClass(typeof(AccountContract))]
    public interface AccountInterface
    {
        // The Item property provides methods to withdraw and deposit to account
        // 
        void withdraw(double amount);

        void deposit(double amount);

    }

    [ContractClassFor(typeof(AccountInterface))]
    internal class AccountContract : AccountInterface
    {
        double balance;

        void AccountInterface.deposit(double amount)
        {
            //Pre-condition 
            //Exception is thrown when one of argurments provided to a method is not valid.
            Contract.Requires<ArgumentException>(amount >= 0.0);
            balance += amount;
            // Post-condition 
            Contract.Ensures(Contract.Result<double>() - amount == this.balance);

        }

        void AccountInterface.withdraw(double amount)
        {
            //Pre condition.
            Contract.Invariant(this.balance >= 0.0);
            Contract.Requires<ArgumentException>(amount >= 0.0);

            Contract.Assert(this.balance - amount >= 0.0);
            balance -= amount;

            //post-condition
            Contract.Ensures(
                 Contract.Result<double>() + amount == this.balance);

        }

        double getBalance()
        {
            return balance;
        }
    }
}