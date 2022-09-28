using ArchitectProg.Kernel.Extensions.Common;
using Microservice.Permissions.Core.Contracts.Requests.Area;
using Microservice.Permissions.Core.Contracts.Responses.Area;

namespace Microservice.Permissions.Core.Services.Interfaces
{
    public interface IAreaService
    {
        Task<Result<int>> Create(CreateAreaRequest request);
        Task<Result<AreaResponse>> Get(int areaId);
        Task<Result<IEnumerable<AreaResponse>>> GetAll(int? applicationId, int? skip, int? take);
        Task<Result> Update(int areaId, UpdateAreaRequest request);
        Task<Result> Delete(int areaId);
        Task<int> Count();
    }
}