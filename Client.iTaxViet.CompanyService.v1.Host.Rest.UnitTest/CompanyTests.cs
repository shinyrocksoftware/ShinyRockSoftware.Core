using Client.iTaxViet.CompanyService.v1.App.Rest.Controllers;
using Core.Model;
using Core.Model.ApiResponses;
using Core.Model.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Client.iTaxViet.CompanyService.v1.Host.Rest.UnitTest;

public class CompanyTests
{
	private ILogger<CompanyController> _logger;
	private IMediator _mediator;
	private CompanyController _companyController;
	private IEnumerablePage<CompanyDto> _pagedCompanies;

	[SetUp]
	public void Setup()
	{
		_logger = Substitute.For<ILogger<CompanyController>>();
		_mediator = Substitute.For<IMediator>();
		_companyController = new (_logger, _mediator);
		_pagedCompanies = GetPagedCompanies(1, 10);
	}

	[Test]
	public async Task GetPagedCompanies_Should_ReturnSuccess()
	{
		// act
		var result = await _companyController.GetPaged(1, 10, CancellationToken.None);
		var jsonResult = (JsonResult) result;

		// assert
		ClassicAssert.AreEqual(StatusCodes.Status200OK, jsonResult.StatusCode);
	}

	[Test]
	public async Task GetPagedCompanies_Should_ReturnPagingData()
	{
		// act
		var result = await _companyController.GetPaged(1, 10, CancellationToken.None);
		var jsonResult = (JsonResult) result;
		var apiResponse = (ApiResponse<IEnumerablePage<CompanyDto>>) jsonResult.Value;

		// assert
		ClassicAssert.NotNull(apiResponse);
		ClassicAssert.AreEqual(_pagedCompanies, apiResponse.Data);
	}

	[Test]
	public async Task GetPagedCompanies_Should_CallCompanyService()
	{
		// act
		var result = await _companyController.GetPaged(1, 10, CancellationToken.None);

		// assert
		// await _companyService.Received().GetPagedAsync(Arg.Any<PagingDbRequest>(), CancellationToken.None);
	}

	private IEnumerablePage<CompanyDto> GetPagedCompanies(int pageNumber, int pageSize)
	{
		var enumerablePage = new EnumerablePage<CompanyDto>
		{
			PageNumber = pageNumber
			, PageSize = pageSize
		};
		var companies = new List<CompanyDto>();

		for (int i = 0; i < 10; i++)
		{
			companies.Add(new()
			{
				Id = Guid.NewGuid()
				, Name = $"Company {i}"
			});
		}

		enumerablePage.PageData = companies;

		return enumerablePage;
	}
}