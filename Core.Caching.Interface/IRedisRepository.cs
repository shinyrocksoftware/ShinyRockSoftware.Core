using Core.Model.Interface;

namespace Core.Caching.Interface;

public interface IRedisRepository : IAutoInjection
{
    bool IsConnected();
    Task<string> GetAsync(string key);
    Task<IEnumerable<string>> GetKeysAsync(string pattern);
    IDictionary<string, string> GetDictionary(string key);
    Task<IDictionary<string, string>> GetDictionaryAsync(string hashKey);
    Task<string> GetDictionaryValueAsync(string hashKey, string dictionaryKey); 
    void SetDictionary(string hashKey, Dictionary<string, string> dictionary, DateTimeOffset? expiresAt = null);
    Task SetDictionaryAsync(string hashKey, IDictionary<string, string> dictionary, DateTimeOffset? expiresAt = null);
    Task SetDictionaryValueAsync(string hashKey, string dictionaryKey, string value, DateTimeOffset? expiresAt = null);
    Task RemoveAsync(string key);
    Task RemoveDictionaryValueAsync(string hashKey, string dictionaryKey);
    bool KeyExist(string key);
        
    T Get<T>(string key);
    Task<T> GetAsync<T>(string key);
    void Set<T>(string key, T value);
    Task SetAsync<T>(string key, T value, DateTimeOffset? expiresAt = null);
    Task SetAsync<T>(string key, T value, TimeSpan ttlInMinutes);
}