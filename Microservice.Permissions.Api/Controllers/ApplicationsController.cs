using ArchitectProg.WebApi.Extensions.Attributes;
using ArchitectProg.WebApi.Extensions.Extensions;
using ArchitectProg.WebApi.Extensions.Responses;
using Microservice.Permissions.Core.Contracts.Requests.Application;
using Microservice.Permissions.Core.Contracts.Responses.Application;
using Microservice.Permissions.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Permissions.Api.Controllers;

[ApiController]
[Route("api/applications")]
public class ApplicationsController : ControllerBase
{
    private readonly IApplicationService applicationService;

    public ApplicationsController(IApplicationService applicationService)
    {
        this.applicationService = applicationService;
    }

    [ProducesBadRequest]
    [ProducesCreated(typeof(int))]
    [HttpPost]
    public async Task<IActionResult> Create(CreateApplicationRequest request)
    {
        var result = await applicationService.Create(request);
        return CreatedAtAction("Get", new {ApplicationId = result}, result);
    }

    [ProducesNotFound]
    [ProducesOk(typeof(ApplicationResponse))]
    [HttpGet("{applicationId:int}")]
    public async Task<IActionResult> Get(int applicationId)
    {
        var result = await applicationService.Get(applicationId);
        var response = result.Match<IActionResult>(x => Ok(x), x => NotFound(x?.Message));

        return response;
    }

    [ProducesOk(typeof(CollectionWrapper<ApplicationResponse>))]
    [HttpGet]
    public async Task<IActionResult> GetAll(int? skip, int? take)
    {
        var applications = await applicationService.GetAll(skip, take);
        var count = await applicationService.Count();
        var result = applications.WrapCollection(count);

        return Ok(result);
    }

    [ProducesNotFound]
    [ProducesNoContent]
    [ProducesBadRequest]
    [HttpPut("{applicationId:int}")]
    public async Task<IActionResult> Update(int applicationId, UpdateApplicationRequest request)
    {
        var result = await applicationService.Update(applicationId, request);
        var response = result.Match<IActionResult>(() => NoContent(), x => NotFound(x?.Message));

        return response;
    }

    [ProducesNotFound]
    [ProducesNoContent]
    [HttpDelete("{applicationId:int}")]
    public async Task<IActionResult> Delete(int applicationId)
    {
        var result = await applicationService.Delete(applicationId);
        var response = result.Match<IActionResult>(() => NoContent(), x => NotFound(x?.Message));

        return response;
    }
}