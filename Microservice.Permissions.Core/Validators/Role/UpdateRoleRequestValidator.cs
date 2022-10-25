using ArchitectProg.Kernel.Extensions.Interfaces;
using FluentValidation;
using Microservice.Permissions.Core.Contracts.Requests.Role;
using Microservice.Permissions.Kernel.Entities;
using Microservice.Permissions.Persistence.EfCore.Specifications.Role;

namespace Microservice.Permissions.Core.Validators.Role;

public sealed class UpdateRoleRequestValidator : AbstractValidator<(int id, UpdateRoleRequest request)>
{
    public UpdateRoleRequestValidator(
        IValidator<int> identifierValidator,
        IRepository<RoleEntity> repository)
    {
        RuleFor(x => x.id).SetValidator(identifierValidator);

        RuleFor(x => x.request.Name).NotNull();
        RuleFor(x => x.request.Name).NotEmpty();
        RuleFor(x => x.request.Name).MaximumLength(64);
        RuleFor(x => x.request.Name).MustAsync(async (x, name, token) =>
        {
            var specification = new RoleByNameSpecification(name);
            var role = await repository.GetOrDefault(specification, token);
            return role is null || role.Id == x.id;
        }).WithMessage("'Name' must be unique.");
    }
}