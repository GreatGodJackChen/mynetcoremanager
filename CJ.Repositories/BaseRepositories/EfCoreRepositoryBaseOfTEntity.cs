﻿using CJ.Data.BaseEntity;
using CJ.Domain.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CJ.Repositories.BaseRepositories
{
    public class EfCoreRepositoryBase<TDbContext, TEntity> : EfCoreRepositoryBase<TDbContext, TEntity, string>, IRepository<TEntity>
       where TEntity : class, IEntity<string>
       where TDbContext : DbContext
    {
        public EfCoreRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
