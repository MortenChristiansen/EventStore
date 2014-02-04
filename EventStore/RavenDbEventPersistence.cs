using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStore
{
    class RavenDbEventPersistence : IEventPersistence
    {
        private DocumentStore _db;

        public RavenDbEventPersistence()
        {
            // TODO: Define connection string in App.config
            _db = new DocumentStore() { ConnectionStringName = "RavenDB" };
            _db.Initialize();
        }

        public void Persist<TEvent>(TEvent e) where TEvent : DomainEvent
        {
            using (var session = _db.OpenSession())
            {
                session.Store(e, e.EventId);
            }
        }

        public IEnumerable<TEvent> Load<TEvent>(string aggregateId) where TEvent : DomainEvent
        {
            using (var session = _db.OpenSession())
            {
                return session.Query<TEvent>().Where(e => e.AggregateId == aggregateId).ToList();
            }
        }

        public IEnumerable<TEvent> LoadSince<TEvent>(string aggregateId, DateTime date) where TEvent : DomainEvent
        {
            using (var session = _db.OpenSession())
            {
                return session.Query<TEvent>().Where(e => e.AggregateId == aggregateId && e.Created >= date).ToList();
            }
        }

        public TEvent LoadSingle<TEvent>(string aggregateId) where TEvent : DomainEvent
        {
            using (var session = _db.OpenSession())
            {
                return session.Query<TEvent>().FirstOrDefault(e => e.AggregateId == aggregateId);
            }
        }

        public TEvent LoadLatest<TEvent>(string aggregateId) where TEvent : DomainEvent
        {
            using (var session = _db.OpenSession())
            {
                return session.Query<TEvent>().OrderByDescending(e => e.Created).FirstOrDefault();
            }
        }
    }
}
