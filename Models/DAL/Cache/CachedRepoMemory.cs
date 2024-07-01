
using Microsoft.Extensions.Caching.Memory;

namespace EFCoreWebApp.Models.DAL.Cache
{
    public class CachedRepoMemory : ICachedRepo
    {
        private readonly IMemoryCache _cache;

        public CachedRepoMemory(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            if (_cache.TryGetValue<T>(key, out var value))
            {
                return value;
            }

            return default(T);
        }

        public T GetOrSet<T>(string key, Func<T> factory, int cacheTimeInMinutes)
        {
            if (_cache.TryGetValue<T>(key, out var value))
            {
                return value;
            }

            _cache.Set(key, factory());

            return factory();
        }

        public async Task RemoveAsync(string key)
        {
            _cache.Remove(key);
        }

        public async Task SetAsync(string key, object data, int cacheTimeInMinutes)
        {
            var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(cacheTimeInMinutes));

            _cache.Set(key, data, cacheOptions);
        }

        public bool TryGetValue<T>(string key, out T result)
        {
            return _cache.TryGetValue<T>(key, out result);
        }
    }
}
