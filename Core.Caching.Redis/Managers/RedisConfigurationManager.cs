using Base.Extension;
using Core.Caching.Interface;
using Core.Configuration.Interface;
using Core.Model;

namespace Core.Caching.Redis.Managers;

public class RedisConfigurationManager : IConfigurationManager
{
    private readonly string _serviceRedisKey;
    private readonly IRedisRepository _redisRepository;

    public RedisConfigurationManager(string serviceRedisKey, IRedisRepository redisRepository)
    {
        _serviceRedisKey = serviceRedisKey;
        _redisRepository = redisRepository;
    }

    public void UpdateConfigurations(IDictionary<string, string> updates)
    {
        var configurations = _redisRepository.GetDictionary(_serviceRedisKey);

        if (updates.IsNotNullNorEmpty())
        {
            foreach (var config in updates)
            {
                configurations[config.Key] = config.Value;
            }

            _redisRepository.SetDictionary(_serviceRedisKey, configurations.ToDictionary());

            LockModel.Configurations = configurations;
        }
    }

    public void DeleteConfiguration(string key)
    {
        var configurations = _redisRepository.GetDictionary(_serviceRedisKey);

        if (configurations.ContainsKey(key))
        {
            configurations.Remove(key);
        }

        _redisRepository.SetDictionary(_serviceRedisKey, configurations.ToDictionary());

        LockModel.Configurations = configurations;
    }

    public IDictionary<string, string> GetConfigurations()
    {
        return _redisRepository.GetDictionary(_serviceRedisKey);
    }
}