using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;

namespace MVCPresentationWithIdentity.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        private void AddBindings()
        {
            kernel.Bind<IVolunteerManager>().To<VolunteerManager>();
            kernel.Bind<ILocationManager>().To<LocationManager>();
            kernel.Bind<ISupplierManager>().To<SupplierManager>();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}