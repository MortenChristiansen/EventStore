using EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DomainEvents
{
    [CachingInfo(MaxStaleness.TwoSeconds)]
    public class FundsAddedEvent : DomainEvent
    {
        public string BrokerName { get; private set; }
        public string CurrencyName { get; private set; }
        public decimal Amount { get; private set; }

        public FundsAddedEvent(string brokerName, string currencyName, decimal amount)
            : this(brokerName, currencyName, amount, DateTime.UtcNow)
        { }

        public FundsAddedEvent(string brokerName, string currencyName, decimal amount, DateTime added)
            : base(brokerName, added)
        {
            BrokerName = brokerName;
            CurrencyName = currencyName;
            Amount = amount;
        }
    }
}
