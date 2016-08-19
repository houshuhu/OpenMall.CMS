using System;
using CMS.EnityFrameWork;
using CPy.Domain.Entities;
using CPy.EntityFramework.Context;
using CPy.EntityFramework.Repositories;
using CPy.EntityFramework.Uow;

namespace CMS.Repository
{
    public class BaseRepository<TEntity> : EfRepositoryBaseOfTEntity<CmsDbContext, TEntity>
        where TEntity : class, IEntity<Guid>
    {
        public BaseRepository(IDbContextProvider<CmsDbContext> provider, IUnitOfWork<CmsDbContext> unitOfWork, IUnitofWorkContext<CmsDbContext> unitofWorkContext) : base(provider, unitOfWork, unitofWorkContext)
        {
        }
    }
}