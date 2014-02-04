using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Queries
{
    public class GetBrokersQuery
    {
        public string StockExchangeName { get; private set; }

        public GetBrokersQuery(string stockExchangeName)
        {
            StockExchangeName = stockExchangeName;
        }        
    }
}
