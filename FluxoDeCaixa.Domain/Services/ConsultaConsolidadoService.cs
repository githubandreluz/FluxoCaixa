using FluxoDeCaixa.Domain.Interfaces.Infra;
using FluxoDeCaixa.Domain.Interfaces.Services;
using FluxoDeCaixa.Domain.Model;

namespace FluxoDeCaixa.Domain.Services
{
    public class ConsultaConsolidadoService : IConsultaConsolidadoService
    {
        private readonly IConsolidadoRepository _consolidadoRepository;
        public ConsultaConsolidadoService(IConsolidadoRepository consolidadoRepository)
        {
            _consolidadoRepository = consolidadoRepository;
        }
        private ResponseData ValidarParametrosEntrada(int ano, int? mes = null)
        {
            ResponseData validacao = new();
            if (ano.ToString().Length != 4)
                validacao.addError("O ano deve estar no formato YYYY.");
            if (mes != null && (mes < 1 || mes > 12))
                validacao.addError("O mes deve estar entre 1 e 12");

            return validacao;
        }
        public ResponseData<IEnumerable<ConsolidadoModel>> GetConsolidado(int ano, int? mes = null)
        {
            var response = new ResponseData<IEnumerable<ConsolidadoModel>>();
            var result = ValidarParametrosEntrada(ano, mes);

            if (result.HasErrors)
                return response.addError(result.AllErrors);

            response.Response = _consolidadoRepository.GetConsolidado(ano, mes);
            return response;
        }
    }
}
