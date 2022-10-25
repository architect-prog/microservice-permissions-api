using ArchitectProg.Kernel.Extensions.Interfaces;
using FluentValidation;
using Microservice.Permissions.Core.Contracts.Requests.Application;
using Microservice.Permissions.Kernel.Entities;
using Microservice.Permissions.Persistence.EfCore.Specifications.Application;

namespace Microservice.Permissions.Core.Validators.Application;

public sealed class UpdateApplicationRequestValidator
    : AbstractValidator<(int id, UpdateApplicationRequest request)>
{
    public UpdateApplicationRequestValidator(
        IValidator<int> identifierValidator,
        IRepository<ApplicationEntity> repository)
    {
        RuleFor(x => x.id).SetValidator(identifierValidator);

        RuleFor(x => x.request.Name).NotNull();
        RuleFor(x => x.request.Name).NotEmpty();
        RuleFor(x => x.request.Name).MaximumLength(64);
        RuleFor(x => x.request.Name).MustAsync(async (x, name, token) =>
        {
            var specification = new ApplicationByNameSpecification(name);
            var application = await repository.GetOrDefault(specification, token);
            return application is null || application.Id == x.id;
        }).WithMessage("'Name' must be unique.");

        RuleFor(x => x.request.Description).NotNull();
    }
}