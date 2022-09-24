using ArchitectProg.WebApi.Extensions.Attributes;
using ArchitectProg.WebApi.Extensions.Extensions;
using ArchitectProg.WebApi.Extensions.Responses;
using Microservice.Permissions.Core.Contracts.Requests.Area;
using Microservice.Permissions.Core.Contracts.Responses.Area;
using Microservice.Permissions.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Permissions.Api.Controllers;

[ApiController]
[Route("api/areas")]
public class AreasController : ControllerBase
{
    private readonly IAreaService areaService;

    public AreasController(IAreaService areaService)
    {
        this.areaService = areaService;
    }

    [ProducesBadRequest]
    [ProducesCreated(typeof(int))]
    [HttpPost]
    public async Task<IActionResult> Create(CreateAreaRequest request)
    {
        var result = await areaService.Create(request);
        return CreatedAtAction("Get", new {AreaId = result}, result);
    }

    [ProducesNotFound]
    [ProducesOk(typeof(AreaResponse))]
    [HttpGet("{areaId:int}")]
    public async Task<IActionResult> Get(int areaId)
    {
        var result = await areaService.Get(areaId);
        var response = result.Match<IActionResult>(x => Ok(x), x => NotFound(x?.Message));

        return response;
    }

    [ProducesOk(typeof(CollectionWrapper<AreaResponse>))]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var areas = await areaService.GetAll();
        var count = await areaService.Count();
        var result = areas.WrapCollection(count);

        return Ok(result);
    }

    [ProducesNotFound]
    [ProducesNoContent]
    [ProducesBadRequest]
    [HttpPut("{areaId:int}")]
    public async Task<IActionResult> Update(int areaId, UpdateAreaRequest request)
    {
        var result = await areaService.Update(areaId, request);
        var response = result.Match<IActionResult>(() => NoContent(), x => NotFound(x?.Message));

        return response;
    }

    [ProducesNotFound]
    [ProducesNoContent]
    [HttpDelete("{areaId:int}")]
    public async Task<IActionResult> Delete(int areaId)
    {
        var result = await areaService.Delete(areaId);
        var response = result.Match<IActionResult>(() => NoContent(), x => NotFound(x?.Message));

        return response;
    }
}