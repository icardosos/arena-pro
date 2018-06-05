using System;
using System.Collections.Generic;

namespace ArenaPro.SharedKernel
{
    public interface IContainerResolver
    {
        object GetService(Type serviceType);
        IEnumerable<object> GetServices(Type serviceType);
        T GetService<T>();
    }
}