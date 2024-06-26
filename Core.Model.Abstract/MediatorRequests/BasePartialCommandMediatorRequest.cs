﻿using Base.Model.Interface.DbRequests;
using Base.Model.Interface.Entities;

namespace Core.Model.Abstract.MediatorRequests;

public abstract class BasePartialCommandMediatorRequest<T, TE, TEO, TV> : BaseCommandMediatorRequest<T, TE, TEO, TV>
	where TV : ICommandDbRequest<T, TE>, new()
	where TE : IEntity<T>
	where TEO : IEntityDto<T>;