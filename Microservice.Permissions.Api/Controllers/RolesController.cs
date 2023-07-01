using ArchitectProg.WebApi.Extensions.Attributes;
using ArchitectProg.WebApi.Extensions.Extensions;
using ArchitectProg.WebApi.Extensions.Responses;
using Microservice.Permissions.Api.Extensions;
using Microservice.Permissions.Azure.Bus.Contracts;
using Microservice.Permissions.Azure.Bus.Services.Interfaces;
using Microservice.Permissions.Core.Contracts.Requests.Role;
using Microservice.Permissions.Core.Contracts.Responses.Role;
using Microservice.Permissions.Core.Services.Interfaces;
using Microservice.Permissions.Messaging.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Permissions.Api.Controllers;

[ApiController]
[Route("api/roles")]
public sealed class RolesController : ControllerBase
{
    private readonly IRoleService roleService;
    private readonly IBusMessagePublisher busMessagePublisher;
    private readonly IAzureBusMessagePublisher azureBusMessagePublisher;

    public RolesController(
        IRoleService roleService,
        IBusMessagePublisher busMessagePublisher,
        IAzureBusMessagePublisher azureBusMessagePublisher)
    {
        this.roleService = roleService;
        this.busMessagePublisher = busMessagePublisher;
        this.azureBusMessagePublisher = azureBusMessagePublisher;
    }

    [ProducesBadRequest]
    [ProducesCreated(typeof(RoleResponse))]
    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleRequest request)
    {
        var result = await roleService.Create(request);
        var response = result.MatchActionResult(x => CreatedAtAction("Get", new { RoleId = x?.Id }, x));

        return response;
    }

    [ProducesNotFound]
    [ProducesOk(typeof(RoleResponse))]
    [HttpGet("{roleId:int}")]
    public async Task<IActionResult> Get(int roleId)
    {
        var result = await roleService.Get(roleId);
        var response = result.MatchActionResult(x => Ok(x));

        await azureBusMessagePublisher.PublishMessage(new BusMessage<int>
        {
            Queue = "send.email",
            Payload = 1
        });

        return response;
    }

    [ProducesNotFound]
    [ProducesOk(typeof(CollectionWrapper<RoleResponse>))]
    [HttpGet]
    public async Task<IActionResult> GetAll(int? skip, int? take)
    {
        var result = await roleService.GetAll(skip, take);
        var count = await roleService.Count();

        await busMessagePublisher.PublishMessage(new Messaging.Contracts.BusMessage<int>
        {
            Exchange = "email",
            Queue = "send.email",
            Payload = 1
        });

        var response = result.MatchActionResult(x => Ok(x?.WrapCollection(count)));
        return response;
    }

    [ProducesNotFound]
    [ProducesNoContent]
    [ProducesBadRequest]
    [HttpPut("{roleId:int}")]
    public async Task<IActionResult> Update(int roleId, UpdateRoleRequest request)
    {
        var result = await roleService.Update(roleId, request);
        var response = result.MatchActionResult(() => NoContent());

        return response;
    }

    [ProducesNotFound]
    [ProducesNoContent]
    [HttpDelete("{roleId:int}")]
    public async Task<IActionResult> Delete(int roleId)
    {
        var result = await roleService.Delete(roleId);
        var response = result.MatchActionResult(() => NoContent());

        return response;
    }
}