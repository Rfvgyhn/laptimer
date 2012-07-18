using System.Collections.Generic;
using System;

namespace LapTimer.Data
{
    public interface IRepository
    {
        TEntity Single<TEntity>(object key) where TEntity : class, new();
        TEntity Single<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, new();
        IEnumerable<TEntity> All<TEntity>() where TEntity : class, new();
        IEnumerable<TEntity> Find<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, new();
        void Save<TEntity>(TEntity item) where TEntity : class, new();
        void Delete<TEntity>(object key) where TEntity : class, new();
    }
}
