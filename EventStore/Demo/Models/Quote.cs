using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    public sealed class Quote
    {
        public StockSymbol Symbol { get; private set; }
        public Money Cost { get; private set; }

        public Quote(StockSymbol symbol, Money cost)
        {
            Symbol = symbol;
            Cost = cost;
        }

        public override bool Equals(object obj)
        {
            var quote = obj as Quote;
            if (quote == null)
                return false;

            return Symbol == quote.Symbol && Cost == quote.Cost;
        }

        public override int GetHashCode()
        {
            return Symbol.GetHashCode() ^ Cost.GetHashCode();
        }
    }
}
