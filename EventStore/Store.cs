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
                    _current = new Store(new InMemoryEventPersistence());

                return _current;
            }
        }

        private Store(IEventPersistence persistence)
        {
            _persistence = persistence;
        }

        public void Save<TEvent>(TEvent e)
        {
            _persistence.Persist(e);
        }
    }
}
