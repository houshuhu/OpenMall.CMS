using System.Data.Entity;

namespace CPy.EntityFramework.Context
{
    public interface IDbContextProvider<out TDbContext>
        where TDbContext : DbContext
    {
        TDbContext DbContext { get; }
    }
}