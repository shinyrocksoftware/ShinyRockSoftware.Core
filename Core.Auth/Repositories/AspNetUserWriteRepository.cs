using Core.Auth.Entities;
using Core.Auth.Interfaces;
using Core.Model.Interface;
using Core.Rds.DbContexts;
using Core.Rds.Repositories;

namespace Core.Auth.Repositories;

public class AspNetUserWriteRepository(INotificationEventDispatcher dispatcher, PlainWriteDbContext dbContext)
	: PlainWriteRepository<Guid, AspNetUser>(dispatcher, dbContext), IAspNetUserWriteRepository;