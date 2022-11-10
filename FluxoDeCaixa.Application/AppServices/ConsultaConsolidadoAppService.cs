using FluxoDeCaixa.Application.Interfaces;
using FluxoDeCaixa.Domain.Interfaces.Services;
using FluxoDeCaixa.Domain.Model;

namespace FluxoDeCaixa.Application.AppServices
{
    public class ConsultaConsolidadoAppService : IConsultaConsolidadoAppService
    {
        private readonly IConsultaConsolidadoService _consolidadoService;
        public ConsultaConsolidadoAppService(IConsultaConsolidadoService consolidadoService)
        {
            _consolidadoService = consolidadoService;
        }

        public ResponseData<IEnumerable<ConsolidadoModel>> GetConsolidado(int ano, int? mes = null) =>
            _consolidadoService.GetConsolidado(ano, mes);

    }
}
