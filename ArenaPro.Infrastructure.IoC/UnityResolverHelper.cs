using ArenaPro.SharedKernel;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;

namespace ArenaPro.Infrastructure.UnityIoC
{

    public class UnityResolverHelper : IContainerResolver
    {
        protected IUnityContainer container;

        public UnityResolverHelper(IUnityContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public T GetService<T>()
        {
            try
            {
                return (T)container.Resolve(typeof(T));
            }
            catch (ResolutionFailedException)
            {
                return default(T);
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

    }
}