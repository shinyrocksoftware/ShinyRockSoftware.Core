using App.Background.Abstract.Controllers;

namespace Lab.App.Background.Controllers;

public class BackgroundController(ILogger<BackgroundController> logger, PeriodicBackgroundService periodicBackgroundService)
	: BaseBackgroundController(logger, periodicBackgroundService);