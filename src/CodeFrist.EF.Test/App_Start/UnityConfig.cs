using System.Web.Mvc;
using CodeFrist.EF.Test.Domain.Context;
using CodeFrist.EF.Test.Domain.Repository;
using CPy.EntityFramework.Context;
using CPy.EntityFramework.Uow;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using CPy.Domain.BaseRepositories;

namespace CodeFrist.EF.Test
{
    public static class UnityConfig
    {
        private static UnityContainer _container;
        public static UnityContainer GetUnityContainer()
        {
            _container=new UnityContainer();
            RegisterComponents();
            return _container;
        }
        
        public static void RegisterComponents()
        {
            //GetUnityContainer();
            KernelRegister(_container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(_container));
        }

        public static void KernelRegister(UnityContainer container)
        {
            container.RegisterType<ICMSDbContext, CMSDbContext>();
            container.RegisterType(typeof (IDbContextProvider<>), typeof (SimpleDbContextProvider<>));
            container.RegisterType(typeof (IUnitOfWork<>), typeof (UnitOfWork<>));
            container.RegisterType(typeof (IUnitofWorkContext<>), typeof (UnitOfWorkContext<>));
            container.RegisterType(typeof (IRepository<>), typeof (BaseRepository<>));
        }
    }
}