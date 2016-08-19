using System.Data.Entity;
using System.Data.Entity.Migrations;
using CMS.EnityFrameWork.Migrations;

namespace CMS.EnityFrameWork
{
    public static class DbContextInitialize
    {
        public static void Initialize()
        {
            Database.SetInitializer<CmsDbContext>(new MigrateDatabaseToLatestVersion<CmsDbContext,Configuration>());
            var dbMigrator = new DbMigrator(new Configuration());
            dbMigrator.Update();
        } 
    }
}