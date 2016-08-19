using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using CPy.Domain.Entities;
using CPy.EntityFramework.Context;
using CPy.EntityFramework.Uow;
using CPy.Domain.BaseRepositories;

namespace CPy.EntityFramework.Repositories
{
    public class EfRepositoryBaseOfTEntity<TDbContext, TEntity> : EfRepositoryBaseOfTEntityAndTPrimaryKey<TDbContext, TEntity, Guid>, IRepository<TEntity>
        where TEntity : class, IEntity<Guid>
        where TDbContext : DbContext
    {
        public EfRepositoryBaseOfTEntity(IDbContextProvider<TDbContext> provider, IUnitOfWork<TDbContext> unitOfWork, IUnitofWorkContext<TDbContext> unitofWorkContext) : base(provider, unitOfWork, unitofWorkContext)
        {
        }
    }
}