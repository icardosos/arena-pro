
using System;

namespace ArenaPro.SharedKernel.Events
{
    public interface IEvent
    {
        string Message { get; set; }
        DateTime Date { get;  }

    }
}
