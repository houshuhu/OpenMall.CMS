using System.Web;
using System.Web.Mvc;
using CMS.Application;
using CMS.EnityFrameWork;
using CMS.IApplication;
using CMS.Repository;
using CPy.Dependency.WebApi;
using CPy.EntityFramework.Context;
using CPy.EntityFramework.Uow;
using Microsoft.Practices.Unity;
using CPy.Domain.BaseRepositories;
using UnityDependencyResolver = Unity.Mvc5.UnityDependencyResolver;

namespace CMS.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            KernelRegister(container);
            Regeister(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
        public static void KernelRegister(UnityContainer container)
        {
            //每个请求注册一个实例（类似于单例模式）
            container.RegisterType<ICmsDbContext, CmsDbContext>(new PerRequestLifetimeManager());
            container.RegisterType(typeof(IDbContextProvider<>), typeof(SimpleDbContextProvider<>), new PerRequestLifetimeManager());
            container.RegisterType(typeof(IUnitOfWork<>), typeof(UnitOfWork<>), new PerRequestLifetimeManager());


            container.RegisterType(typeof(IUnitofWorkContext<>), typeof(UnitOfWorkContext<>));
            container.RegisterType(typeof(IRepository<>), typeof(BaseRepository<>));
        }

        public static void Regeister(UnityContainer container)
        {
            container.RegisterType<IUserApplication, UserApplication>();
        }
    }
}