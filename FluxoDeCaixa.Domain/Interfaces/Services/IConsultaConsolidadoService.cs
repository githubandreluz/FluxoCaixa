using FluxoDeCaixa.Domain.Model;

namespace FluxoDeCaixa.Domain.Interfaces.Services
{
    public interface IConsultaConsolidadoService
    {
        ResponseData<IEnumerable<ConsolidadoModel>> GetConsolidado(int ano, int? mes = null);
    }
}
