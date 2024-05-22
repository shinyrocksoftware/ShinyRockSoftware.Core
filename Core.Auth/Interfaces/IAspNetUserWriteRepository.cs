using Core.Auth.Entities;
using Core.Rds.Interface;

namespace Core.Auth.Interfaces;

public interface IAspNetUserWriteRepository : IPlainWriteRepository<Guid, AspNetUser>;