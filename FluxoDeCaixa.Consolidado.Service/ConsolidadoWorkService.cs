using FluxoDeCaixa.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FluxoDeCaixa.Consolidado.Service
{
    public class ConsolidadoWorkService : BackgroundService
    {
        public IServiceProvider _services { get; }
        public ConsolidadoWorkService(IServiceProvider services)
        {
            _services = services;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Consolidar(stoppingToken);
        }
        private async Task Consolidar(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _services.CreateScope())
                {
                    var consolidadoService =
                        scope.ServiceProvider
                            .GetRequiredService<IConsolidadoService>();

                    await consolidadoService.ExecutarConsolidacao();
                }

                await Task.Delay(3000, stoppingToken);
            }
        }

    }
}