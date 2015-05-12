using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using Ninject.Extensions.Wcf;
using Ninject.Modules;
using Ninject.Web.Common;
using OCC.Data;

namespace OCC.Service.Webhost.DependencyInjection
{
    public class ConfigurationModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<ServiceHost>().To<NinjectServiceHost>();
            Kernel.Bind<OCCDB>().ToSelf().InRequestScope();
        }
    }
}