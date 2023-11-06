using Microsoft.Practices.Unity;
using StatsSA.SystemArchitecture.Resolver;
using System.Web.Http;
using Unity.WebApi;

namespace StatsSA.eRecruitment.WebApi.Unity
{
    public class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            // register dependency resolver for WebAPI RC
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);           
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            ComponentLoader.LoadContainer(container, ".\\bin", "StatsSA.eRecruitment*.dll");
           // ComponentLoader.LoadContainer(container, ".\\bin", "StatsSA.SystemArchitecture.Provider.dll");
           // ComponentLoader.LoadContainer(container, ".\\bin", "StatsSA.SystemArchitecture.Manager.dll");
        }
    }
}