using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStore
{
    public abstract class DomainEvent
    {
        public string AggregateId { get; private set; }
        public DateTime Created { get; private set; }
        public string EventId { get; private set; }

        private DomainEvent()
        {

        }

        public DomainEvent(string id, DateTime created)
        {
            AggregateId = id;
            Created = created;
            EventId = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return GetType().Name + ": " + AggregateId;
        }
    }
}
