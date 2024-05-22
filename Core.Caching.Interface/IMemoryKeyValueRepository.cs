using Core.Model.Interface;
using Microsoft.Extensions.Caching.Memory;

namespace Core.Caching.Interface;

public interface IMemoryKeyValueRepository : IMemoryCache, IAutoInjection
{
    T Get<T>(string key);
    Task<T> GetAsync<T>(string key);
    T Set<T>(string key, T value, MemoryCacheEntryOptions? options = null);
    Task<T> SetAsync<T>(string key, T value, MemoryCacheEntryOptions? options = null);
}