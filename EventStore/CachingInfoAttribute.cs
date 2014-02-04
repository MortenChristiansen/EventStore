using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStore
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
    public class CachingInfoAttribute : Attribute
    {
        public MaxStaleness MaxStaleness { get; private set; }

        public CachingInfoAttribute(MaxStaleness maxStaleness)
        {
            MaxStaleness = maxStaleness;
        }
    }
}
