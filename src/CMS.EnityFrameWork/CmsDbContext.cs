using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using CMS.Model.Models.Sys;
using CPy.EntityFramework.Context;
using CPy.EntityFramework.FluentApi;

namespace CMS.EnityFrameWork
{
    public class CmsDbContext : EfBaseDbContext, ICmsDbContext
    {
        /// <summary>
        /// DbContext的Key：每次新创建一个DbContext,则新生成一个Key
        /// </summary>
        public Guid Id { get; private set; }

        public CmsDbContext()
            : base("openmall")
        {
            Id=Guid.NewGuid();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var types = Assembly.GetAssembly(typeof(User)).GetTypes();

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

        public DbSet<User> Users { get; set; }
        public DbSet<People> Peoples { get; set; }
    }
}