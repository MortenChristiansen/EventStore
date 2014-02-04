using Demo.DomainEvents;
using Demo.Queries;
using Demo.Repositories;
using EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DomainServices
{
    public class FindBrokersService
    {
        public void Handle(GetBrokersQuery query)
        {
            var repository = new StockExchangeRepository();
            var brokers = repository.GetStockExchangeBrokers(query.StockExchangeName);

            var evt = new BrokersFoundEvent(query.StockExchangeName, brokers);
            Store.Current.Publish(evt);
        }
    }
}
