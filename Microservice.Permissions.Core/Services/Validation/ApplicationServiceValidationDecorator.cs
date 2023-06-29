using ArchitectProg.Kernel.Extensions.Factories.Interfaces;
using ArchitectProg.Kernel.Extensions.Utils;
using FluentValidation;
using Microservice.Permissions.Core.Constants;
using Microservice.Permissions.Core.Contracts.Requests.Application;
using Microservice.Permissions.Core.Contracts.Responses.Application;
using Microservice.Permissions.Core.Services.Interfaces;

namespace Microservice.Permissions.Core.Services.Validation;

public sealed class ApplicationServiceValidationDecorator : IApplicationService
{
    private readonly IResultFactory resultFactory;
    private readonly IApplicationService applicationService;
    private readonly IValidator<int> identifierValidator;
    private readonly IValidator<(int?, int?)> skipTakeValidator;
    private readonly IValidator<CreateApplicationRequest> createApplicationRequestValidator;
    private readonly IValidator<(int, UpdateApplicationRequest)> updateApplicationRequestValidator;

    public ApplicationServiceValidationDecorator(
        IResultFactory resultFactory,
        IApplicationService applicationService,
        IValidator<int> identifierValidator,
        IValidator<(int?, int?)> skipTakeValidator,
        IValidator<CreateApplicationRequest> createApplicationRequestValidator,
        IValidator<(int, UpdateApplicationRequest)> updateApplicationRequestValidator)
    {
        this.resultFactory = resultFactory;
        this.applicationService = applicationService;
        this.identifierValidator = identifierValidator;
        this.skipTakeValidator = skipTakeValidator;
        this.createApplicationRequestValidator = createApplicationRequestValidator;
        this.updateApplicationRequestValidator = updateApplicationRequestValidator;
    }

    public async Task<Result<ApplicationResponse>> Create(CreateApplicationRequest request)
    {
        var validationResult = await createApplicationRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var failureResult = resultFactory.ValidationFailure<ApplicationResponse>(validationResult.ToString());
            return failureResult;
        }

        return await applicationService.Create(request);
    }

    public Task<Result<ApplicationResponse>> Get(int applicationId)
    {
        var validationResult = identifierValidator.Validate(applicationId);
        if (!validationResult.IsValid)
        {
            var failureResult = resultFactory
                .ResourceNotFoundFailure<ApplicationResponse>(validationResult.ToString());
            return Task.FromResult(failureResult);
        }

        return applicationService.Get(applicationId);
    }

    public Task<Result<IEnumerable<ApplicationResponse>>> GetAll(int? skip, int? take)
    {
        var validationResult = skipTakeValidator.Validate((skip, take));
        if (!validationResult.IsValid)
        {
            var failureResult = resultFactory
                .ResourceNotFoundFailure<IEnumerable<ApplicationResponse>>(validationResult.ToString());
            return Task.FromResult(failureResult);
        }

        return applicationService.GetAll(skip, take);
    }

    public async Task<Result> Update(int applicationId, UpdateApplicationRequest request)
    {
        var requestValidationResult = await updateApplicationRequestValidator
            .ValidateAsync((applicationId, request));

        if (!requestValidationResult.IsValid)
        {
            var failureResult = resultFactory.ValidationFailure(requestValidationResult.ToString());
            return failureResult;
        }

        var result = await applicationService.Update(applicationId, request);
        return result;
    }

    public Task<Result> Delete(int applicationId)
    {
        var validationResult = identifierValidator.Validate(applicationId);
        if (!validationResult.IsValid)
        {
            var failureResult = resultFactory.ResourceNotFoundFailure(Names.Application);
            return Task.FromResult(failureResult);
        }

        return applicationService.Delete(applicationId);
    }

    public Task<int> Count()
    {
        return applicationService.Count();
    }
}