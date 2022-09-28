using ArchitectProg.Kernel.Extensions;
using ArchitectProg.Kernel.Extensions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Permissions.Database.Repositories
{
    public sealed class EntityFrameworkRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext context;
        private readonly DbSet<T> entitiesSet;

        public EntityFrameworkRepository(DbContext context)
        {
            this.context = context;
            entitiesSet = context.Set<T>();
        }

        public async Task Add(T entity, CancellationToken token = default)
        {
            await entitiesSet.AddAsync(entity, token);
            await context.SaveChangesAsync(token);
        }

        public async Task AddRange(IEnumerable<T> entities, CancellationToken token = default)
        {
            await entitiesSet.AddRangeAsync(entities, token);
            await context.SaveChangesAsync(token);
        }

        public Task Update(T entity, CancellationToken token = default)
        {
            entitiesSet.Update(entity);
            return context.SaveChangesAsync(token);
        }

        public Task UpdateRange(IEnumerable<T> entities, CancellationToken token = default)
        {
            entitiesSet.UpdateRange(entities);
            return context.SaveChangesAsync(token);
        }

        public Task Delete(T entity, CancellationToken token = default)
        {
            entitiesSet.Remove(entity);
            return context.SaveChangesAsync(token);
        }

        public Task<int> Count(ISpecification<T> specification, CancellationToken token = default)
        {
            var entities = specification.AddPredicates(entitiesSet);
            return entities.CountAsync(token);
        }

        public Task<bool> Exists(ISpecification<T> specification, CancellationToken token = default)
        {
            var entities = specification.AddPredicates(entitiesSet);
            return entities.AnyAsync(token);
        }

        public async Task<T?> GetOrDefault(object id, CancellationToken token = default)
        {
            var result = await entitiesSet.FindAsync(new[]{id}, token);
            return result;
        }

        public Task<T?> GetOrDefault(ISpecification<T> specification, CancellationToken token = default)
        {
            var result = entitiesSet.ApplySpecification(specification);
            return result.FirstOrDefaultAsync(token);
        }

        public async Task<T[]> List(
            ISpecification<T> specification,
            int? skip = null,
            int? take = null,
            CancellationToken token = default)
        {
            var entities = entitiesSet.ApplySpecification(specification);

            if (skip.HasValue)
                entities = entities.Skip(skip.Value);

            if (take.HasValue)
                entities = entities.Take(take.Value);

            var result = await entities.ToArrayAsync(token);
            return result;
        }
    }
}