using ArchitectProg.WebApi.Extensions.Attributes;
using ArchitectProg.WebApi.Extensions.Extensions;
using ArchitectProg.WebApi.Extensions.Responses;
using Microservice.Permissions.Api.Extensions;
using Microservice.Permissions.Core.Contracts.Requests.Area;
using Microservice.Permissions.Core.Contracts.Responses.Area;
using Microservice.Permissions.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Permissions.Api.Controllers;

[ApiController]
[Route("api/areas")]
public sealed class AreasController : ControllerBase
{
    private readonly IAreaService areaService;

    public AreasController(IAreaService areaService)
    {
        this.areaService = areaService;
    }

    [ProducesBadRequest]
    [ProducesCreated(typeof(AreaResponse))]
    [HttpPost]
    public async Task<IActionResult> Create(CreateAreaRequest request)
    {
        var result = await areaService.Create(request);
        var response = result.MatchActionResult(x => CreatedAtAction("Get", new {AreaId = x?.Id}, x));
        return response;
    }

    [ProducesNotFound]
    [ProducesOk(typeof(AreaResponse))]
    [HttpGet("{areaId:int}")]
    public async Task<IActionResult> Get(int areaId)
    {
        var result = await areaService.Get(areaId);
        var response = result.MatchActionResult(x => Ok(x));

        return response;
    }

    [ProducesNotFound]
    [ProducesOk(typeof(CollectionWrapper<AreaResponse>))]
    [HttpGet]
    public async Task<IActionResult> GetAll(int? applicationId, int? skip, int? take)
    {
        var result = await areaService.GetAll(applicationId, skip, take);
        var count = await areaService.Count(applicationId);

        var response = result.MatchActionResult(x => Ok(x?.WrapCollection(count)));
        return response;
    }

    [ProducesNotFound]
    [ProducesNoContent]
    [ProducesBadRequest]
    [HttpPut("{areaId:int}")]
    public async Task<IActionResult> Update(int areaId, UpdateAreaRequest request)
    {
        var result = await areaService.Update(areaId, request);
        var response = result.MatchActionResult(() => NoContent());

        return response;
    }

    [ProducesNotFound]
    [ProducesNoContent]
    [HttpDelete("{areaId:int}")]
    public async Task<IActionResult> Delete(int areaId)
    {
        var result = await areaService.Delete(areaId);
        var response = result.MatchActionResult(() => NoContent());

        return response;
    }
}