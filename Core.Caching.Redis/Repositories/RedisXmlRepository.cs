using System.Xml.Linq;
using Microsoft.AspNetCore.DataProtection.Repositories;
using StackExchange.Redis;

namespace Core.Caching.Redis.Repositories;

public class RedisXmlRepository : IXmlRepository
{
    private readonly Func<IDatabase> _databaseFactory;
    private readonly RedisKey _key;

    /// <summary>
    /// Creates a <see cref="T:Microsoft.AspNetCore.DataProtection.StackExchangeRedis.RedisXmlRepository" /> with keys stored at the given directory.
    /// </summary>
    /// <param name="databaseFactory">The delegate used to create <see cref="T:StackExchange.Redis.IDatabase" /> instances.</param>
    /// <param name="key">The <see cref="T:StackExchange.Redis.RedisKey" /> used to store key list.</param>
    public RedisXmlRepository(Func<IDatabase> databaseFactory, RedisKey key)
    {
        _databaseFactory = databaseFactory;
        _key = key;
    }

    /// <inheritdoc />
    public IReadOnlyCollection<XElement> GetAllElements()
    {
        return GetAllElementsCore().ToList().AsReadOnly();
    }

    private IEnumerable<XElement> GetAllElementsCore()
    {
        var redisValueArray = _databaseFactory().ListRange(_key);
        foreach (var t in redisValueArray)
        {
            yield return XElement.Parse(t);
        }
    }

    /// <inheritdoc />
    public void StoreElement(XElement element, string friendlyName)
    {
        _databaseFactory().ListRightPush(_key, (RedisValue) element.ToString(SaveOptions.DisableFormatting));
    }
}