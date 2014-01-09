using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Commands
{
    public class PlaceOrderCommand
    {
        public string BrokerName { get; private set; }
        public string SymbolName { get; private set; }
        public int Shares { get; private set; }

        public PlaceOrderCommand(string brokerName, string symbolName, int shares)
        {
            BrokerName = brokerName;
            SymbolName = symbolName;
            Shares = shares;
        }
    }
}
