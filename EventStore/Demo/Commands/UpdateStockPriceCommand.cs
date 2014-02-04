using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Commands
{
    public class UpdateStockPriceCommand
    {
        public string StockName { get; private set; }
        public string StockExchangeName { get; private set; }
        public decimal Cost { get; private set; }
        public string CurrencySymbol { get; private set; }

        public UpdateStockPriceCommand(string stockName, string stockExchangeName, decimal cost, string currencySymbol)
        {
            StockName = stockName;
            StockExchangeName = stockExchangeName;
            Cost = cost;
            CurrencySymbol = currencySymbol;
        }
    }
}
