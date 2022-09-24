using ArchitectProg.Kernel.Extensions.Interfaces;
using Microservice.Permissions.Core.Contracts.Responses.Application;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Mappers.Interfaces;

public interface IApplicationMapper : IMapper<ApplicationEntity, ApplicationResponse>
{
}