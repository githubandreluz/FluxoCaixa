using FluxoDeCaixa.Domain.Model;

namespace FluxoDeCaixa.Domain.Interfaces.Infra
{
    public interface IConsolidadoRepository
    {
        IEnumerable<DateTime> GetDataPendentesCalcular();
        void InserirConsolidado(ConsolidadoModel consolidado);
        void UpdateConsolidado(ConsolidadoModel consolidado);
        bool PossuiDataConsolidada(DateTime data);
        IEnumerable<ConsolidadoModel> GetConsolidadoData(IEnumerable<DateTime> datas);
        void AlterarDatasParaCalculado(DateTime data, DateTime dataProcessamento);
        IEnumerable<ConsolidadoModel> GetConsolidado(int ano, int? mes = null);
    }
}
