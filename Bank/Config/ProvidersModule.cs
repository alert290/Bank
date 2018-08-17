using Bank.Domain.Providers;
using Bank.Providers;
using Ninject.Modules;

namespace Bank.Config
{
    public class ProvidersModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IResourcesProvider>().To<ResourcesProvider>().InSingletonScope();
        }
    }
}