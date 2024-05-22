using App.Rest.Abstract.Controllers;
using Core.BackgroundService.Interface;
using Core.Job.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Background.Abstract.Controllers;

public abstract class BaseBackgroundController(ILogger<BaseBackgroundController> logger, ITimerBackgroundService timerBackgroundService)
	: BaseApiController(logger)
{
	[HttpGet("background/report")]
	public IActionResult GetReport()
	{
		return ReturnSuccess(new ServiceStatus(timerBackgroundService.ServiceName, timerBackgroundService.IsEnabled, timerBackgroundService.ExecutionCount));
	}

	[HttpPut("background/enable")]
	public IActionResult Enable()
	{
		timerBackgroundService.IsEnabled = true;
		return ReturnSuccess(true);
	}

	[HttpPut("background/disable")]
	public IActionResult Disable()
	{
		timerBackgroundService.IsEnabled = false;
		return ReturnSuccess(true);
	}
}