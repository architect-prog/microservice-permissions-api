using Microservice.Permissions.Core.Contracts.Requests.Area;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Creators.Interfaces;

public interface IAreaCreator
{
    AreaEntity Create(CreateAreaRequest request);
}