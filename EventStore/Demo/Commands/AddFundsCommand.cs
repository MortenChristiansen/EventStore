using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Commands
{
    public class AddFundsCommand
    {
        public string BrokerName { get; private set; }
        public string CurrencyName { get; private set; }
        public decimal Amount { get; private set; }

        public AddFundsCommand(string brokerName, string currencyName, decimal amount)
        {
            BrokerName = brokerName;
            CurrencyName = currencyName;
            Amount = amount;
        }
    }
}
