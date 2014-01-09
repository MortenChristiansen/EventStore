using Demo.DomainEvents;
using EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class StockExchange
    {
        public Quote GetQuote(StockSymbol symbol)
        {
            var rand = new Random();
            var dollars = 1 + rand.Next(1000);

            var price = new Money(dollars, new Currency("USD"));

            var quote = new Quote(symbol, price);
            return quote;
        }

        public void PlaceOrder(Order order)
        {
            var cost = order.Shares * order.Quote.Cost.Amount;

            Store.Current.Save(new OrderPlacedEvent(order.Broker.Name, order.Symbol.Symbol, order.Shares, cost, order.Quote.Cost.Currency.Name));
        }
    }
}
