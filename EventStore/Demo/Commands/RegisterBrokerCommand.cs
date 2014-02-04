using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Commands
{
    public class RegisterBrokerCommand
    {
        public string BrokerName { get; private set; }
        public string StockExchangeName { get; private set; }

        public RegisterBrokerCommand(string brokerName, string stockExchangeName)
        {
            BrokerName = brokerName;
            StockExchangeName = stockExchangeName;
        }
    }
}
