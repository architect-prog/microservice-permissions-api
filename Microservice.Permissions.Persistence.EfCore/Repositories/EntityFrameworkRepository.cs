using ArchitectProg.Kernel.Extensions;
using ArchitectProg.Kernel.Extensions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Permissions.Database.Repositories;

public sealed class EntityFrameworkRepository<T> : IRepository<T> where T : class
{
    private readonly DbContext context;
    private readonly DbSet<T> entitiesSet;

    public EntityFrameworkRepository(DbContext context)
    {
        this.context = context;
        entitiesSet = context.Set<T>();
    }

    public async Task Add(T entity)
    {
        await entitiesSet.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task AddRange(IEnumerable<T> entities)
    {
        await entitiesSet.AddRangeAsync(entities);
        await context.SaveChangesAsync();
    }

    public Task Update(T entity)
    {
        entitiesSet.Update(entity);
        return context.SaveChangesAsync();
    }

    public Task UpdateRange(IEnumerable<T> entities)
    {
        entitiesSet.UpdateRange(entities);
        return context.SaveChangesAsync();
    }

    public Task Delete(T entity)
    {
        entitiesSet.Remove(entity);
        return context.SaveChangesAsync();
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