using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Queries
{
    public class GetQuoteQuery
    {
        public string BrokerName { get; private set; }
        public string SymbolName { get; private set; }

        public GetQuoteQuery(string brokerName, string symbolName)
        {
            BrokerName = brokerName;
            SymbolName = symbolName;
        }        
    }
}
