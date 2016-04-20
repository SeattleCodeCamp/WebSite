using Ninject.Extensions.Wcf;
using Ninject.Modules;
using Ninject.Web.Common;
using CC.Data;

namespace CC.Service.Webhost.DependencyInjection
{
    public class ConfigurationModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<CCDB>().ToSelf().InRequestScope();
        }
    }
}