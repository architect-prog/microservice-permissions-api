using ArchitectProg.Kernel.Extensions.Interfaces;
using FluentValidation;
using Microservice.Permissions.Core.Contracts.Requests.Role;
using Microservice.Permissions.Kernel.Entities;
using Microservice.Permissions.Persistence.EfCore.Specifications.Role;

namespace Microservice.Permissions.Core.Validators.Role;

public sealed class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequest>
{
    public CreateRoleRequestValidator(IRepository<RoleEntity> repository)
    {
        RuleFor(x => x.Name).NotNull();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(64);
        RuleFor(x => x.Name).MustAsync(async (x, token) =>
        {
            var specification = new RoleByNameSpecification(x);
            var isExist = await repository.Exists(specification, token);
            return !isExist;
        }).WithMessage("'Name' must be unique.");
    }
}