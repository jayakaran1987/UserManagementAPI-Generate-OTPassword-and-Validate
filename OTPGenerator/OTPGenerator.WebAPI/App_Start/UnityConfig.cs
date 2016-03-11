using Microsoft.Practices.Unity;
using OTPGenerator.Service.Service;
using System.Web.Http;
using Unity.WebApi;

namespace OTPGenerator.WebAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUserManagementService, UserManagementService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}