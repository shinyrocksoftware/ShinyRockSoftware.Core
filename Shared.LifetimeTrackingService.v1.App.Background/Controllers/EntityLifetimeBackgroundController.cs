using App.Background.Abstract.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.LifetimeTrackingService.v1.App.Background.BackgroundServices;

namespace Shared.LifetimeTrackingService.v1.App.Background.Controllers;

[Route("entityLifetime/changed")]
public class EntityLifetimeBackgroundController(ILogger<EntityLifetimeBackgroundController> logger, EntityLifetimeBackgroundService entityLifetimeBackgroundService)
	: BaseBackgroundController(logger, entityLifetimeBackgroundService);