using Microsoft.Practices.Unity;
using ArenaPro.SharedKernel.Events;

namespace ArenaPro.Infrastructure.UnityIoC
{
    public static class DependencyRegister
    {
        /// <summary>
        /// TransientLifetimeManager - Cada Resolve gera uma nova instância.
        /// ContainerControlledLifetimeManager - Utiliza Singleton
        /// </summary>
        /// <param name="container"></param>
        public static void Register(UnityContainer container)
        {
            container.RegisterType<IEventHandler<DomainNotification>, DomainNotificationEventHandler>(new HierarchicalLifetimeManager());            
        }
    }
}
