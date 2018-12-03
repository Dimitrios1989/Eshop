using Eshop.ClassLibrary.Models.Repository;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace Eshop.Administration
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<Controllers.AccountController>(new InjectionConstructor());
            container.RegisterType<Controllers.ManageController>(new InjectionConstructor());
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}