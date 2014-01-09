using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Order
    {
        public Broker Broker { get; private set; }
        public Quote Quote { get; private set; }
        public StockSymbol Symbol { get; private set; }
        public int Shares { get; private set; }

        public Order(Broker broker, Quote quote, StockSymbol symbol, int shares)
        {
            Broker = broker;
            Quote = quote;
            Symbol = symbol;
            Shares = shares;
        }
    }
}
