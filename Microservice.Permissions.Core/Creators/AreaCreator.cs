using Microservice.Permissions.Core.Contracts.Requests.Area;
using Microservice.Permissions.Core.Creators.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Creators
{
    public sealed class AreaCreator : IAreaCreator
    {
        public AreaEntity Create(CreateAreaRequest request)
        {
            var result = new AreaEntity
            {
                ApplicationId = request.ApplicationId,
                Name = request.Name
            };

            return result;
        }
    }
}