using System;
using System.Data.Entity;

namespace CPy.EntityFramework.Context
{
    public class SimpleDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : DbContext, new()
    {
        /// <summary>
        /// DbContextProvider的Key
        /// </summary>
        private readonly Guid _id =Guid.NewGuid();
        public TDbContext DbContext { get; private set; }

        public SimpleDbContextProvider(TDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}