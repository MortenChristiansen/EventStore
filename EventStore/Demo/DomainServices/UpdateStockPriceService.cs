using Demo.Commands;
using Demo.Models;
using Demo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DomainServices
{
    public class UpdateStockPriceService
    {
        public void Handle(UpdateStockPriceCommand command)
        {
            var stockExchangeRepository = new StockExchangeRepository();
            var exchange = stockExchangeRepository.GetStockExchange(command.StockExchangeName);

            var price = new Money(command.Cost, new Currency(command.CurrencySymbol));
            var stockSymbol = new StockSymbol(command.StockName);
            exchange.UpdatePrice(stockSymbol, price);
        }
    }
}
