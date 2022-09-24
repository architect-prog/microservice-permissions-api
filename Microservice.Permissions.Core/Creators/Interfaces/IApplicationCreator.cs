using Microservice.Permissions.Core.Contracts.Requests.Application;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Creators.Interfaces;

public interface IApplicationCreator
{
    ApplicationEntity Create(CreateApplicationRequest request);
}