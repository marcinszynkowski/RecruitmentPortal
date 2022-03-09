using Autofac;
using Autofac.Integration.Mvc;
using RP.Data.Context;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ConfigureContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<RPDbContext>().AsSelf().InstancePerRequest();           
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);
            builder.RegisterModule<AccountModule.AccountModule>();
            builder.RegisterModule<AdminModule.AdminModule>();
            builder.RegisterModule<RecruitmentModule.RecruitmentModule>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}