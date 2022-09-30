using ArchitectProg.Kernel.Extensions.Common;
using FluentValidation;
using Microservice.Permissions.Core.Contracts.Requests.Role;
using Microservice.Permissions.Core.Contracts.Responses.Role;
using Microservice.Permissions.Core.Services.Interfaces;

namespace Microservice.Permissions.Core.Services
{
    public sealed class RoleServiceValidationDecorator : IRoleService
    {
        private readonly IRoleService roleService;
        private readonly IValidator<int> identifierValidator;
        private readonly IValidator<(int?, int?)> skipTakeValidator;
        private readonly IValidator<CreateRoleRequest> createRoleRequestValidator;
        private readonly IValidator<(int, UpdateRoleRequest)> updateRoleRequestValidator;

        public RoleServiceValidationDecorator(
            IRoleService roleService,
            IValidator<int> identifierValidator,
            IValidator<(int?, int?)> skipTakeValidator,
            IValidator<CreateRoleRequest> createRoleRequestValidator,
            IValidator<(int, UpdateRoleRequest)> updateRoleRequestValidator)
        {
            this.roleService = roleService;
            this.identifierValidator = identifierValidator;
            this.skipTakeValidator = skipTakeValidator;
            this.createRoleRequestValidator = createRoleRequestValidator;
            this.updateRoleRequestValidator = updateRoleRequestValidator;
        }

        public async Task<Result<int>> Create(CreateRoleRequest request)
        {
            var validationResult = await createRoleRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var failureResult = ResultFactory.ValidationFailure<int>(validationResult.ToString());
                return failureResult;
            }

            var result = await roleService.Create(request);
            return result;
        }

        public Task<Result<RoleResponse>> Get(int roleId)
        {
            var validationResult = identifierValidator.Validate(roleId);
            if (!validationResult.IsValid)
            {
                var failureResult = ResultFactory.ResourceNotFoundFailure<RoleResponse>(validationResult.ToString());
                return Task.FromResult(failureResult);
            }

            return roleService.Get(roleId);
        }

        public Task<Result<IEnumerable<RoleResponse>>> GetAll(int? skip = null, int? take = null)
        {
            var validationResult = skipTakeValidator.Validate((skip, take));
            if (!validationResult.IsValid)
            {
                var failureResult =
                    ResultFactory.ResourceNotFoundFailure<IEnumerable<RoleResponse>>(validationResult.ToString());
                return Task.FromResult(failureResult);
            }

            return roleService.GetAll(skip, take);
        }

        public async Task<Result> Update(int roleId, UpdateRoleRequest request)
        {
            var validationResult = await updateRoleRequestValidator.ValidateAsync((roleId, request));
            if (!validationResult.IsValid)
            {
                var failureResult = ResultFactory.ValidationFailure(validationResult.ToString());
                return failureResult;
            }

            var result = await roleService.Update(roleId, request);
            return result;
        }

        public Task<Result> Delete(int roleId)
        {
            var validationResult = identifierValidator.Validate(roleId);
            if (!validationResult.IsValid)
            {
                var failureResult = ResultFactory.ResourceNotFoundFailure(validationResult.ToString());
                return Task.FromResult(failureResult);
            }

            return roleService.Delete(roleId);
        }

        public Task<int> Count()
        {
            return roleService.Count();
        }
    }
}