using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStore
{
    public class AggregateNotFoundException<TAggregate> : Exception
    {
        public AggregateNotFoundException(string aggregateId)
            : base(typeof(TAggregate).Name + ":" + aggregateId)
        {

        }
    }
}
