using ArchitectProg.Kernel.Extensions.Common;
using ArchitectProg.Kernel.Extensions.Interfaces;
using ArchitectProg.Kernel.Extensions.Specifications;
using Microservice.Permissions.Core.Contracts.Requests.Application;
using Microservice.Permissions.Core.Contracts.Responses.Application;
using Microservice.Permissions.Core.Creators.Interfaces;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Core.Services.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Services
{
    public sealed class ApplicationService : IApplicationService
    {
        private readonly IApplicationCreator applicationCreator;
        private readonly IApplicationMapper applicationMapper;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IRepository<ApplicationEntity> repository;

        public ApplicationService(
            IApplicationCreator applicationCreator,
            IApplicationMapper applicationMapper,
            IUnitOfWorkFactory unitOfWorkFactory,
            IRepository<ApplicationEntity> repository)
        {
            this.applicationCreator = applicationCreator;
            this.applicationMapper = applicationMapper;
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.repository = repository;
        }

        public async Task<Result<int>> Create(CreateApplicationRequest request)
        {
            var application = applicationCreator.Create(request);
            using (var transaction = unitOfWorkFactory.BeginTransaction())
            {
                await repository.Add(application);
                await transaction.Commit();
            }

            var result = application.Id;
            return result;
        }

        public async Task<Result<ApplicationResponse>> Get(int applicationId)
        {
            var application = await repository.GetOrDefault(applicationId);
            if (application is null)
            {
                var failureResult = ResultFactory.ResourceNotFoundFailure<ApplicationResponse>(nameof(application));
                return failureResult;
            }

            var result = applicationMapper.Map(application);
            return result;
        }

        public async Task<Result<IEnumerable<ApplicationResponse>>> GetAll(int? skip, int? take)
        {
            var applications = await repository
                .List(SpecificationFactory.AllSpecification<ApplicationEntity>(), skip, take);

            var result = applicationMapper.MapCollection(applications).ToArray();
            return result;
        }

        public async Task<Result> Update(int applicationId, UpdateApplicationRequest request)
        {
            var application = await repository.GetOrDefault(applicationId);
            if (application is null)
            {
                var failureResult = ResultFactory.ResourceNotFoundFailure(nameof(application));
                return failureResult;
            }

            application.Name = request.Name;
            application.Description = request.Description;
            using (var transaction = unitOfWorkFactory.BeginTransaction())
            {
                await repository.Update(application);
                await transaction.Commit();
            }

            return ResultFactory.Success();
        }

        public async Task<Result> Delete(int applicationId)
        {
            var application = await repository.GetOrDefault(applicationId);
            if (application is null)
            {
                var failureResult = ResultFactory.ResourceNotFoundFailure(nameof(application));
                return failureResult;
            }

            await repository.Delete(application);
            return ResultFactory.Success();
        }

        public Task<int> Count()
        {
            var result = repository.Count(SpecificationFactory.AllSpecification<ApplicationEntity>());
            return result;
        }
    }
}