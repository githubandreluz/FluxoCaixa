using FluxoDeCaixa.Application.ViewModels;
using FluxoDeCaixa.Domain.Model;

namespace FluxoDeCaixa.Application.Interfaces
{
    public interface IMovimentacaoAppService
    {
        ResponseData inserir(MovimentacaoViewModel novaMovimentacao);
    }
}
