using ArchitectProg.WebApi.Extensions.Attributes;
using Microservice.Permissions.Core.Contracts.Responses.Permission;
using Microservice.Permissions.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Permissions.Api.Controllers
{
    [ApiController]
    [Route("api/permissions")]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            this.permissionService = permissionService;
        }

        [ProducesOk(typeof(IEnumerable<PermissionCollectionResponse>))]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int[]? areaIds, [FromQuery] int[]? roleIds)
        {
            var permissions = await permissionService.GetAll(roleIds, areaIds);
            return Ok(permissions);
        }

        [ProducesOk(typeof(IEnumerable<PermissionCollectionResponse>))]
        [HttpGet("areas/{areaId:int}/roles/{roleId:int}")]
        public async Task<IActionResult> Get(int roleId, int areaId)
        {
            var permissions = await permissionService.GetAll(new[] {roleId}, new[] {areaId});
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