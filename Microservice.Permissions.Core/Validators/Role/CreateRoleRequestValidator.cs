using ArchitectProg.Kernel.Extensions.Interfaces;
using FluentValidation;
using Microservice.Permissions.Core.Contracts.Requests.Role;
using Microservice.Permissions.Database.Specifications.Role;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Validators.Role
{
    public sealed class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequest>
    {
        public CreateRoleRequestValidator(IRepository<RoleEntity> repository)
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Name).MustAsync(async (x, token) =>
            {
                var specification = new RoleByNameSpecification(x);
                var isExist = await repository.Exists(specification, token);
                return !isExist;
            }).WithMessage("'Name' must be unique.");
        }
    }
}