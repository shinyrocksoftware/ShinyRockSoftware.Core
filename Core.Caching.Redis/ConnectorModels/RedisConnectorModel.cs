using Core.Attribute;
using Core.Extension;
using Core.Model.Abstract.ConnectorModels;
using Core.Model.Interface;

namespace Core.Caching.Redis.ConnectorModels;

public class RedisConnectorModel : BaseValidationModel, IConnectorModel
{
    [SingleKeyConnectorModel("redis_host")]
    public string Host { get; set; }
    [SingleKeyConnectorModel("redis_port", typeof(int), false)]
    public int Port { get; set; } = 6379;
    [SingleKeyConnectorModel("redis_password")]
    public string Password { get; set; }
    [SingleKeyConnectorModel("redis_prefix")]
    public string Prefix { get; set; }
    [SingleKeyConnectorModel("redis_ttl", typeof(int), false)]
    public int? Ttl { get; set; }

    public string ConnectionString => $"{Host}:{Port},password={Password},channelPrefix={Prefix}";

    public override bool IsValid => Host.IsNotNullNorEmpty() && Prefix.IsNotNullNorEmpty();
    public override string InvalidMessage => "Requires 'redis_host', and 'redis_prefix'";
}