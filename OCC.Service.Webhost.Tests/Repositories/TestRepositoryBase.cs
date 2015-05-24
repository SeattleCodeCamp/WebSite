using Ninject;
using OCC.Data;
using OCC.Service.Webhost.DependencyInjection;
using OCC.Service.Webhost.Services;
using OCC.Service.Webhost.Tests.Helpers;

namespace OCC.Service.Webhost.Tests.Repositories
{
    public class TestRepositoryBase
    {
        protected static InMemoryOCCDB dbContext = new InMemoryOCCDB();

        protected CodeCampService GetTestService()
        {
            var kernel = new StandardKernel();
            Bootstrapper.Configure(kernel);
            kernel.Rebind<OCCDB>().ToConstant(dbContext);
            return kernel.Get<CodeCampService>();
        }   
    }
}
