using System.ServiceModel;
using Ninject;
using Ninject.Extensions.Wcf;
using Ninject.Web.Common;
using OCC.Data;

namespace OCC.Service.Webhost.DependencyInjection
{
    public class NinjectCustomServiceHostFactory : NinjectServiceHostFactory
    {
        public NinjectCustomServiceHostFactory()
        {
            var kernel = new StandardKernel();
            Bootstrapper.Configure(kernel);
            SetKernel(kernel);
        }
    }
}