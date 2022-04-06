﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;

using LogicLayer;
using LogicLayerInterfaces;
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

            kernel.Bind<IEventManager>().To<LogicLayer.EventManager>().InRequestScope();
            kernel.Bind<IUserManager>().To<LogicLayer.UserManager>().InRequestScope();            
            
            //kernel.Bind<IEventManager>().To<LogicLayer.EventManager>().WithConstructorArgument("fake", new EventAccessorFake());


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