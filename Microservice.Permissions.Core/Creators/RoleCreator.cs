using Microservice.Permissions.Core.Contracts.Requests.Role;
using Microservice.Permissions.Core.Creators.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Creators
{
    public class RoleCreator : IRoleCreator
    {
        public RoleEntity Create(CreateRoleRequest request)
        {
            var result = new RoleEntity
            {
                Name = request.Name
            };

            return result;
        }
    }
}