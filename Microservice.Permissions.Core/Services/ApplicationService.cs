using ArchitectProg.Kernel.Extensions.Factories.Interfaces;
using ArchitectProg.Kernel.Extensions.Interfaces;
using ArchitectProg.Kernel.Extensions.Utils;
using Microservice.Permissions.Core.Contracts.Requests.Application;
using Microservice.Permissions.Core.Contracts.Responses.Application;
using Microservice.Permissions.Core.Creators.Interfaces;
using Microservice.Permissions.Core.Mappers.Interfaces;
using Microservice.Permissions.Core.Services.Interfaces;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Core.Services;

public sealed class ApplicationService : IApplicationService
{
    private readonly IResultFactory resultFactory;
    private readonly ISpecificationFactory specificationFactory;
    private readonly IApplicationCreator applicationCreator;
    private readonly IApplicationMapper applicationMapper;
    private readonly IUnitOfWorkFactory unitOfWorkFactory;
    private readonly IRepository<ApplicationEntity> repository;

    public ApplicationService(
        IResultFactory resultFactory,
        ISpecificationFactory specificationFactory,
        IApplicationCreator applicationCreator,
        IApplicationMapper applicationMapper,
        IUnitOfWorkFactory unitOfWorkFactory,
        IRepository<ApplicationEntity> repository)
    {
        this.resultFactory = resultFactory;
        this.specificationFactory = specificationFactory;
        this.applicationCreator = applicationCreator;
        this.applicationMapper = applicationMapper;
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.repository = repository;
    }

    public async Task<Result<ApplicationResponse>> Create(CreateApplicationRequest request)
    {
        var application = applicationCreator.Create(request);
        using (var transaction = unitOfWorkFactory.BeginTransaction())
        {
            await repository.Add(application);
            await transaction.Commit();
        }

        var result = applicationMapper.Map(application);
        return result;
    }

    public async Task<Result<ApplicationResponse>> Get(int applicationId)
    {
        var application = await repository.GetOrDefault(applicationId);
        if (application is null)
        {
            var failureResult = resultFactory.ResourceNotFoundFailure<ApplicationResponse>(nameof(application));
            return failureResult;
        }

        var result = applicationMapper.Map(application);
        return result;
    }

    public async Task<Result<IEnumerable<ApplicationResponse>>> GetAll(int? skip, int? take)
    {
        var applications = await repository
            .List(specificationFactory.AllSpecification<ApplicationEntity>(), skip, take);

        var result = applicationMapper.MapCollection(applications).ToArray();
        return result;
    }

    public async Task<Result> Update(int applicationId, UpdateApplicationRequest request)
    {
        var application = await repository.GetOrDefault(applicationId);
        if (application is null)
        {
            var failureResult = resultFactory.ResourceNotFoundFailure(nameof(application));
            return failureResult;
        }

        application.Name = request.Name;
        application.Description = request.Description;
        using (var transaction = unitOfWorkFactory.BeginTransaction())
        {
            await repository.Update(application);
            await transaction.Commit();
        }

        return resultFactory.Success();
    }

    public async Task<Result> Delete(int applicationId)
    {
        var application = await repository.GetOrDefault(applicationId);
        if (application is null)
        {
            var failureResult = resultFactory.ResourceNotFoundFailure(nameof(application));
            return failureResult;
        }

        using (var transaction = unitOfWorkFactory.BeginTransaction())
        {
            await repository.Delete(application);
            await transaction.Commit();
        }

        return resultFactory.Success();
    }

    public Task<int> Count()
    {
        var result = repository.Count(specificationFactory.AllSpecification<ApplicationEntity>());
        return result;
    }
}