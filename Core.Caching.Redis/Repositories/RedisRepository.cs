using Base.Extension;
using Core.Attribute.AutoInjection;
using Core.Caching.Interface;
using Microsoft.Extensions.Logging;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Core.Caching.Redis.Repositories;

[ScopedAutoInjection]
internal class RedisRepository(ILogger<RedisRepository> logger, IRedisDatabase redisDatabase) : IRedisRepository
{
    private readonly string[] _skipKeys =
    [
        "absexp"
        , "sldexp"
    ];

    private readonly ILogger _logger = logger;

    public bool IsConnected()
    {
        return redisDatabase.Database.Multiplexer.IsConnected;
    }

    public async Task<string> GetAsync(string key)
    {
        return await redisDatabase.GetAsync<string>(GetKey(key));
    }

    public async Task<IEnumerable<string>> GetKeysAsync(string pattern)
    {
        var key = $"{pattern}*";

        return await GetInternalAsync(key, async () => await redisDatabase.SearchKeysAsync(GetKey(key)));
    }

    public IDictionary<string, string> GetDictionary(string key)
    {
        return GetInternal(key, () =>
        {
            var result = redisDatabase.HashGetAllAsync<string>(GetKey(key)).GetAwaiter().GetResult();
            return result.Where(c => !_skipKeys.ContainsCI(c.Key)).ToDictionary(x => x.Key, x => x.Value);
        });
    }

    public async Task<IDictionary<string, string>> GetDictionaryAsync(string hashKey)
    {
        return await GetInternalAsync(hashKey, async () =>
        {
            var result = await redisDatabase.HashGetAllAsync<string>(GetKey(hashKey));
            return result.Where(c => !_skipKeys.ContainsCI(c.Key)).ToDictionary(x => x.Key, x => x.Value);
        });
    }

    public async Task<string> GetDictionaryValueAsync(string hashKey, string dictionaryKey)
    {
	    return await (_skipKeys.ContainsCI(hashKey)
		    ? Task.FromResult(string.Empty)
		    : GetInternalAsync(hashKey, async () => await redisDatabase.HashGetAsync<string>(GetKey(hashKey), GetKey(dictionaryKey))));
    }

    public void SetDictionary(string hashKey, Dictionary<string, string> dictionary, DateTimeOffset? expiresAt = null)
    {
        var tasks = new List<Task>();

        var key = GetKey(hashKey);

        if (expiresAt.HasValue)
        {
            tasks.Add(redisDatabase.UpdateExpiryAsync(key, DateTimeOffset.UtcNow.AddDays(-1)));
        }

        tasks.Add(redisDatabase.HashSetAsync(key, dictionary));

        Task.WhenAll(tasks).GetAwaiter().GetResult();
    }

    public async Task SetDictionaryAsync(string hashKey, IDictionary<string, string> dictionary, DateTimeOffset? expiresAt = null)
    {
        var key = GetKey(hashKey);
        if (expiresAt.HasValue)
        {
            await redisDatabase.UpdateExpiryAsync(key, expiresAt.Value);
        }

        await redisDatabase.HashSetAsync(key, dictionary);
    }

    public async Task SetDictionaryValueAsync(string hashKey, string dictionaryKey, string value, DateTimeOffset? expiresAt = null)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value), "Value to be stored to Redis is null");
        }

        var dict = await GetDictionaryAsync(hashKey);
        var storedDictKey = GetKey(dictionaryKey);
        var storedDictionaryValue = value.Serialize();

        if (dict == null)
        {
            dict = new Dictionary<string, string>
            {
                {
                    storedDictKey, storedDictionaryValue
                }
            };
        }
        else
        {
            dict[storedDictKey] = storedDictionaryValue;
        }

        await SetDictionaryAsync(hashKey, dict, expiresAt);
    }

    public async Task RemoveAsync(string key)
    {
        await redisDatabase.RemoveAsync(GetKey(key));
    }

    public async Task RemoveDictionaryValueAsync(string hashKey, string dictionaryKey)
    {
        var dict = await GetDictionaryAsync(hashKey);

        if (dict != null)
        {
            var storedDictKey = GetKey(dictionaryKey);

            if (dict.ContainsKey(storedDictKey))
            {
                dict.Remove(storedDictKey);
            }
        }

        await SetDictionaryAsync(hashKey, dict);
    }

    public bool KeyExist(string key)
    {
        return redisDatabase.ExistsAsync(GetKey(key)).GetAwaiter().GetResult();
    }

    public T Get<T>(string key)
    {
        return GetInternal(key, () => redisDatabase.GetAsync<T>(GetKey(key)).GetAwaiter().GetResult());
    }

    public async Task<T> GetAsync<T>(string key)
    {
        return await GetInternalAsync(key, async () => await redisDatabase.GetAsync<T>(GetKey(key)));
    }

    public void Set<T>(string key, T value)
    {
        redisDatabase.AddAsync(GetKey(key), value).GetAwaiter().GetResult();
    }

    public async Task SetAsync<T>(string key, T value, DateTimeOffset? expiresAt = null)
    {
        if (expiresAt.HasValue)
        {
            await redisDatabase.AddAsync(GetKey(key), value, expiresAt.Value);
        }
        else
        {
            await redisDatabase.AddAsync(GetKey(key), value);
        }
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan ttlInMinutes)
    {
        await redisDatabase.AddAsync(GetKey(key), value, ttlInMinutes);
    }

    private T GetInternal<T>(string key, Func<T> func)
    {
        T result = default;

        try
        {
            result = func();
        }
        catch (Exception e)
        {
            _logger.LogError($"Error on getting Redis key {key}. Exception {e}");
        }

        return result;
    }

    private async Task<T> GetInternalAsync<T>(string key, Func<Task<T>> func)
    {
        T result = default;

        try
        {
            result = await func();
        }
        catch (Exception e)
        {
            _logger.LogError($"Error on getting Redis key {key}. Exception {e}");
        }

        return result;
    }

    private string GetKey(string key)
    {
        return key.ToLower();
    }
}