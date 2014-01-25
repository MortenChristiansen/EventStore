using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStore
{
    public class Store
    {
        private static Store _current;
        private IEventPersistence _persistence;

        public static Store Current
        {
            get
            {
                if (_current == null)
                    _current = new Store(new CachedEventPersistence(new InMemoryEventPersistence()));

                return _current;
            }
        }

        private Store(IEventPersistence persistence)
        {
            _persistence = persistence;
        }

        public void Save<TEvent>(TEvent e)
            where TEvent : DomainEvent
        {
            Console.WriteLine(e);

            _persistence.Persist(e);
        }

        public IEnumerable<TEvent> Load<TEvent>(string aggregateId)
            where TEvent : DomainEvent
        {
            return _persistence.Load<TEvent>(aggregateId);
        }

        public IEnumerable<TEvent> LoadSince<TEvent>(string aggregateId, DateTime date)
            where TEvent : DomainEvent
        {
            return _persistence.LoadSince<TEvent>(aggregateId, date);
        }

        public TEvent LoadSingle<TEvent>(string aggregateId)
            where TEvent : DomainEvent
        {
            return _persistence.LoadSingle<TEvent>(aggregateId);
        }

        public TEvent LoadLatest<TEvent>(string aggregateId)
            where TEvent : DomainEvent
        {
            return _persistence.LoadLatest<TEvent>(aggregateId);
        }
    }
}
