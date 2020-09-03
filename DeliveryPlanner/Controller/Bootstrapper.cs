using Autofac;
using DeliveryPlanner.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryPlanner.Controller
{
    class Bootstrapper
    {
        public static ILifetimeScope CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();          
            containerBuilder.RegisterType<ViewService>().As<IViewService>();
            return containerBuilder.Build();
        }
    }
}
