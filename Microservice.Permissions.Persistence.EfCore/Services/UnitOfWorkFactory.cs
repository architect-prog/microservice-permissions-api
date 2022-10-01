using ArchitectProg.Kernel.Extensions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Permissions.Database.Services;

public sealed class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IServiceProvider serviceProvider;

    public UnitOfWorkFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public IUnitOfWork BeginTransaction()
    {
        var dbContext = serviceProvider.GetRequiredService<DbContext>();
        var result = new UnitOfWork(dbContext);
        return result;
    }
}