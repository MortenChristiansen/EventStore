using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStore
{
    public class CachedEventPersistence : IEventPersistence
    {
        private Dictionary<Type, CachingInfoAttribute> _typeCachingInfo = new Dictionary<Type, CachingInfoAttribute>();
        private Dictionary<string, CacheResult<object>> _events = new Dictionary<string, CacheResult<object>>();
        private IEventPersistence _persistence;

        public CachedEventPersistence(IEventPersistence persistence)
        {
            _persistence = persistence;
        }

        public void ClearCache()
        {
            _events.Clear();
        }

        public void Persist<TEvent>(TEvent e) where TEvent : DomainEvent
        {
            // TODO: Implement some way to invalidate interdependent caches

            var cacheInfo = GetCachingInfo<TEvent>();
            if (cacheInfo != null && cacheInfo.MaxStaleness != MaxStaleness.None)
            {
                // TODO: Implement better updating of the cached data
                var key = "LoadSingle-" + typeof(TEvent).Name + "-" + e.AggregateId;
                UpdateCacheValues(key, cacheInfo.MaxStaleness, e);
            }

            _persistence.Persist<TEvent>(e);
        }

        private void UpdateCacheValues(string cacheKey, MaxStaleness maxStaleness, object value)
        {
            var cacheResult = new CacheResult<object>(value, maxStaleness);
            if (_events.ContainsKey(cacheKey))
                _events[cacheKey] = cacheResult;
            else
                _events.Add(cacheKey, cacheResult);
        }

        public IEnumerable<TEvent> Load<TEvent>(string aggregateId) where TEvent : DomainEvent
        {
            var cacheInfo = GetCachingInfo<TEvent>();
            if (cacheInfo == null || cacheInfo.MaxStaleness == MaxStaleness.None) return _persistence.Load<TEvent>(aggregateId);

            RemoveOldEntries(cacheInfo.MaxStaleness);

            var key = "Load-" + typeof(TEvent).Name + "-" + aggregateId;
            if (_events.ContainsKey(key))
                return (IEnumerable<TEvent>)_events[key].Value;

            var value = _persistence.Load<TEvent>(aggregateId);
            UpdateCacheValues(key, cacheInfo.MaxStaleness, value);

            return value;
        }

        public void RemoveOldEntries(MaxStaleness maxStaleness)
        {
            var validFrom = DateTime.UtcNow - GetStalenessDuration(maxStaleness);
            var entriesToRemove = _events.Where(e => e.Value.MaxStaleness <= maxStaleness && e.Value.Cached <= validFrom).ToList();

            foreach (var entry in entriesToRemove)
            {
                _events.Remove(entry.Key);
            }
        }

        private TimeSpan GetStalenessDuration(MaxStaleness maxStaleness)
        {
            switch (maxStaleness)
            {
                case MaxStaleness.None:
                    return TimeSpan.Zero;
                case MaxStaleness.TwoSeconds:
                    return TimeSpan.FromSeconds(2);
                case MaxStaleness.FifteenSeconds:
                    return TimeSpan.FromSeconds(15);
                case MaxStaleness.ThirtySeconds:
                    return TimeSpan.FromSeconds(30);
                case MaxStaleness.OneMinute:
                    return TimeSpan.FromMinutes(1);
                case MaxStaleness.TwoMinutes:
                    return TimeSpan.FromMinutes(2);
                case MaxStaleness.FiveMinutes:
                    return TimeSpan.FromMinutes(5);
                case MaxStaleness.FifteenMinutes:
                    return TimeSpan.FromMinutes(15);
                case MaxStaleness.ThirtyMinues:
                    return TimeSpan.FromMinutes(30);
                case MaxStaleness.OneHour:
                    return TimeSpan.FromHours(1);
                case MaxStaleness.TwoHours:
                    return TimeSpan.FromHours(2);
                case MaxStaleness.SixHours:
                    return TimeSpan.FromHours(6);
                case MaxStaleness.OneDay:
                    return TimeSpan.FromDays(1);
                case MaxStaleness.NoLimit:
                    return TimeSpan.MaxValue;
                default:
                    throw new NotSupportedException();
            }
        }

        public IEnumerable<TEvent> LoadSince<TEvent>(string aggregateId, DateTime date) where TEvent : DomainEvent
        {
            var cacheInfo = GetCachingInfo<TEvent>();
            if (cacheInfo == null || cacheInfo.MaxStaleness == MaxStaleness.None) return _persistence.LoadSince<TEvent>(aggregateId, date);

            RemoveOldEntries(cacheInfo.MaxStaleness);

            var key = "LoadSince-" + typeof(TEvent).Name + "-" + aggregateId + "-" + date.Ticks;
            if (_events.ContainsKey(key))
                return (IEnumerable<TEvent>)_events[key].Value;

            var value = _persistence.LoadSince<TEvent>(aggregateId, date);
            UpdateCacheValues(key, cacheInfo.MaxStaleness, value);

            return value;
        }

        public TEvent LoadSingle<TEvent>(string aggregateId) where TEvent : DomainEvent
        {
            var cacheInfo = GetCachingInfo<TEvent>();
            if (cacheInfo == null || cacheInfo.MaxStaleness == MaxStaleness.None) return _persistence.LoadSingle<TEvent>(aggregateId);

            RemoveOldEntries(cacheInfo.MaxStaleness);

            var key = "LoadSingle-" + typeof(TEvent).Name + "-" + aggregateId;
            if (_events.ContainsKey(key))
                return (TEvent)_events[key].Value;

            var value = _persistence.LoadSingle<TEvent>(aggregateId);
            UpdateCacheValues(key, cacheInfo.MaxStaleness, value);

            return value;
        }

        public TEvent LoadLatest<TEvent>(string aggregateId) where TEvent : DomainEvent
        {
            var cacheInfo = GetCachingInfo<TEvent>();
            if (cacheInfo == null || cacheInfo.MaxStaleness == MaxStaleness.None) return _persistence.LoadLatest<TEvent>(aggregateId);

            RemoveOldEntries(cacheInfo.MaxStaleness);

            var key = "LoadLatest-" + typeof(TEvent).Name + "-" + aggregateId;
            if (_events.ContainsKey(key))
                return (TEvent)_events[key].Value;

            var value = _persistence.LoadLatest<TEvent>(aggregateId);
            UpdateCacheValues(key, cacheInfo.MaxStaleness, value);

            return value;
        }

        private CachingInfoAttribute GetCachingInfo<TEvent>() where TEvent : DomainEvent
        {
            var type = typeof(TEvent);
            if (!_typeCachingInfo.ContainsKey(type))
            {
                var cacheInfo = type.CustomAttributes.OfType<CachingInfoAttribute>().SingleOrDefault();
                _typeCachingInfo.Add(type, cacheInfo);
            }

            return _typeCachingInfo[type];
        }
    }
}
