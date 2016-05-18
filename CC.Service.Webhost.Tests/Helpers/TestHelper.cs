using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Extensions.Factory;
using CC.Data;
using CC.Service.Webhost.DependencyInjection;
using CC.Service.Webhost.CodeCampSvc;

namespace CC.Service.Webhost.Tests.Helpers
{
    public static class TestHelper
    {
        public static CodeCampService GetTestService(CCDB dbContext, Action<IKernel> bindingDelegate = null)
        {
            var kernel = new StandardKernel();
            Bootstrapper.Configure(kernel);
            kernel.Rebind<CCDB>().ToConstant(dbContext);
            if (bindingDelegate != null)
            {
                bindingDelegate.Invoke(kernel);
            }
            //return kernel.Get<CodeCampService>();
            return new CodeCampService(kernel);
        }
    }
}
