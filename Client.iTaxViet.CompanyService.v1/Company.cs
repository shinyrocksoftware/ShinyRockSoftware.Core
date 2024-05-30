using System.ComponentModel.DataAnnotations;
using Core.Model.Abstract.Entities;
using Base.Model.Interface.Entities;

namespace Client.iTaxViet.CompanyService.v1;

public partial class Company : BaseEntity<Guid>, IEntity<Guid>
{
    [Required, MaxLength(255)]
    public string Name { get; set; }

    [Required, MaxLength(255)]
    public string Code { get; set; }

    [Required, MaxLength(255)]
    public string TaxIdentificationNumber { get; set; }

    [Required]
    public DateTime EstablishmentAt { get; set; }
}