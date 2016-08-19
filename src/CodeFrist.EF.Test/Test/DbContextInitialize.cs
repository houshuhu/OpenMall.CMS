using System.Data.Entity;
using System.Data.Entity.Migrations;
using CodeFrist.EF.Test.Domain.Context;
using NUnit.Framework;

namespace CodeFrist.EF.Test.Test
{
    public static class DbContextInitialize
    {
        public static void Initialize()
        {
            //自动迁移
            Database.SetInitializer<CMSDbContext>(new MigrateDatabaseToLatestVersion<CMSDbContext, Migrations.Configuration>());
            var dbMigrator = new DbMigrator(new Migrations.Configuration());
            dbMigrator.Update();
        }
    }
}