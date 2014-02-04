using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStore
{
    public enum MaxStaleness
    {
        None,
        TwoSeconds,
        FifteenSeconds,
        ThirtySeconds,
        OneMinute,
        TwoMinutes,
        FiveMinutes,
        FifteenMinutes,
        ThirtyMinues,
        OneHour,
        TwoHours,
        SixHours,
        OneDay,
        NoLimit
    }
}
