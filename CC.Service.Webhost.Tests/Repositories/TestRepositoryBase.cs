using Ninject;
using CC.Data;
using CC.Service.Webhost.DependencyInjection;
using CC.Service.Webhost.CodeCampSvc;
using CC.Service.Webhost.Tests.Helpers;

namespace CC.Service.Webhost.Tests.Repositories
{
    public class TestRepositoryBase
    {
        protected static InMemoryOCCDB dbContext = new InMemoryOCCDB();

        protected CodeCampService GetTestService()
        {
            var kernel = new StandardKernel();
            Bootstrapper.Configure(kernel);
            kernel.Rebind<CCDB>().ToConstant(dbContext);
            return kernel.Get<CodeCampService>();
        }   
    }
}
