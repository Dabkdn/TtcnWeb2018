using System.Web.Mvc;

using Ttcn_web.Services.Abtractions;
using Ttcn_web.Services.Implementations;

using Unity;
using Unity.Mvc5;

namespace Ttcn_web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IFurnitureTypeService, FurnitureTypeService>();
            container.RegisterType<ILoginService, LoginService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}