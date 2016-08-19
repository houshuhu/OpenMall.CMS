using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using CPy.EntityFramework.FluentApi;

namespace CPy.EntityFramework.Context
{
    public class EfBaseDbContext:DbContext
    {
        /// <summary>
        /// 数据库名称或者链接字符串
        /// nameOrConnectionString
        /// </summary>
        /// <param name="constring"></param>
        public EfBaseDbContext(string constring="default"):base(constring)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var types = Assembly.GetAssembly(typeof(BaseEntityTypeConfiguration<>)).GetTypes();
            //var types = Assembly.GetExecutingAssembly().GetTypes();

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
    }
}