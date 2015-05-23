using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;

namespace OCC.Service.Webhost.DependencyInjection
{
    public static class Bootstrapper
    {
        public static void Configure(IKernel kernel)
        {
            kernel.Load(new []
            {
                new ConfigurationModule(), 
            });
        }
    }
}