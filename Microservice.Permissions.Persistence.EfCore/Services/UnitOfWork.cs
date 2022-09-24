using System.Data;
using ArchitectProg.Kernel.Extensions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microservice.Permissions.Database.Services;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly IDbContextTransaction transaction;

    public UnitOfWork(DbContext applicationDatabaseContext)
    {
        var currentTransaction = applicationDatabaseContext.Database.CurrentTransaction;
        transaction = currentTransaction
                      ?? applicationDatabaseContext.Database.BeginTransaction(IsolationLevel.ReadCommitted);
    }

    public async Task Commit()
    {
        await transaction.CommitAsync();
    }

    public async Task Rollback()
    {
        await transaction.RollbackAsync();
    }

    public void Dispose()
    {
        transaction.Dispose();
    }
}