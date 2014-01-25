using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStore
{
    class InMemoryEventPersistence : IEventPersistence
    {
        private Dictionary<Type, List<object>> _events = new Dictionary<Type, List<object>>();

        public void Persist<TEvent>(TEvent e) where TEvent : DomainEvent
        {
            var type = typeof(TEvent);
            if (!_events.ContainsKey(type))
                _events.Add(type, new List<object>());

            _events[type].Add(e);
        }

        public IEnumerable<TEvent> Load<TEvent>(string aggregateId) where TEvent : DomainEvent
        {
            var type = typeof(TEvent);
            if (!_events.ContainsKey(type))
                return new TEvent[0];
            return _events[type].OfType<TEvent>().Where(e => e.AggregateId == aggregateId);
        }

        public IEnumerable<TEvent> LoadSince<TEvent>(string aggregateId, DateTime date) where TEvent : DomainEvent
        {
            var type = typeof(TEvent);
            if (!_events.ContainsKey(type))
                return new TEvent[0];
            return _events[type].OfType<TEvent>().Where(e => e.AggregateId == aggregateId && e.Created >= date);
        }

        public TEvent LoadSingle<TEvent>(string aggregateId) where TEvent : DomainEvent
        {
            var type = typeof(TEvent);
            if (!_events.ContainsKey(type))
                return null;
            return _events[type].OfType<TEvent>().FirstOrDefault(e => e.AggregateId == aggregateId);
        }

        public TEvent LoadLatest<TEvent>(string aggregateId) where TEvent : DomainEvent
        {
            var type = typeof(TEvent);
            if (!_events.ContainsKey(type))
                return null;
            return _events[type].OfType<TEvent>().OrderByDescending(e => e.Created).FirstOrDefault(e => e.AggregateId == aggregateId);
        }
    }
}
