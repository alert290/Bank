using Bank.Domain.Handlers.Abstract;
using Bank.Domain.Handlers.Concrete;
using Ninject.Modules;

namespace Bank.Config
{
    public class HandlersModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepositoriesHandler>().To<RepositoriesHandler>();
            Bind<IServiceHandler>().To<ServiceHandler>();
        }
    }
}