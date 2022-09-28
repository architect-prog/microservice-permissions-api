using ArchitectProg.Kernel.Extensions.Interfaces;
using FluentValidation;
using Microservice.Permissions.Core.Contracts.Requests.Area;
using Microservice.Permissions.Database.Specifications.Application;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Validators.Area
{
    public class CreateAreaRequestValidator : AbstractValidator<CreateAreaRequest>
    {
        public CreateAreaRequestValidator(IRepository<ApplicationEntity> repository)
        {
            RuleFor(x => x.ApplicationId).GreaterThan(0);

            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Name).MustAsync(async (request, x, token) =>
            {
                var specification = new ApplicationWithAreaSpecification(request.ApplicationId, x!);
                var application = await repository.GetOrDefault(specification, token);
                if (application is null)
                    return false;

                return application.Areas?.Count() == 0;
            }).WithMessage("'Name' must be unique.");
        }
    }
}