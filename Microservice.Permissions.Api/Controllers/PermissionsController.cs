using ArchitectProg.WebApi.Extensions.Attributes;
using Microservice.Permissions.Api.Extensions;
using Microservice.Permissions.Core.Contracts.Requests.Permissions;
using Microservice.Permissions.Core.Contracts.Responses.Permission;
using Microservice.Permissions.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Permissions.Api.Controllers
{
    [ApiController]
    [Route("api/permissions")]
    public sealed class PermissionsController : ControllerBase
    {
        private readonly IPermissionCollectionService permissionCollectionService;

        public PermissionsController(IPermissionCollectionService permissionCollectionService)
        {
            this.permissionCollectionService = permissionCollectionService;
        }

        [ProducesBadRequest]
        [ProducesCreated(typeof(IEnumerable<PermissionCollectionResponse>))]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePermissionsRequest request)
        {
            var result = await permissionCollectionService.Create(request);
            var response = result.MatchActionResult(x => CreatedAtAction("GetAll", new { }, x));

            return response;
        }

        [ProducesBadRequest]
        [ProducesOk(typeof(IEnumerable<PermissionCollectionResponse>))]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int[]? areaIds, [FromQuery] int[]? roleIds)
        {
            var permissions = await permissionCollectionService.GetAll(roleIds, areaIds);
            return Ok(permissions);
        }

        [ProducesOk(typeof(IEnumerable<PermissionCollectionResponse>))]
        [HttpGet("areas/{areaId:int}/roles/{roleId:int}")]
        public async Task<IActionResult> Get(int roleId, int areaId)
        {
            var permissions = await permissionCollectionService.GetAll(new[] {roleId}, new[] {areaId});
            return Ok(permissions);
        }

        // [ProducesOk(typeof(IEnumerable<PermissionsResponse>))]
        // [HttpGet("applications/{application}/areas/{area}/roles/{role}")]
        // public async Task<IActionResult> Permissions(string application, string role, string area)
        // {
        //     var permissions = await permissionService.GetAll(null, null);
        //     return Ok(permissions);
        // }
    }
}