using CodeFrist.EF.Test.Domain.Model;
using CPy.EntityFramework.FluentApi;

namespace CodeFrist.EF.Test.Domain.FluentApi
{
    public class UserFluentConfig:BaseEntityTypeConfiguration<User>
    {
        public UserFluentConfig()
        {
            ToTable("User");
        }
    }
}