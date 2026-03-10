using AutoFeed_Backend_DAO.Models;
using AutoFeed_Backend_Repositories.BasicRepo;
using System;

namespace AutoFeed_Backend_Repositories.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    GenericRepository<T> Repository<T>() where T : class;
    AutoFeedDBContext Context { get; }
    int Save();
    Task<int> SaveAsync();
    System.Threading.Tasks.Task BeginTransactionAsync();
    System.Threading.Tasks.Task CommitTransactionAsync();
    System.Threading.Tasks.Task RollbackTransactionAsync();
}
