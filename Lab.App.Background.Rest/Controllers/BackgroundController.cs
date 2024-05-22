using App.Background.Abstract.Controllers;
using Lab.App.Background.Rest.BackgroundServices;

namespace Lab.App.Background.Rest.Controllers;

public class BackgroundController(ILogger<BackgroundController> logger, DefaultBackgroundService defaultBackgroundService)
	: BaseBackgroundController(logger, defaultBackgroundService);