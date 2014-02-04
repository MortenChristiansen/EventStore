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
        private Dictionary<StockSymbol, Money> _stockPrices = new Dictionary<StockSymbol, Money>();

        public string Name { get; private set; }

        public StockExchange(string name)
        {
            Name = name;
        }

        public Quote GetQuote(StockSymbol symbol)
        {
            if (!_stockPrices.ContainsKey(symbol))
                return null;

            var price = _stockPrices[symbol];

            var quote = new Quote(symbol, price);
            return quote;
        }

        public void PlaceOrder(Order order)
        {
            var cost = order.Shares * order.Quote.Cost.Amount;

            Store.Current.Publish(new OrderPlacedEvent(order.Broker.Name, order.Symbol.Symbol, order.Shares, cost, order.Quote.Cost.Currency.Name));
        }

        public Broker RegisterBroker(string name)
        {
            Store.Current.Publish(new BrokerRegisteredEvent(name, Name));
            return new Broker(name);
        }

        public void UpdatePrice(StockSymbol stockSymbol, Money price)
        {
            var priceDelta = 0m;
            if (_stockPrices.ContainsKey(stockSymbol))
            {
                priceDelta = price.Amount - _stockPrices[stockSymbol].Amount;
            }

            var evt = new StockPriceUpdatedEvent(stockSymbol.Symbol, Name, price.Amount, priceDelta, price.Currency.Name);
            Apply(evt);
            Store.Current.Publish(evt);
        }

        public void Apply(StockPriceUpdatedEvent evt)
        {
            var symbol = new StockSymbol(evt.StockName);
            var price = new Money(evt.NewPrice, new Currency(evt.CurrencySymbol));

            if (_stockPrices.ContainsKey(symbol))
            {
                _stockPrices[symbol] = price;
            }
            else
            {
                _stockPrices.Add(symbol, price);
            }
        }
    }
}
