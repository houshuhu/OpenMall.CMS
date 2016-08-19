using System.Data.Entity;
using CMS.Model.Models.Sys;

namespace CMS.EnityFrameWork
{
    public interface ICmsDbContext
    {
        DbSet<User> Users { get; set; }
    }
}