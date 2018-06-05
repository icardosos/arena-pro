using System.Collections.Generic;

namespace ArenaPro.SharedKernel.Events
{
    public class DomainNotificationEventHandler : IEventHandler<DomainNotification>
    {
        private IList<DomainNotification> _notifications;

        public DomainNotificationEventHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public void Handle(DomainNotification args)
        {
            _notifications.Add(args);
        }

        public IEnumerable<DomainNotification> Notify()
        {
            return this._notifications;
        }

        public bool HasNotifications()
        {
            return this._notifications.Count > 0;
        }
    }

}