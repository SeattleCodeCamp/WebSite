using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using Ninject;
using Ninject.Extensions.Wcf;

namespace OCC.Service.Webhost
{
    public class NinjectCustomServiceHostFactory : NinjectServiceHostFactory
    {
        public NinjectCustomServiceHostFactory()
        {
            var kernel = new StandardKernel();
            kernel.Bind<ServiceHost>().To<NinjectServiceHost>();
            SetKernel(kernel);
        }
    }
}