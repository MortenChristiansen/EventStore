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
                    _current = new Store(new CachedEventPersistence(new RavenDbEventPersistence()));

                return _current;
            }
        }

        private Store(IEventPersistence persistence)
        {
            _persistence = persistence;
        }

        public void Publish<TEvent>(TEvent e)
            where TEvent : DomainEvent
        {
            Console.WriteLine(e);

            _persistence.Persist(e);

            TriggerHandlers(e);
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

        private Dictionary<Type, List<object>> _generalHandlers = new Dictionary<Type, List<object>>();
        private Dictionary<KeyValuePair<Type, string>, List<object>> _specificHandlers = new Dictionary<KeyValuePair<Type, string>, List<object>>();

        private void TriggerHandlers<TEvent>(TEvent e)
            where TEvent : DomainEvent
        {
            var type = typeof(TEvent);
            var key = new KeyValuePair<Type, string>(type, e.AggregateId);

            if (_generalHandlers.ContainsKey(type))
            {
                var generalHandlers = _generalHandlers[type];
                foreach (var handler in generalHandlers.Cast<Action<TEvent>>())
                {
                    handler(e);
                }
            }

            if (_specificHandlers.ContainsKey(key))
            {
                var specificHandlers = _specificHandlers[key];
                foreach (var handler in specificHandlers.Cast<Action<TEvent>>())
                {
                    handler(e);
                }
            }
        }

        public void Subscribe<TEvent>(Action<TEvent> handler)
        {
            var type = typeof(TEvent);
            if (!_generalHandlers.ContainsKey(type))
                _generalHandlers.Add(type, new List<object>());

            _generalHandlers[type].Add(handler);
        }

        public void Subscribe<TEvent>(string aggregateId, Action<TEvent> handler)
        {
            var type = typeof(TEvent);
            var key = new KeyValuePair<Type, string>(type, aggregateId);

            if (!_specificHandlers.ContainsKey(key))
                _specificHandlers.Add(key, new List<object>());

            _specificHandlers[key].Add(handler);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> handler)
        {
            var type = typeof(TEvent);
            _generalHandlers[type].Remove(handler);
        }

        public void Unscribe<TEvent>(string aggregateId, Action<TEvent> handler)
        {
            var type = typeof(TEvent);
            var key = new KeyValuePair<Type, string>(type, aggregateId);

            _specificHandlers[key].Remove(handler);
        }
    }
}
