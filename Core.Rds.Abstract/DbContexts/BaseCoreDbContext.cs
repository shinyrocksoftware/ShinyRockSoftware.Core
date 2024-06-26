﻿using Core.Model.Abstract.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Rds.Abstract.DbContexts;

public abstract class BaseCoreDbContext<T>(DbContextOptions<T> options) : BaseDbContext<T>(options)
	where T : DbContext
{
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		BaseOnModelCreating(modelBuilder, typeof(BaseEntity<>));
	}
}