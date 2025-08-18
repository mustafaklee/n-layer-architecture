using App.Application.Contracts.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace App.Caching
{
    public class CacheService : ICacheService
    {
        private IMemoryCache _memoryCache;
        public CacheService(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }
        public Task AddAsync<T>(string cacheKey, T value, TimeSpan expireTimeSpan)
        {
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expireTimeSpan
            };
            _memoryCache.Set(cacheKey, value, cacheOptions);
            return Task.CompletedTask;
        }

        public Task<T?> GetAsync<T>(string cacheKey)
        {
            if(_memoryCache.TryGetValue(cacheKey, out T cacheItem))
            {
                return Task.FromResult(cacheItem);
            }
            return Task.FromResult(default(T));
        }

        public Task RemoveAsync<T>(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
            return Task.CompletedTask;
        }
    }
}
