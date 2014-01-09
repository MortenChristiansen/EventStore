using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    public sealed class Money
    {
        public decimal Amount { get; private set; }
        public Currency Currency { get; private set; }

        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public Money Add(Money money)
        {
            if (money.Currency != Currency)
                throw new ArgumentException();

            return new Money(Amount + money.Amount, Currency);
        }

        public override bool Equals(object obj)
        {
            var money = obj as Money;
            if (money == null)
                return false;

            return Amount == money.Amount && Currency == money.Currency;
        }

        public override int GetHashCode()
        {
            return Amount.GetHashCode() ^ Currency.GetHashCode();
        }
    }
}
