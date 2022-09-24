using ArchitectProg.Kernel.Extensions;
using ArchitectProg.Kernel.Extensions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Permissions.Database.Repositories;

public sealed class EntityFrameworkRepository<T> : IRepository<T> where T : class
{
    private readonly DbSet<T> entitiesSet;

    public EntityFrameworkRepository(DbContext dbContext)
    {
        entitiesSet = dbContext.Set<T>();
    }

    public async Task Add(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        await entitiesSet.AddAsync(entity);
    }

    public async Task AddRange(IEnumerable<T> entities)
    {
        if (entities is null)
            throw new ArgumentNullException(nameof(entities));

        await entitiesSet.AddRangeAsync(entities);
    }

    public Task Update(T entity)
    {
        entitiesSet.Update(entity);
        return Task.CompletedTask;
    }

    public Task UpdateRange(IEnumerable<T> entities)
    {
        entitiesSet.UpdateRange(entities);
        return Task.CompletedTask;
    }

    public Task Delete(T entity)
    {
        entitiesSet.Remove(entity);
        return Task.CompletedTask;
    }

    public Task<int> Count(ISpecification<T> specification)
    {
        var entities = specification.AddPredicates(entitiesSet);
        return entities.CountAsync();
    }

    public Task<bool> Exists(ISpecification<T> specification)
    {
        var entities = specification.AddPredicates(entitiesSet);
        return entities.AnyAsync();
    }

    public async Task<T?> GetOrDefault(object id)
    {
        var result = await entitiesSet.FindAsync(id);
        return result;
    }

    public Task<T?> GetOrDefault(ISpecification<T> specification)
    {
        var result = entitiesSet.ApplySpecification(specification);
        return result.FirstOrDefaultAsync();
    }

    public async Task<T[]> List(ISpecification<T> specification, int? skip = null, int? take = null)
    {
        var entities = entitiesSet.ApplySpecification(specification);

        if (skip.HasValue)
            entities = entities.Skip(skip.Value);

        if (take.HasValue)
            entities = entities.Take(take.Value);

        var result = await entities.ToArrayAsync();
        return result;
    }
}