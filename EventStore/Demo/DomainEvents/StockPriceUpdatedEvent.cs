using EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DomainEvents
{
    [CachingInfo(MaxStaleness.None)]
    public class StockPriceUpdatedEvent : DomainEvent
    {
        public string StockName { get; private set; }
        public string StockExchangeName { get; private set; }
        public decimal NewPrice { get; private set; }
        public decimal PriceDelta { get; private set; }
        public string CurrencySymbol { get; private set; }

        public StockPriceUpdatedEvent(string stockName, string stockExchangeName, decimal newPrice, decimal priceDelta, string currencySymbol)
            : this(stockName, stockExchangeName, newPrice, priceDelta, currencySymbol, DateTime.UtcNow)
        { }

        public StockPriceUpdatedEvent(string stockName, string stockExchangeName, decimal newPrice, decimal priceDelta, string currencySymbol, DateTime eventDate)
            : base(stockExchangeName, eventDate)
        {
            StockName = stockName;
            StockExchangeName = stockExchangeName;
            NewPrice = newPrice;
            PriceDelta = priceDelta;
            CurrencySymbol = currencySymbol;
        }

        public override string ToString()
        {
            var op = PriceDelta >= 0 ? "+" : "-";

            return string.Format("New price for stock symbol '{0}': {1:0.00}{2} {3}{4:0.00}", StockName, NewPrice, CurrencySymbol, op, PriceDelta);
        }
    }
}
