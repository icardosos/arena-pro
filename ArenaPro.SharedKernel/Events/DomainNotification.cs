using System;

namespace ArenaPro.SharedKernel.Events
{
    public class DomainNotification : IEvent
    {
        public string Message { get; set; }
        public DateTime Date { get; private set; }

        public DomainNotification(string msg)
        {
            this.Message = msg;
            this.Date = DateTime.Now;
        }        
    }
}
