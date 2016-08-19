using System.Collections.Generic;
using System.Data.Entity;
using CPy.EntityFramework.Context;

namespace CPy.EntityFramework.Uow
{
    public class UnitOfWorkContext<TDbContext> : IUnitofWorkContext<TDbContext> where TDbContext : DbContext
    {
        private readonly IDbContextProvider<TDbContext> _provider;
        protected virtual TDbContext Dbcontext
        {
            get { return _provider.DbContext; }
        }

        public UnitOfWorkContext(IDbContextProvider<TDbContext> provider)
        {
            _provider = provider;
        }

        /// <summary>
        ///   为指定的类型返回 System.Data.Entity.DbSet，这将允许对上下文中的给定实体执行 CRUD 操作。
        /// </summary>
        /// <typeparam name="TEntity"> 应为其返回一个集的实体类型。 </typeparam>
        /// <returns> 给定实体类型的 System.Data.Entity.DbSet 实例。 </returns>
        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return Dbcontext.Set<TEntity>();
        }

        /// <summary>
        ///     注册一个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterNew<TEntity>(TEntity entity) where TEntity : class
        {
            EntityState state = Dbcontext.Entry(entity).State;
            if (state == EntityState.Detached)
            {
                Dbcontext.Entry(entity).State = EntityState.Added;
            }
        }

        /// <summary>
        ///     批量注册多个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        public void RegisterNew<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            try
            {
                Dbcontext.Configuration.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                {
                    RegisterNew(entity);
                }
            }
            finally
            {
                Dbcontext.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        /// <summary>
        ///     注册一个更改的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterModified<TEntity>(TEntity entity) where TEntity : class
        {
            if (Dbcontext.Entry(entity).State == EntityState.Detached)
            {
                Dbcontext.Set<TEntity>().Attach(entity);
            }
            Dbcontext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        ///   批量更改对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        public void RegisterModified<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            try
            {
                Dbcontext.Configuration.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                {
                    RegisterModified(entity);
                }
            }
            finally
            {
                Dbcontext.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        /// <summary>
        ///   注册一个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            Dbcontext.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        ///   批量注册多个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        public void RegisterDeleted<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            try
            {
                Dbcontext.Configuration.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                {
                    RegisterDeleted(entity);
                }
            }
            finally
            {
                Dbcontext.Configuration.AutoDetectChangesEnabled = true;
            }
        }

    }
}