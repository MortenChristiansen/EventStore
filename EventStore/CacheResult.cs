using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStore
{
    public struct CacheResult<T>
    {
        public T Value { get; private set; }
        public DateTime Cached { get; private set; }
        public MaxStaleness MaxStaleness { get; private set; }

        public CacheResult(T value, MaxStaleness maxStaleness)
            : this()
        {
            Value = value;
            Cached = DateTime.UtcNow;
            MaxStaleness = maxStaleness;
        }
    }
}
