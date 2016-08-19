using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CPy.Domain.BaseRepositories;
using CPy.Domain.Entities;
using CPy.EntityFramework.Context;
using CPy.EntityFramework.Uow;

namespace CPy.EntityFramework.Repositories
{
    public class EfRepositoryBaseOfTEntityAndTPrimaryKey<TDbContext, TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TDbContext : DbContext
    {
        private readonly IDbContextProvider<TDbContext> _provider;
        private readonly IUnitOfWork<TDbContext> _unitOfWork;
        private readonly IUnitofWorkContext<TDbContext> _unitofWorkContext;

        public EfRepositoryBaseOfTEntityAndTPrimaryKey(IDbContextProvider<TDbContext> provider, IUnitOfWork<TDbContext> unitOfWork, IUnitofWorkContext<TDbContext> unitofWorkContext)
        {
            _provider = provider;
            _unitOfWork = unitOfWork;
            _unitofWorkContext = unitofWorkContext;
        }

        #region GetDbContext And Table ( 获取Dbcontext,和TEntity对应的DbSet )

        /// <summary>
        /// Gets EF DbContext object.
        /// </summary>
        protected virtual TDbContext Context { get { return _provider.DbContext; } }

        /// <summary>
        /// Gets DbSet for given entity.
        /// </summary>
        protected virtual DbSet<TEntity> Table { get { return Context.Set<TEntity>(); } }

        #endregion

        

        #region   Base (基本操作，单个实体的增删改 )
        public override IQueryable<TEntity> GetAll()
        {
            return Table;
        }

        public override TEntity Insert(TEntity entity)
        {
            _unitofWorkContext.RegisterNew(entity);
            Commit();
            return entity;
        }

        public override TEntity Update(TEntity entity)
        {
            _unitofWorkContext.RegisterModified(entity);
            Commit();
            return entity;
        }

        public override void Delete(TEntity entity)
        {
            _unitofWorkContext.RegisterDeleted(entity);
            Commit();
        }

        public override void Delete(TPrimaryKey id)
        {
            var entity = Table.FirstOrDefault(t => t.Id.Equals(id));
            if (entity == null)
            {
                throw new Exception(string.Format("{0}实体找不到该记录", typeof(TEntity).Name));
            }
            Delete(entity);
        }
        #endregion


        #region Extensions ( 拓展操作：多个实体的批量增删改 )
        public override int Insert(IEnumerable<TEntity> entities)
        {
            _unitofWorkContext.RegisterNew(entities);
            return Commit();
        }

        public async override Task<int> InsertAsync(IEnumerable<TEntity> entities)
        {
            _unitofWorkContext.RegisterNew(entities);
            return await CommitAsync();
        }

        public override int Update(IEnumerable<TEntity> entities)
        {
            _unitofWorkContext.RegisterModified(entities);
            return Commit();
        }

        public override Task<int> UpdateAsny(IEnumerable<TEntity> entities)
        {
            _unitofWorkContext.RegisterModified(entities);
            return CommitAsync();
        }

        public override int Delete(IEnumerable<TEntity> entities)
        {
            _unitofWorkContext.RegisterDeleted(entities);
            return Commit();
        }

        public override Task<int> DeleteAsny(IEnumerable<TEntity> entities)
        {
            _unitofWorkContext.RegisterDeleted(entities);
            return CommitAsync();
        }

        #endregion


        #region Commit （提交：同步提交，异步提交）
        protected int Commit()
        {
            return _unitOfWork.IsTransactional ? 0 : _unitOfWork.Commit();
        }

        protected Task<int> CommitAsync()
        {
            return _unitOfWork.IsTransactional ? Task.FromResult(0) : _unitOfWork.CommitAsync();
        }

        #endregion

    }
}