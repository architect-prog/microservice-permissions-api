using ArchitectProg.WebApi.Extensions.Attributes;
using Microservice.Permissions.Api.Extensions;
using Microservice.Permissions.Core.Contracts.Requests.Permission;
using Microservice.Permissions.Core.Contracts.Responses.Permission;
using Microservice.Permissions.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Permissions.Api.Controllers;

[ApiController]
[Route("api/permissions")]
public sealed class PermissionsController : ControllerBase
{
    private readonly IPermissionService permissionService;

    public PermissionsController(IPermissionService permissionService)
    {
        this.permissionService = permissionService;
    }

    [ProducesNotFound]
    [ProducesOk(typeof(IEnumerable<PermissionCollectionResponse>))]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int[]? areaIds, [FromQuery] int[]? roleIds)
    {
        var result = await permissionService.GetAll(areaIds, roleIds);
        var response = result.MatchActionResult(x => Ok(x));

        return response;
    }

    [ProducesNotFound]
    [ProducesBadRequest]
    [ProducesNoContent]
    [HttpPut]
    public async Task<IActionResult> Update(UpdatePermissionsRequest request)
    {
        var result = await permissionService.Update(request);
        var response = result.MatchActionResult(() => NoContent());

        return response;
    }

    [ProducesNotFound]
    [ProducesNoContent]
    [HttpDelete]
    public async Task<IActionResult> Delete(int areaId, [FromQuery] string[] permissions)
    {
        var result = await permissionService.Delete(areaId, permissions);
        var response = result.MatchActionResult(() => NoContent());

        return response;
    }
}