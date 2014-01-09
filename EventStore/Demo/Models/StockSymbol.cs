using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    public sealed class StockSymbol
    {
        public string Symbol { get; private set; }

        public StockSymbol(string symbol)
        {
            Symbol = symbol;
        }

        public override bool Equals(object obj)
        {
            var symbol = obj as StockSymbol;
            if (symbol == null)
                return false;

            return Symbol == symbol.Symbol;
        }

        public override int GetHashCode()
        {
            return Symbol.GetHashCode();
        }
    }
}
