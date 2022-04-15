﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;

using DataAccessFakes;
using Ninject.Web.Common;

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
            // live
            kernel.Bind<IEventManager>().To<LogicLayer.EventManager>().InRequestScope();
            kernel.Bind<IUserManager>().To<LogicLayer.UserManager>().InRequestScope();
            kernel.Bind<IVolunteerManager>().To<VolunteerManager>();
            kernel.Bind<ILocationManager>().To<LocationManager>();
            kernel.Bind<ISupplierManager>().To<SupplierManager>();
            kernel.Bind<ISublocationManager>().To<SublocationManager>();
            kernel.Bind<IParkingLotManager>().To<ParkingLotManager>();


            // fake
            //kernel.Bind<IEventManager>().To<LogicLayer.EventManager>().WithConstructorArgument("eventAccessor", new EventAccessorFake());





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