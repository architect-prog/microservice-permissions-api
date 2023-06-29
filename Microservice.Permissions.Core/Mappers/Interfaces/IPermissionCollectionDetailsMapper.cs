using ArchitectProg.Kernel.Extensions.Mappers.Interfaces;
using Microservice.Permissions.Core.Contracts.Responses.Permission;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Mappers.Interfaces;

public interface IPermissionCollectionDetailsMapper
    : IMapper<PermissionCollectionEntity, PermissionCollectionDetailsResponse>
{
}