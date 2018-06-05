using ArenaPro.Infrastructure.Identity;
using ArenaPro.Infrastructure.Identity.Context;

using Microsoft.Practices.Unity;

namespace ArenaPro.Infrastructure.IoC
{
    public class Bootstrapper
    {
        public static void RegisterServices(UnityContainer container)
        {

            //container.RegisterInstance<ApplicationDbContext>(new ApplicationDbContext(), new HierarchicalLifetimeManager());
            //container.RegisterInstance<IUserStore<ApplicationUser>>(new UserStore<ApplicationUser>(), new PerRequestLifetimeManager());
            //container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            //container.Register<ApplicationSignInManager>(Lifestyle.Scoped);
            //container.Register<IUsuarioRepository, UsuarioRepository>(Lifestyle.Scoped);
        }
    }
}
