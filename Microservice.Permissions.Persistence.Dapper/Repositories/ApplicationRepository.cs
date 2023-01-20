using ArchitectProg.Kernel.Extensions.Interfaces;
using Dapper;
using Microservice.Permissions.Kernel.Entities;

namespace Microservice.Permissions.Persistence.Dapper.Repositories;

public class ApplicationRepository : IRepository<ApplicationEntity>
{
    private readonly ApplicationDatabaseContext context;

    public ApplicationRepository(ApplicationDatabaseContext context)
    {
        this.context = context;
    }

    public async Task Add(ApplicationEntity entity, CancellationToken token = new())
    {
        var query = "INSERT INTO public.applications(name, description) VALUES (@Name, @Description);";

        var connection = context.Connection;
        var applicationId = await connection.ExecuteAsync(query, entity);
    }

    public async Task AddRange(IEnumerable<ApplicationEntity> entities, CancellationToken token = new())
    {
        var query = "INSERT INTO public.applications(name, description) VALUES (@Name, @Description);";

        var connection = context.Connection;
        var applicationIds = await connection.ExecuteAsync(query, entities);
    }

    public Task Update(ApplicationEntity entity, CancellationToken token = new())
    {
        throw new NotImplementedException();
    }

    public Task UpdateRange(IEnumerable<ApplicationEntity> entities, CancellationToken token = new())
    {
        throw new NotImplementedException();
    }

    public Task Delete(ApplicationEntity entity, CancellationToken token = new())
    {
        throw new NotImplementedException();
    }

    public Task DeleteRange(IEnumerable<ApplicationEntity> entities, CancellationToken token = new())
    {
        throw new NotImplementedException();
    }

    public Task<int> Count(
        ISpecification<ApplicationEntity> specification,
        CancellationToken token = new())
    {
        throw new NotImplementedException();
    }

    public Task<bool> Exists(
        ISpecification<ApplicationEntity> specification,
        CancellationToken token = new())
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationEntity?> GetOrDefault(object id, CancellationToken token = new())
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationEntity?> GetOrDefault(
        ISpecification<ApplicationEntity> specification,
        CancellationToken token = new())
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationEntity[]> List(
        ISpecification<ApplicationEntity> specification,
        int? skip = null,
        int? take = null,
        CancellationToken token = new())
    {
        throw new NotImplementedException();
    }
}