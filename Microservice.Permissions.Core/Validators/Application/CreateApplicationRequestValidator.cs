using ArchitectProg.Kernel.Extensions.Interfaces;
using FluentValidation;
using Microservice.Permissions.Core.Contracts.Requests.Application;
using Microservice.Permissions.Database.Specifications.Application;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Validators.Application;

public sealed class CreateApplicationRequestValidator : AbstractValidator<CreateApplicationRequest>
{
    public CreateApplicationRequestValidator(IRepository<ApplicationEntity> repository)
    {
        RuleFor(x => x.Name).NotNull();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(64);
        RuleFor(x => x.Name).MustAsync(async (x, token) =>
        {
            var specification = new ApplicationByNameSpecification(x);
            var isExist = await repository.Exists(specification, token);
            return !isExist;
        }).WithMessage("'Name' must be unique.");

        RuleFor(x => x.Description).NotNull();
    }
}