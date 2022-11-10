using Microsoft.Extensions.DependencyInjection;
using App = FluxoDeCaixa.Application.Mapper;
using Repo = FluxoDeCaixa.Infra.Data.Mapper;
namespace FluxoDeCaixa.IoC
{
    public static class MapperConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddAutoMapper(x =>
            {
                x.AddProfile(new App.ProfileConfig());
                x.AddProfile(new Repo.ProfileConfig());
            });
        }
    }
}
