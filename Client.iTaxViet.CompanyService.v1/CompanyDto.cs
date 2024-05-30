using Core.Model.Abstract.Entities;
using Base.Model.Interface.Entities;

namespace Client.iTaxViet.CompanyService.v1;

public partial class CompanyDto : BaseEntityDto<Guid>, IEntityDto<Guid>
{
	public string Name { get; set; }
	public string Code { get; set; }
	public string TaxIdentificationNumber { get; set; }
	public DateTime EstablishmentAt { get; set; }
}