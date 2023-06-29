using ArchitectProg.Kernel.Extensions.Factories.Interfaces;
using ArchitectProg.Kernel.Extensions.Utils;
using FluentValidation;
using Microservice.Permissions.Core.Constants;
using Microservice.Permissions.Core.Contracts.Requests.Permission;
using Microservice.Permissions.Core.Contracts.Responses.Permission;
using Microservice.Permissions.Core.Services.Interfaces;

namespace Microservice.Permissions.Core.Services.Validation;

public sealed class PermissionServiceValidationDecorator : IPermissionService
{
    private readonly IResultFactory resultFactory;
    private readonly IPermissionService permissionService;
    private readonly IValidator<int> identifierValidator;
    private readonly IValidator<int[]> identifierArrayValidator;
    private readonly IValidator<string[]> permissionNamesValidator;
    private readonly IValidator<UpdatePermissionsRequest> updatePermissionsRequestValidator;

    public PermissionServiceValidationDecorator(
        IResultFactory resultFactory,
        IPermissionService permissionService,
        IValidator<int> identifierValidator,
        IValidator<int[]> identifierArrayValidator,
        IValidator<string[]> permissionNamesValidator,
        IValidator<UpdatePermissionsRequest> updatePermissionsRequestValidator)
    {
        this.resultFactory = resultFactory;
        this.permissionService = permissionService;
        this.identifierValidator = identifierValidator;
        this.identifierArrayValidator = identifierArrayValidator;
        this.permissionNamesValidator = permissionNamesValidator;
        this.updatePermissionsRequestValidator = updatePermissionsRequestValidator;
    }

    public Task<Result<PermissionCollectionDetailsResponse>> Get(string application, string area, string role)
    {
        if (string.IsNullOrWhiteSpace(application)
            || string.IsNullOrWhiteSpace(area)
            || string.IsNullOrWhiteSpace(role))
        {
            var failureResult = resultFactory
                .ResourceNotFoundFailure<PermissionCollectionDetailsResponse>(Names.Permission);
            return Task.FromResult(failureResult);
        }

        return permissionService.Get(application, area, role);
    }

    public Task<Result<IEnumerable<PermissionCollectionResponse>>> GetAll(int[]? areaIds, int[]? roleIds)
    {
        var areasValidationResult = identifierArrayValidator.Validate(areaIds ?? Array.Empty<int>());
        if (!areasValidationResult.IsValid)
        {
            var failureResult = resultFactory
                .ResourceNotFoundFailure<IEnumerable<PermissionCollectionResponse>>(Names.Permission);
            return Task.FromResult(failureResult);
        }

        var rolesValidationResult = identifierArrayValidator.Validate(roleIds ?? Array.Empty<int>());
        if (!rolesValidationResult.IsValid)
        {
            var failureResult = resultFactory
                .ResourceNotFoundFailure<IEnumerable<PermissionCollectionResponse>>(Names.Permission);
            return Task.FromResult(failureResult);
        }

        return permissionService.GetAll(areaIds, roleIds);
    }

    public async Task<Result> Update(UpdatePermissionsRequest request)
    {
        var validationResult = await updatePermissionsRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var failureResult = resultFactory.ValidationFailure(validationResult.ToString());
            return failureResult;
        }

        var result = await permissionService.Update(request);
        return result;
    }

    public Task<Result> Delete(int areaId, string[] permissions)
    {
        var permissionsValidationResult = permissionNamesValidator.Validate(permissions);
        if (!permissionsValidationResult.IsValid)
        {
            var failureResult = resultFactory.ResourceNotFoundFailure(Names.Permission);
            return Task.FromResult(failureResult);
        }

        var areaValidationResult = identifierValidator.Validate(areaId);
        if (!areaValidationResult.IsValid)
        {
            var failureResult = resultFactory.ResourceNotFoundFailure(Names.Permission);
            return Task.FromResult(failureResult);
        }

        return permissionService.Delete(areaId, permissions);
    }
}