﻿using Base.Model.Interface.DbRequests;
using Base.Model.Interface.Entities;

namespace Core.Model.Abstract.MediatorRequests;

public abstract class BasePartialPlainCommandMediatorRequest<T, TE, TEO, TV> : BasePlainCommandMediatorRequest<T, TE, TEO, TV>
	where TV : IPlainCommandDbRequest<T, TE>, new()
	where TE : IPlainEntity<T>
	where TEO : IPlainEntityDto<T>;