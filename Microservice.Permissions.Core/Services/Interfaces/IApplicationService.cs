using ArchitectProg.Kernel.Extensions.Common;
using Microservice.Permissions.Core.Contracts.Requests.Application;
using Microservice.Permissions.Core.Contracts.Responses.Application;

namespace Microservice.Permissions.Core.Services.Interfaces
{
    public interface IApplicationService
    {
        Task<Result<int>> Create(CreateApplicationRequest request);
        Task<Result<ApplicationResponse>> Get(int applicationId);
        Task<Result<IEnumerable<ApplicationResponse>>> GetAll(int? skip, int? take);
        Task<Result> Update(int applicationId, UpdateApplicationRequest request);
        Task<Result> Delete(int applicationId);
        Task<int> Count();
    }
}