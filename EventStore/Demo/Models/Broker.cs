using Demo.DomainEvents;
using EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Broker
    {
        public string Name { get; private set; }
        public Money Funds { get; private set; }
        public Portfolio Portfolio { get; private set; }

        public Broker(string name)
        {
            Name = name;
            Funds = new Money(0, new Currency("USD"));
            Portfolio = new Portfolio(new Dictionary<StockSymbol, int>());
        }

        public void AddFunds(Money funds)
        {
            var evt = new FundsAddedEvent(Name, funds.Currency.Name, funds.Amount);
            Apply(evt);
            Store.Current.Publish(evt);
        }

        public void Apply(FundsAddedEvent evt)
        {
            var currency = new Currency(evt.CurrencyName);
            var money = new Money(evt.Amount, currency);

            Funds = Funds.Add(money);
        }
    }
}
