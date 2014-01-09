using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DomainEvents
{
    public class OrderPlacedEvent
    {
        public string BrokerName { get; private set; }
        public string SymbolName { get; private set; }
        public int Shares { get; private set; }
        public decimal PriceOfPurchase { get; private set; }
        public string Currency { get; private set; }

        public OrderPlacedEvent(string brokerName, string symbolName, int shares, decimal priceOfPurchase, string currency)
        {
            BrokerName = brokerName;
            SymbolName = symbolName;
            Shares = shares;
            PriceOfPurchase = priceOfPurchase;
            Currency = currency;
        }
    }
}
