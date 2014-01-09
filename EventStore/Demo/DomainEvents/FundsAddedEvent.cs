using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DomainEvents
{
    public class FundsAddedEvent
    {
        public string BrokerName { get; private set; }
        public string CurrencyName { get; private set; }
        public decimal Amount { get; private set; }

        public FundsAddedEvent(string brokerName, string currencyName, decimal amount)
        {
            BrokerName = brokerName;
            CurrencyName = currencyName;
            Amount = amount;
        }
    }
}
