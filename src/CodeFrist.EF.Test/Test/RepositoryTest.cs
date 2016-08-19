using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CodeFrist.EF.Test.Domain.Context;
using CodeFrist.EF.Test.Domain.Model;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using CPy.Domain.BaseRepositories;
using NUnit.Framework;

namespace CodeFrist.EF.Test.Test
{
    public class RepositoryTest:TestBase
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        [Test]
        public void GetList()
        {
            IRepository<Role> roleRepository = (IRepository<Role>)Container.Resolve(typeof(IRepository<Role>));
            var a = roleRepository.GetAllList();
        }

        [Test]
        public async Task<int>  Insert()
        {
            //IRepository<Role> roleRepository = (IRepository<Role>)Container.Resolve(typeof(IRepository<Role>));
            //var role = new Role()
            //{
            //    RName = "004"
            //};
            //var a=roleRepository.InsertAndGetId(role);


            using (var context=new CMSDbContext())
            {
                var list = new List<Role>();
                for (int i = 0; i < 10; i++)
                {
                    list.Add(new Role()
                    {
                        RName = i.ToString()
                    });
                }
                context.Roles.AddRange(list);
                var result=await context.SaveChangesAsync();
                return result;
            }

        }





    }
}