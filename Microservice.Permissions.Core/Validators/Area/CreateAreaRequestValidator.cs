﻿using ArchitectProg.Kernel.Extensions.Interfaces;
using ArchitectProg.Kernel.Extensions.Specifications;
using FluentValidation;
using Microservice.Permissions.Core.Contracts.Requests.Area;
using Microservice.Permissions.Database.Specifications.Area;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Validators.Area
{
    public sealed class CreateAreaRequestValidator : AbstractValidator<CreateAreaRequest>
    {
        public CreateAreaRequestValidator(
            IRepository<AreaEntity> areaRepository,
            IRepository<ApplicationEntity> applicationRepository)
        {
            RuleFor(x => x.ApplicationId).GreaterThan(0);
            RuleFor(x => x.ApplicationId).MustAsync(async (x, token) =>
            {
                var specification = SpecificationFactory.ByIdSpecification<ApplicationEntity, int>(x);
                var isExists = await applicationRepository.Exists(specification, token);
                return isExists;
            }).WithMessage("'Application' with such id don't exist");

            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Name).MustAsync(async (x, name, token) =>
            {
                var specification = new AreaByNameSpecification(x.ApplicationId, name);
                var isExists = await areaRepository.Exists(specification, token);
                return !isExists;
            }).WithMessage("'Name' must be unique.");
        }
    }
}