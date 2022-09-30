using ArchitectProg.Kernel.Extensions.Common;
using FluentValidation;
using Microservice.Permissions.Core.Contracts.Requests.Area;
using Microservice.Permissions.Core.Contracts.Responses.Area;
using Microservice.Permissions.Core.Services.Interfaces;

namespace Microservice.Permissions.Core.Services
{
    public sealed class AreaServiceValidationDecorator : IAreaService
    {
        private readonly IAreaService areaService;
        private readonly IValidator<int> identifierValidator;
        private readonly IValidator<(int?, int?)> skipTakeValidator;
        private readonly IValidator<CreateAreaRequest> createAreaRequestValidator;
        private readonly IValidator<(int, UpdateAreaRequest)> updateAreaRequestValidator;

        public AreaServiceValidationDecorator(
            IAreaService areaService,
            IValidator<int> identifierValidator,
            IValidator<(int?, int?)> skipTakeValidator,
            IValidator<CreateAreaRequest> createAreaRequestValidator,
            IValidator<(int, UpdateAreaRequest)> updateAreaRequestValidator)
        {
            this.areaService = areaService;
            this.identifierValidator = identifierValidator;
            this.skipTakeValidator = skipTakeValidator;
            this.createAreaRequestValidator = createAreaRequestValidator;
            this.updateAreaRequestValidator = updateAreaRequestValidator;
        }

        public async Task<Result<AreaResponse>> Create(CreateAreaRequest request)
        {
            var validationResult = await createAreaRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var failureResult = ResultFactory.ValidationFailure<AreaResponse>(validationResult.ToString());
                return failureResult;
            }

            return await areaService.Create(request);
        }

        public Task<Result<AreaResponse>> Get(int areaId)
        {
            var validationResult = identifierValidator.Validate(areaId);
            if (!validationResult.IsValid)
            {
                var failureResult = ResultFactory.ResourceNotFoundFailure<AreaResponse>(validationResult.ToString());
                return Task.FromResult(failureResult);
            }

            return areaService.Get(areaId);
        }

        public Task<Result<IEnumerable<AreaResponse>>> GetAll(int? applicationId, int? skip, int? take)
        {
            var validationResult = skipTakeValidator.Validate((skip, take));
            if (!validationResult.IsValid)
            {
                var failureResult = ResultFactory
                    .ResourceNotFoundFailure<IEnumerable<AreaResponse>>(validationResult.ToString());
                return Task.FromResult(failureResult);
            }

            return areaService.GetAll(applicationId, skip, take);
        }

        public async Task<Result> Update(int areaId, UpdateAreaRequest request)
        {
            var validationResult = await updateAreaRequestValidator.ValidateAsync((areaId, request));
            if (!validationResult.IsValid)
            {
                var failureResult = ResultFactory.ValidationFailure(validationResult.ToString());
                return failureResult;
            }

            var result = await areaService.Update(areaId, request);
            return result;
        }

        public Task<Result> Delete(int areaId)
        {
            var validationResult = identifierValidator.Validate(areaId);
            if (!validationResult.IsValid)
            {
                var failureResult = ResultFactory.ResourceNotFoundFailure(validationResult.ToString());
                return Task.FromResult(failureResult);
            }

            return areaService.Delete(areaId);
        }

        public Task<int> Count()
        {
            return areaService.Count();
        }
    }
}