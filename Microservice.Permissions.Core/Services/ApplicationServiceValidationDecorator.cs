using ArchitectProg.Kernel.Extensions.Common;
using FluentValidation;
using Microservice.Permissions.Core.Contracts.Requests.Application;
using Microservice.Permissions.Core.Contracts.Responses.Application;
using Microservice.Permissions.Core.Services.Interfaces;

namespace Microservice.Permissions.Core.Services
{
    public sealed class ApplicationServiceValidationDecorator : IApplicationService
    {
        private readonly IApplicationService applicationService;
        private readonly IValidator<(int?, int?)> skipTakeValidator;
        private readonly IValidator<CreateApplicationRequest> createApplicationRequestValidator;
        private readonly IValidator<UpdateApplicationRequest> updateApplicationRequestValidator;

        public ApplicationServiceValidationDecorator(
            IApplicationService applicationService,
            IValidator<(int?, int?)> skipTakeValidator,
            IValidator<CreateApplicationRequest> createApplicationRequestValidator,
            IValidator<UpdateApplicationRequest> updateApplicationRequestValidator)
        {
            this.applicationService = applicationService;
            this.skipTakeValidator = skipTakeValidator;
            this.createApplicationRequestValidator = createApplicationRequestValidator;
            this.updateApplicationRequestValidator = updateApplicationRequestValidator;
        }

        public async Task<Result<int>> Create(CreateApplicationRequest request)
        {
            var validationResult = await createApplicationRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var failureResult = ResultFactory.ValidationFailure<int>(validationResult.ToString());
                return failureResult;
            }

            return await applicationService.Create(request);
        }

        public Task<Result<ApplicationResponse>> Get(int applicationId)
        {
            return applicationService.Get(applicationId);
        }

        public Task<Result<IEnumerable<ApplicationResponse>>> GetAll(int? skip, int? take)
        {
            var validationResult = skipTakeValidator.Validate((skip, take));
            if (!validationResult.IsValid)
            {
                var failureResult = ResultFactory
                    .ValidationFailure<IEnumerable<ApplicationResponse>>(validationResult.ToString());
                return Task.FromResult(failureResult);
            }

            return applicationService.GetAll(skip, take);
        }

        public async Task<Result> Update(int applicationId, UpdateApplicationRequest request)
        {
            var validationResult = await updateApplicationRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var failureResult = ResultFactory.ValidationFailure(validationResult.ToString());
                return failureResult;
            }

            var result = await applicationService.Update(applicationId, request);
            return result;
        }

        public Task<Result> Delete(int applicationId)
        {
            return applicationService.Delete(applicationId);
        }

        public Task<int> Count()
        {
            return applicationService.Count();
        }
    }
}