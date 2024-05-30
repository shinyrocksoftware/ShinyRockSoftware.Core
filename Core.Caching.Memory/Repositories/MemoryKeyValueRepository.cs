using Core.Attribute.AutoInjection;
using Base.Extension;
using Core.Caching.Interface;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Core.Caching.Memory.Repositories;

[SingletonAutoInjection]
internal class MemoryKeyValueRepository(IOptions<MemoryCacheOptions> optionsAccessor)
    : MemoryCache(optionsAccessor), IMemoryKeyValueRepository
{
    public T Get<T>(string key)
    {
        TryGetValue(key, out var result);
        return result == null ? default : result.ToString().Deserialize<T>();
    }

    public async Task<T> GetAsync<T>(string key)
    {
        return await Task.Factory.StartNew(() => Get<T>(key));
    }

    public T Set<T>(string key, T value, MemoryCacheEntryOptions? options = null)
    {
        Remove(key);

        var entry = CreateEntry(key);
        entry.Value = value.Serialize();

        options ??= new()
        {
            Priority = CacheItemPriority.NeverRemove
        };

        entry.SetOptions(options);
        entry.Dispose();

        return value;
    }

    public async Task<T> SetAsync<T>(string key, T value, MemoryCacheEntryOptions? options = null)
    {
        return await Task.Factory.StartNew(() => Set(key, value, options));
    }
}