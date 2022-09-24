using ArchitectProg.WebApi.Extensions.Attributes;
using ArchitectProg.WebApi.Extensions.Extensions;
using ArchitectProg.WebApi.Extensions.Responses;
using Microservice.Permissions.Core.Contracts.Requests.Role;
using Microservice.Permissions.Core.Contracts.Responses.Role;
using Microservice.Permissions.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Permissions.Api.Controllers;

[ApiController]
[Route("api/roles")]
public class RolesController : ControllerBase
{
    private readonly IRoleService roleService;

    public RolesController(IRoleService roleService)
    {
        this.roleService = roleService;
    }

    [ProducesBadRequest]
    [ProducesCreated(typeof(int))]
    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleRequest request)
    {
        var result = await roleService.Create(request);
        return CreatedAtAction("Get", new {RoleId = result}, result);
    }

    [ProducesNotFound]
    [ProducesOk(typeof(RoleResponse))]
    [HttpGet("{roleId:int}")]
    public async Task<IActionResult> Get(int roleId)
    {
        var result = await roleService.Get(roleId);
        var response = result.Match<IActionResult>(x => Ok(x), x => NotFound(x?.Message));

        return response;
    }

    [ProducesOk(typeof(CollectionWrapper<RoleResponse>))]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roles = await roleService.GetAll();
        var count = await roleService.Count();
        var result = roles.WrapCollection(count);

        return Ok(result);
    }

    [ProducesNotFound]
    [ProducesNoContent]
    [ProducesBadRequest]
    [HttpPut("{roleId:int}")]
    public async Task<IActionResult> Update(int roleId, UpdateRoleRequest request)
    {
        var result = await roleService.Update(roleId, request);
        var response = result.Match<IActionResult>(() => NoContent(), x => NotFound(x?.Message));

        return response;
    }

    [ProducesNotFound]
    [ProducesNoContent]
    [HttpDelete("{roleId:int}")]
    public async Task<IActionResult> Delete(int roleId)
    {
        var result = await roleService.Delete(roleId);
        var response = result.Match<IActionResult>(() => NoContent(), x => NotFound(x?.Message));

        return response;
    }
}