using FluxoDeCaixa.Infra.Data.Context;
using FluxoDeCaixa.IoC.Injectors;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoDeCaixa.IoC
{
    public class BootStrapperConfig
    {
        public static void RegisterServices(IServiceCollection services)
        {
            Load(services, AppServicesConfig.GetInjectors());
            Load(services, ServicesConfig.GetInjectors());
            Load(services, RepositoriesConfig.GetInjectors());

            //This to explain my self at this part, i'm injecting this service as Singleton to simulate my database in order to do not loose information while the API is running.
            //This will allow me to work with the information without a real database.
            services.AddSingleton(typeof(FluxoCaixaContext), typeof(FluxoCaixaContext));
        }

        private static void Load(IServiceCollection services, Dictionary<Type, Type> injectors)
        {
            foreach (var item in injectors)
            {
                var service = item.Key;
                var implementation = item.Value;
                services.AddScoped(service, implementation);
            }
        }
    }
}
