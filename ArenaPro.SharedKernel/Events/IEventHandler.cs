using System.Collections.Generic;
namespace ArenaPro.SharedKernel.Events
{
    public interface IEventHandler<T> where T : IEvent
    {
        void Handle(T args);
        IEnumerable<T> Notify();
        bool HasNotifications();
    }
}