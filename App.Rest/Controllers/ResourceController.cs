using App.Rest.Abstract.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Rest.Controllers;

[Route("resource")]
public class ResourceController(ILogger<ResourceController> logger) : BaseApiController(logger)
{
	[HttpGet("cpu")]
	public IActionResult GetCpu()
	{
		return ReturnSuccess(SingletonEntities.CpuCounter.NextValue());
	}

	[HttpGet("memory")]
	public IActionResult GetMemory()
	{
		return ReturnSuccess(SingletonEntities.MemoryCounter.NextValue());
	}
}