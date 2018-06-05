
namespace ArenaPro.SharedKernel.Events
{
    public static class EventTrigger
    {
        public static IContainerResolver ContainerResolver { get; set; }

        public static void Raise<T>(T args) where T : IEvent
        {
            if (ContainerResolver != null)
            {
                var obj = ContainerResolver.GetService(typeof(IEventHandler<T>));
                ((IEventHandler<T>)obj).Handle(args);
            }
        }

        public static void RaiseDomainNotification(string msg)
        {
            Raise(new DomainNotification(msg));
        }
    }
}
