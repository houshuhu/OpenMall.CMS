using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using CodeFrist.EF.Test.Domain.Model;
using CPy.EntityFramework.Context;
using CPy.EntityFramework.FluentApi;

namespace CodeFrist.EF.Test.Domain.Context
{
    public class CMSDbContext : EfBaseDbContext, ICMSDbContext
    {
        public CMSDbContext()
            : base("openmall")
        {
            this.Database.Initialize(false);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();

            var registerTypes = types.Where(
                    t => t.IsClass && t.BaseType != null && t.BaseType.IsGenericType &&
                        t != typeof(BaseEntityTypeConfiguration<>) &&
                        t.BaseType.GetGenericTypeDefinition() == typeof(BaseEntityTypeConfiguration<>)).ToList();
            foreach (var registerType in registerTypes)
            {
                dynamic configurationInstance = Activator.CreateInstance(registerType);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Role> Roles { get; set; }
    }
}