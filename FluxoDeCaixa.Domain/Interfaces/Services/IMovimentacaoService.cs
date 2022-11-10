using FluxoDeCaixa.Domain.Model;

namespace FluxoDeCaixa.Domain.Interfaces.Services
{
    public interface IMovimentacaoService
    {
        ResponseData inserir(MovimentacaoModel movimentacao);
    }
}
