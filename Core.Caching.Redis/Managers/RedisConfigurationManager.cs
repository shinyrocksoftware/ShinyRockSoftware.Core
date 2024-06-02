using Base.Extension;
using Core.Caching.Interface;
using Core.Configuration.Interface;
using Core.Model;

namespace Core.Caching.Redis.Managers;

public class RedisConfigurationManager(string serviceRedisKey, IRedisRepository redisRepository) : IConfigurationManager
{
    public void UpdateConfigurations(IDictionary<string, string> updates)
    {
        var configurations = redisRepository.GetDictionary(serviceRedisKey);

        if (updates.IsNotNullNorEmpty())
        {
            foreach (var config in updates)
            {
                configurations[config.Key] = config.Value;
            }

            redisRepository.SetDictionary(serviceRedisKey, configurations.ToDictionary());

            LockModel.Configurations = configurations;
        }
    }

    public void DeleteConfiguration(string key)
    {
        var configurations = redisRepository.GetDictionary(serviceRedisKey);

        if (configurations.ContainsKey(key))
        {
            configurations.Remove(key);
        }

        redisRepository.SetDictionary(serviceRedisKey, configurations.ToDictionary());

        LockModel.Configurations = configurations;
    }

    public IDictionary<string, string> GetConfigurations()
    {
        return redisRepository.GetDictionary(serviceRedisKey);
    }
}