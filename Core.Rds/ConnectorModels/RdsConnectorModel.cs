using Base.Extension;
using Core.Attribute;
using Core.Model.Abstract.ConnectorModels;
using Base.Model.Interface;

namespace Core.Rds.ConnectorModels;

public class RdsConnectorModel : BaseValidationModel, IConnectorModel
{
	[SingleKeyConnectorModel("rds_custom", required: false)]
	public string Custom { get; set; }
	[SingleKeyConnectorModel("rds_database")]
	public string Database { get; set; }

	[SingleKeyConnectorModel("rds_host_reader")]
	public string HostReader { get; set; }
	[SingleKeyConnectorModel("rds_port_reader", typeof(int), false)]
	public int PortReader { get; set; }
	[SingleKeyConnectorModel("rds_username_reader")]
	public string UsernameReader { get; set; }
	[SingleKeyConnectorModel("rds_password_reader")]
	public string PasswordReader { get; set; }

	[SingleKeyConnectorModel("rds_host_writer")]
	public string HostWriter { get; set; }
	[SingleKeyConnectorModel("rds_port_writer", typeof(int), false)]
	public int PortWriter { get; set; }
	[SingleKeyConnectorModel("rds_username_writer")]
	public string UsernameWriter { get; set; }
	[SingleKeyConnectorModel("rds_password_writer")]
	public string PasswordWriter { get; set; }

	public string ConnectionStringReader => $"Server={HostReader};Port={PortReader};Database={Database};User Id={UsernameReader};Password={PasswordReader};";
	public string ConnectionStringWriter => $"Server={HostWriter};Port={PortWriter};Database={Database};User Id={UsernameWriter};Password={PasswordWriter};";

	public override bool IsValid => Database.IsNotNullNorEmpty()
	                                && HostReader.IsNotNullNorEmpty() && UsernameReader.IsNotNullNorEmpty() && PasswordReader.IsNotNullNorEmpty()
	                                && HostWriter.IsNotNullNorEmpty() && UsernameWriter.IsNotNullNorEmpty() && PasswordWriter.IsNotNullNorEmpty();

	public override string InvalidMessage => "Requires 'rds_database', 'rds_host_reader', 'rds_username_reader', 'rds_password_reader', 'rds_host_writer', 'rds_username_writer', and 'rds_password_writer'";
}