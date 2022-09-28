﻿using ArchitectProg.Kernel.Extensions.Interfaces;
using FluentValidation;
using Microservice.Permissions.Core.Contracts.Requests.Application;
using Microservice.Permissions.Database.Specifications.Application;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Validators.Application
{
    public class UpdateApplicationRequestValidator : AbstractValidator<UpdateApplicationRequest>
    {
        public UpdateApplicationRequestValidator(IRepository<ApplicationEntity> repository)
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Name).MustAsync(async (x, token) =>
            {
                var specification = new ApplicationByNameSpecification(x!);
                var isExist = await repository.Exists(specification, token);
                return !isExist;
            }).WithMessage("'Name' must be unique.");
        }
    }
}