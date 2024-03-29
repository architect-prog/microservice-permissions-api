﻿using ArchitectProg.WebApi.Extensions.Attributes;
using ArchitectProg.WebApi.Extensions.Extensions;
using ArchitectProg.WebApi.Extensions.Responses;
using Microservice.Permissions.Api.Extensions;
using Microservice.Permissions.Core.Contracts.Requests.Application;
using Microservice.Permissions.Core.Contracts.Responses.Application;
using Microservice.Permissions.Core.Contracts.Responses.Permission;
using Microservice.Permissions.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Permissions.Api.Controllers;

[ApiController]
[Route("api/applications")]
public sealed class ApplicationsController : ControllerBase
{
    private readonly IPermissionService permissionService;
    private readonly IApplicationService applicationService;

    public ApplicationsController(
        IPermissionService permissionService,
        IApplicationService applicationService)
    {
        this.permissionService = permissionService;
        this.applicationService = applicationService;
    }

    [ProducesBadRequest]
    [ProducesCreated(typeof(ApplicationResponse))]
    [HttpPost]
    public async Task<IActionResult> Create(CreateApplicationRequest request)
    {
        var result = await applicationService.Create(request);
        var response = result.MatchActionResult(x => CreatedAtAction("Get", new {ApplicationId = x?.Id}, x));

        return response;
    }

    [ProducesNotFound]
    [ProducesOk(typeof(ApplicationResponse))]
    [HttpGet("{applicationId:int}")]
    public async Task<IActionResult> Get(int applicationId)
    {
        var result = await applicationService.Get(applicationId);
        var response = result.MatchActionResult(x => Ok(x));

        return response;
    }

    [ProducesNotFound]
    [ProducesOk(typeof(CollectionWrapper<ApplicationResponse>))]
    [HttpGet]
    public async Task<IActionResult> GetAll(int? skip, int? take)
    {
        var result = await applicationService.GetAll(skip, take);
        var count = await applicationService.Count();

        var response = result.MatchActionResult(x => Ok(x?.WrapCollection(count)));
        return response;
    }

    [ProducesNotFound]
    [ProducesNoContent]
    [ProducesBadRequest]
    [HttpPut("{applicationId:int}")]
    public async Task<IActionResult> Update(int applicationId, UpdateApplicationRequest request)
    {
        var result = await applicationService.Update(applicationId, request);
        var response = result.MatchActionResult(() => NoContent());

        return response;
    }

    [ProducesNotFound]
    [ProducesNoContent]
    [HttpDelete("{applicationId:int}")]
    public async Task<IActionResult> Delete(int applicationId)
    {
        var result = await applicationService.Delete(applicationId);
        var response = result.MatchActionResult(() => NoContent());

        return response;
    }

    [ProducesNotFound]
    [ProducesOk(typeof(PermissionCollectionDetailsResponse))]
    [HttpGet("{application}/areas/{area}/roles/{role}/permissions")]
    public async Task<IActionResult> GetAll(string application, string area, string role)
    {
        var result = await permissionService.Get(application, area, role);
        var response = result.MatchActionResult(x => Ok(x));

        return response;
    }
}