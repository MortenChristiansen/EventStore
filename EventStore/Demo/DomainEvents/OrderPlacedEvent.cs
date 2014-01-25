using Demo.Models;
using EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DomainEvents
{
    [CachingInfo(MaxStaleness.ThirtySeconds)]
    public class OrderPlacedEvent : DomainEvent
    {
        public string BrokerName { get; private set; }
        public string SymbolName { get; private set; }
        public int Shares { get; private set; }
        public decimal PriceOfPurchase { get; private set; }
        public string Currency { get; private set; }

        public OrderPlacedEvent(string brokerName, string symbolName, int shares, decimal priceOfPurchase, string currency)
            : this(null, brokerName, symbolName, shares, priceOfPurchase, currency, DateTime.UtcNow)
        { }

        public OrderPlacedEvent(string orderId, string brokerName, string symbolName, int shares, decimal priceOfPurchase, string currency, DateTime eventDate)
            : base(orderId, eventDate)
        {
            BrokerName = brokerName;
            SymbolName = symbolName;
            Shares = shares;
            PriceOfPurchase = priceOfPurchase;
            Currency = currency;
        }
    }
}
