using EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DomainEvents
{
    [CachingInfo(MaxStaleness.OneHour)]
    public class BrokerRegisteredEvent : DomainEvent
    {
        public string BrokerName { get; private set; }
        public string StockExchangeName { get; private set; }

        public BrokerRegisteredEvent(string brokerName, string stockExchangeName)
            : this(brokerName, stockExchangeName, DateTime.UtcNow)
        { }

        public BrokerRegisteredEvent(string brokerName, string stockExchangeName, DateTime added)
            : base(brokerName, added)
        {
            BrokerName = brokerName;
        }
    }
}
