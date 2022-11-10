using FluxoDeCaixa.Domain.Interfaces.Infra;
using FluxoDeCaixa.Domain.Interfaces.Services;
using FluxoDeCaixa.Domain.Model;

namespace FluxoDeCaixa.Domain.Services
{
    public class ConsolidadoService : IConsolidadoService
    {
        private readonly IConsolidadoRepository _consolidadoRepository;
        public ConsolidadoService(IConsolidadoRepository consolidadoRepository)
        {
            _consolidadoRepository = consolidadoRepository;
        }

        private void InsertOrUpdate(ConsolidadoModel consolidado)
        {
            if (!_consolidadoRepository.PossuiDataConsolidada(consolidado.Data.Date))
                _consolidadoRepository.InserirConsolidado(consolidado);
            else
                _consolidadoRepository.UpdateConsolidado(consolidado);
        }
        public async Task ExecutarConsolidacao()
        {
            var datasRecalcular = _consolidadoRepository.GetDataPendentesCalcular();
            var dataProcessamento = DateTime.Now;
            var datasJaConsolidadas = _consolidadoRepository.GetConsolidadoData(datasRecalcular);
            foreach (var consolidado in datasJaConsolidadas)
            {
                InsertOrUpdate(consolidado);
                _consolidadoRepository.AlterarDatasParaCalculado(consolidado.Data, dataProcessamento);
            }

            await Task.CompletedTask;
        }
    }
}
