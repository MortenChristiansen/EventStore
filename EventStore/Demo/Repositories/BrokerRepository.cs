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
    class BrokerRepository
    {
        public Broker GetBroker(string brokerName, string stockExchangeName)
        {
            var brokerEvent = Store.Current.Load<BrokerRegisteredEvent>(brokerName).FirstOrDefault(e => e.StockExchangeName == stockExchangeName);
            if (brokerEvent == null)
                throw new AggregateNotFoundException<Broker>(brokerName);

            var fundsAddedEvents = Store.Current.Load<FundsAddedEvent>(brokerName);
            var broker = new Broker(brokerName);

            foreach (var evt in fundsAddedEvents)
            {
                broker.Apply(evt);
            }

            return broker;
        }
    }
}
