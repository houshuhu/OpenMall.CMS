using System;
using CodeFrist.EF.Test.Domain.Context;
using CPy.Domain.Entities;
using CPy.EntityFramework.Context;
using CPy.EntityFramework.Repositories;
using CPy.EntityFramework.Uow;

namespace CodeFrist.EF.Test.Domain.Repository
{
    public class BaseRepository<TEntity> : EfRepositoryBaseOfTEntity<CMSDbContext, TEntity>
        where TEntity : class, IEntity<Guid>
    {
        public BaseRepository(IDbContextProvider<CMSDbContext> provider, IUnitOfWork<CMSDbContext> unitOfWork, IUnitofWorkContext<CMSDbContext> unitofWorkContext) : base(provider, unitOfWork, unitofWorkContext)
        {
        }
    }
}