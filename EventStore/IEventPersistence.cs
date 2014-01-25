using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStore
{
    public interface IEventPersistence
    {
        void Persist<TEvent>(TEvent e) where TEvent : DomainEvent;
        IEnumerable<TEvent> Load<TEvent>(string aggregateId) where TEvent : DomainEvent;
        IEnumerable<TEvent> LoadSince<TEvent>(string aggregateId, DateTime date) where TEvent : DomainEvent;
        TEvent LoadSingle<TEvent>(string aggregateId) where TEvent : DomainEvent;
        TEvent LoadLatest<TEvent>(string aggregateId) where TEvent : DomainEvent;
    }
}
