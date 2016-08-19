using System;
using System.Data.Entity;
using System.Threading.Tasks;
using CPy.EntityFramework.Context;

namespace CPy.EntityFramework.Uow
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        public Guid Id = Guid.NewGuid();
        private readonly IDbContextProvider<TDbContext> _provider;
        private bool _disposed = false;

        public UnitOfWork(IDbContextProvider<TDbContext> provider)
        {
            _provider = provider;
        }


        public bool IsTransactional { get; set; }

        public int Commit()
        {
            if (_disposed)
            {
                throw new Exception(string.Format("{0}已释放", this.GetType().FullName));
            }
            try
            {
                return _provider.DbContext.SaveChanges();

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                //释放DbContext
                //Dispose();
            }
        }

        public async Task<int> CommitAsync()
        {
            if (_disposed)
            {
                throw new Exception(string.Format("{0}已释放", this.GetType().FullName));
            }
            try
            {
                return await _provider.DbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                //释放DbContext
                //Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing && _provider.DbContext != null)
            {
                _provider.DbContext.Dispose();
            }

            _disposed = true;
        }
    }
}