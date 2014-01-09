using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStore
{
    interface IEventPersistence
    {
        void Persist<TEvent>(TEvent e);
    }
}
