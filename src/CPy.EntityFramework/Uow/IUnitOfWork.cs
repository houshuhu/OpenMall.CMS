using System.Data.Entity;
using System.Threading.Tasks;
using CPy.EntityFramework.Context;

namespace CPy.EntityFramework.Uow
{
    public interface IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        bool IsTransactional { get; set; }
        int Commit();

        Task<int> CommitAsync();
    }
}