using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Portfolio
    {
        private Dictionary<StockSymbol, int> _stocks;

        public IReadOnlyDictionary<StockSymbol, int> Stocks { get { return _stocks; } }

        public Portfolio(Dictionary<StockSymbol, int> stocks)
        {
            _stocks = stocks;
        }
    }
}
