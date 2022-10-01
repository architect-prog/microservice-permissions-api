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
        private readonly IPermissionService permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            this.permissionService = permissionService;
        }

        [ProducesBadRequest]
        [ProducesCreated(typeof(IEnumerable<PermissionCollectionResponse>))]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePermissionsRequest request)
        {
            var result = await permissionService.Create(request);
            var response = result.MatchActionResult(x => CreatedAtAction("GetAll", new { }, x));

            return response;
        }

        [ProducesBadRequest]
        [ProducesOk(typeof(IEnumerable<PermissionCollectionResponse>))]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int[]? areaIds, [FromQuery] int[]? roleIds)
        {
            var result = await permissionService.GetAll(roleIds, areaIds);
            var response = result.MatchActionResult(x => Ok(x));

            return response;
        }

        [ProducesNotFound]
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
        public async Task<IActionResult> Delete(int areaId, string[] permission)
        {
            var result = await permissionService.Delete(areaId, permission);
            var response = result.MatchActionResult(() => NoContent());

            return response;
        }
    }
}