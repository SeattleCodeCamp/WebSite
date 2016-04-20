using Ninject;

namespace CC.Service.Webhost.DependencyInjection
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