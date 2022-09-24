using ArchitectProg.Kernel.Extensions.Common;
using Microservice.Permissions.Core.Contracts.Requests.Application;
using Microservice.Permissions.Core.Contracts.Responses.Application;

namespace Microservice.Permissions.Core.Services.Interfaces;

public interface IApplicationService
{
    Task<int> Create(CreateApplicationRequest request);
    Task<Result<ApplicationResponse>> Get(int applicationId);
    Task<IEnumerable<ApplicationResponse>> GetAll();
    Task<Result> Update(int applicationId, UpdateApplicationRequest request);
    Task<Result> Delete(int applicationId);
    Task<int> Count();
}