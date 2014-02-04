using EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DomainEvents
{
    [CachingInfo(MaxStaleness.FiveMinutes)]
    public class BrokersFoundEvent : DomainEvent
    {
        public IEnumerable<string> BrokerNames { get; private set; }
        public string StockExchangeName { get; private set; }

        public BrokersFoundEvent(string stockExchangeName, IEnumerable<string> brokerNames)
            : this(stockExchangeName, brokerNames, DateTime.UtcNow)
        { }

        public BrokersFoundEvent(string stockExchangeName, IEnumerable<string> brokerNames, DateTime added)
            : base(stockExchangeName, added)
        {
            StockExchangeName = stockExchangeName;
            BrokerNames = brokerNames;
        }
    }
}
