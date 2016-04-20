using Ninject;
using Ninject.Extensions.Wcf;

namespace CC.Service.Webhost.DependencyInjection
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