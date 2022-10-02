using ArchitectProg.Kernel.Extensions.Interfaces;
using FluentValidation;
using Microservice.Permissions.Core.Contracts.Requests.Permission;
using Microservice.Permissions.Database.Specifications.Permission;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Validators.Permission;

public sealed class UpdatePermissionsRequestValidator : AbstractValidator<UpdatePermissionsRequest>
{
    public UpdatePermissionsRequestValidator(
        IValidator<int> identifierValidator,
        IValidator<string[]> permissionNamesValidator,
        IRepository<PermissionCollectionEntity> repository)
    {
        RuleFor(x => x.AreaId).SetValidator(identifierValidator);
        RuleFor(x => x.RoleId).SetValidator(identifierValidator);

        RuleFor(x => x).MustAsync(async (x, token) =>
        {
            var specification = new PermissionCollectionsSpecification(new[] {x.AreaId}, new[] {x.RoleId});
            var isExists = await repository.Exists(specification, token);
            return isExists;
        }).WithMessage("'PermissionCollection' for such role and area don't exist");

        RuleFor(x => x.Names).SetValidator(permissionNamesValidator);
    }
}