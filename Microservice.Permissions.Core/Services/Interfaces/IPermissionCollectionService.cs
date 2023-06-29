using ArchitectProg.Kernel.Extensions.Utils;

namespace Microservice.Permissions.Core.Services.Interfaces;

public interface IPermissionCollectionService
{
    Task<Result<IEnumerable<int>>> CreateForRole(int roleId);
    Task<Result<IEnumerable<int>>> CreateForArea(int areaId);
}