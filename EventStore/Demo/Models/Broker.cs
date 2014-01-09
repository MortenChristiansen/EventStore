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

        public Broker(string name, Money funds, Portfolio portfolio)
        {
            Name = name;
            Funds = funds;
            Portfolio = portfolio;
        }

        public void AddFunds(Money funds)
        {
            Funds = Funds.Add(funds);

            Store.Current.Save(new FundsAddedEvent(Name, funds.Currency.Name, funds.Amount));
        }
    }
}
