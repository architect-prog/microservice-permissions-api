using ArchitectProg.Kernel.Extensions.Interfaces;
using ArchitectProg.Kernel.Extensions.Specifications;
using FluentValidation;
using Microservice.Permissions.Core.Contracts.Requests.Area;
using Microservice.Permissions.Database.Specifications.Area;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Validators.Area
{
    public class UpdateAreaRequestValidator : AbstractValidator<(int id, UpdateAreaRequest request)>
    {
        public UpdateAreaRequestValidator(
            IValidator<int> identifierValidator,
            IRepository<AreaEntity> areaRepository,
            IRepository<ApplicationEntity> applicationRepository)
        {
            RuleFor(x => x.id).SetValidator(identifierValidator);

            RuleFor(x => x.request.ApplicationId).GreaterThan(0);
            RuleFor(x => x.request.ApplicationId).MustAsync(async (x, token) =>
            {
                var specification = SpecificationFactory.ByIdSpecification<ApplicationEntity, int>(x);
                var isExists = await applicationRepository.Exists(specification, token);
                return isExists;
            }).WithMessage("'Application' with such id don't exist");

            RuleFor(x => x.request.Name).NotNull().NotEmpty();
            RuleFor(x => x.request.Name).MustAsync(async (x, name, token) =>
            {
                var specification = new AreaByNameSpecification(x.request.ApplicationId, name);
                var area = await areaRepository.GetOrDefault(specification, token);
                return area is null || area.Id == x.id;
            }).WithMessage("'Name' must be unique.");
        }
    }
}