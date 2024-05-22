using Core.Attribute;
using Core.Extension;
using Core.Model.Abstract.ConnectorModels;
using Core.Model.Interface;

namespace Core.Stream.ConnectorModels;

public class KafkaConnectorModel : BaseValidationModel, IConnectorModel
{
    [SingleKeyConnectorModel("services_kafka_bootstrap_servers")]
    public string BootstrapServers { get; set; }

    [SingleKeyConnectorModel("services_kafka_consumer_reboot_time_by_second")]
    public int ConsumerRebootTimeBySecond { get; set; } = 60;

    [SingleKeyConnectorModel("services_kafka_consumer_process_interval_by_second")]
    public int ConsumerProcessIntervalBySecond { get; set; } = 20;

    [SingleKeyConnectorModel("services_kafka_consumer_timeout_by_second")]
    public int ConsumerTimeoutBySecond { get; set; } = 5;

    public override bool IsValid => BootstrapServers.IsNotNullNorEmpty();

    public override string InvalidMessage => "Requires 'services_kafka_bootstrap_servers'";
}