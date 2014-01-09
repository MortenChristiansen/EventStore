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

        public void Persist<TEvent>(TEvent e)
        {
            var type = typeof(TEvent);
            if (!_events.ContainsKey(type))
                _events.Add(type, new List<object>());

            _events[type].Add(e);
        }
    }
}
