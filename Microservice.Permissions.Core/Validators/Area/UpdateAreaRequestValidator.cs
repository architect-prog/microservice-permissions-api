using ArchitectProg.Kernel.Extensions.Interfaces;
using FluentValidation;
using Microservice.Permissions.Core.Contracts.Requests.Area;
using Microservice.Permissions.Database.Specifications.Area;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Validators.Area
{
    public class UpdateAreaRequestValidator : AbstractValidator<UpdateAreaRequest>
    {
        public UpdateAreaRequestValidator(IRepository<AreaEntity> repository)
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.ApplicationId).GreaterThan(0);

            RuleFor(x => x.Name).MustAsync(async (r, x, token) =>
            {
                var specification = new AreaByNameSpecification(r.ApplicationId, x);
                var isExist = await repository.Exists(specification, token);
                return !isExist;
            }).WithMessage("'Name' must be unique.");
        }
    }
}