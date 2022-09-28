using ArchitectProg.Kernel.Extensions.Common;

namespace Microservice.Permissions.Core.Services.Interfaces
{
    public interface IAreaRoleService
    {
        Task<Result<IEnumerable<int>>> CreateForRole(int roleId);
        Task<Result<IEnumerable<int>>> CreateForArea(int areaId);
    }
}