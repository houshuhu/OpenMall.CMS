using System.Data.Entity;
using CodeFrist.EF.Test.Domain.Model;

namespace CodeFrist.EF.Test.Domain.Context
{
    public interface ICMSDbContext
    {
        DbSet<Role> Roles { get; set; }
    }
}