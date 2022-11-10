using FluxoDeCaixa.Domain.Interfaces.Infra;
using FluxoDeCaixa.Domain.Interfaces.Services;
using FluxoDeCaixa.Domain.Model;

namespace FluxoDeCaixa.Domain.Services
{
    public class MovimentacaoService : IMovimentacaoService
    {
        private readonly IMovimentacaoRepository _repository;
        public MovimentacaoService(IMovimentacaoRepository repository)
        {
            _repository = repository;
        }
        private ResponseData ValidarNovoLancamento(MovimentacaoModel movimentacao)
        {
            var responseData = new ResponseData();

            if (movimentacao.Valor <= 0)
                responseData.addError("Valor inválido, o valor deve ser superior a zero");

            return responseData;
        }
        public ResponseData inserir(MovimentacaoModel movimentacao)
        {
            var result = ValidarNovoLancamento(movimentacao);
            if (result.HasErrors)
                return result;
            _repository.Inserir(movimentacao);
            return result;
        }
    }
}
