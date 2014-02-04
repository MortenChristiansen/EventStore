using Demo.Commands;
using Demo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DomainServices
{
    public class RegisterBrokerService
    {
        public void Handle(RegisterBrokerCommand command)
        {
            var repository = new StockExchangeRepository();
            var exchange = repository.GetStockExchange(command.StockExchangeName);

            exchange.RegisterBroker(command.BrokerName);
        }
    }
}
