using CodeFrist.EF.Test.Domain.Model;
using CPy.EntityFramework.FluentApi;

namespace CodeFrist.EF.Test.Domain.FluentApi
{
    public class RoleFluentConfig:BaseEntityTypeConfiguration<Role>
    {
        public RoleFluentConfig()
        {
            ToTable("Role");
        }
    }
}