using FluxoDeCaixa.Domain.Model;

namespace FluxoDeCaixa.Application.Interfaces
{
    public interface IConsultaConsolidadoAppService
    {
        ResponseData<IEnumerable<ConsolidadoModel>> GetConsolidado(int ano, int? mes = null);
    }
}
