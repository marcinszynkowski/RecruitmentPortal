using Autofac;
using RP.AccountModule.Interfaces;
using RP.AccountModule.Services;

namespace RP.AccountModule
{
    public class AccountModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerRequest();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
        }
    }
}