using Microservice.Permissions.Core.Contracts.Requests.Application;
using Microservice.Permissions.Core.Creators.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Creators;

public sealed class ApplicationCreator : IApplicationCreator
{
    public ApplicationEntity Create(CreateApplicationRequest request)
    {
        var result = new ApplicationEntity
        {
            Name = request.Name,
            Description = request.Description
        };

        return result;
    }
}