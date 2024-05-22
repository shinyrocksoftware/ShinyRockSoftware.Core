using App.Rest.Abstract.Controllers;
using Client.iTaxViet.CompanyService.v1.App.Features.Commands;
using Client.iTaxViet.CompanyService.v1.App.Features.Queries;
using Client.iTaxViet.CompanyService.v1.App.Rest.ApiRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Client.iTaxViet.CompanyService.v1.App.Rest.Controllers;

[Route("entities")]
public class CompanyController(ILogger<CompanyController> logger, IMediator mediator)
	: BaseApiController(logger)
{
	[HttpGet("{id:guid}")]
	public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
	{
		var entity = await mediator.Send(new GetCompanyByIdQuery(id), cancellationToken);
		return entity == null ? ReturnNotFound() : ReturnSuccess(entity);
	}

	[HttpGet("{pageSize:int}/{pageNumber:int}")]
	public async Task<IActionResult> GetPaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
	{
		var entities = await mediator.Send(new GetPagedCompaniesQuery(pageNumber, pageSize), cancellationToken);
		return ReturnSuccess(entities);
	}

	[HttpPost]
	public async Task<IActionResult> Post(CreateCompanyCommandApiRequest request, CancellationToken cancellationToken)
	{
		var entity = await mediator.Send(request.ToMediatorRequest(), cancellationToken);
		return ReturnSuccess(entity);
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> Put(Guid id, UpdateCompanyCommandApiRequest request, CancellationToken cancellationToken)
	{
		var entity = await mediator.Send(request.ToMediatorRequest(id), cancellationToken);
		return ReturnSuccess(entity);
	}

	[HttpPatch("{id:guid}")]
	public async Task<IActionResult> Patch(Guid id, UpdateCompanyPartialCommandApiRequest request, CancellationToken cancellationToken)
	{
		var entity = await mediator.Send(request.ToMediatorRequest(id), cancellationToken);
		return ReturnSuccess(entity);
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
	{
		var success = await mediator.Send(new DeleteCompanyCommand(id), cancellationToken);
		return ReturnSuccess(success);
	}
}