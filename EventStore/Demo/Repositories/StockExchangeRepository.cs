using Demo.DomainEvents;
using Demo.Models;
using EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repositories
{
    class StockExchangeRepository
    {
        public StockExchange GetStockExchange(string name)
        {
            var priceUpdatedEvents = Store.Current.Load<StockPriceUpdatedEvent>(name); 

            var exchange = new StockExchange(name);

            foreach (var priceUpdate in priceUpdatedEvents)
            {
                exchange.Apply(priceUpdate);
            }

            return exchange;
        }

        public IEnumerable<string> GetStockExchangeBrokers(string stockExchangeName)
        {
            var brokerRegisteredEvents = Store.Current.Load<BrokerRegisteredEvent>(stockExchangeName);

            foreach (var e in brokerRegisteredEvents)
            {
                yield return e.BrokerName;
            }
        }
    }
}
