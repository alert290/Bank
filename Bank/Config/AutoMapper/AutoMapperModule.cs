using AutoMapper;
using Ninject.Modules;

namespace Bank.Config.AutoMapper
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            var mapperConfiguration = this.CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();
            Bind<IMapper>().ToMethod(ctx => new Mapper(mapperConfiguration, type => ctx.Kernel.GetService(type)));
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CreditCardProfile>();
                cfg.AddProfile<CustomerProfile>();
                cfg.AddProfile<TransactionProfile>();
            });

            config.AssertConfigurationIsValid();
            return config;
        }
    }
}