using FluxoDeCaixa.Domain.Model;

namespace FluxoDeCaixa.Domain.Interfaces.Infra
{
    public interface IMovimentacaoRepository
    {
        void Inserir(MovimentacaoModel novaMovimentacao);
    }
}
