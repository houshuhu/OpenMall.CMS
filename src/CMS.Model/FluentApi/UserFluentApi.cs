using CMS.Model.Models.Sys;
using CPy.EntityFramework.FluentApi;

namespace CMS.Model.FluentApi
{
    public class UserFluentApi:BaseEntityTypeConfiguration<User>
    {
        public UserFluentApi()
        {
            ToTable("User");
        }
    }
}