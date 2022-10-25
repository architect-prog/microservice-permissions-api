using System.Data;
using ArchitectProg.Kernel.Extensions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microservice.Permissions.Persistence.EfCore.Services;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly bool isNestedTransaction;
    private readonly IDbContextTransaction transaction;

    public UnitOfWork(DbContext context)
    {
        var currentTransaction = context.Database.CurrentTransaction;

        isNestedTransaction = currentTransaction is not null;
        transaction = currentTransaction ?? context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
    }

    public async Task Commit()
    {
        if (isNestedTransaction)
            return;

        await transaction.CommitAsync();
    }

    public async Task Rollback()
    {
        if (isNestedTransaction)
            return;

        await transaction.RollbackAsync();
    }

    public void Dispose()
    {
        if (isNestedTransaction)
            return;

        transaction.Dispose();
    }
}