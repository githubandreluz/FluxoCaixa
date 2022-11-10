using AutoMapper;
using FluxoDeCaixa.Application.Interfaces;
using FluxoDeCaixa.Application.ViewModels;
using FluxoDeCaixa.Domain.Interfaces.Services;
using FluxoDeCaixa.Domain.Model;

namespace FluxoDeCaixa.Application.AppServices
{
    public class MovimentacaoAppService : IMovimentacaoAppService
    {
        private readonly IMovimentacaoService _movimentacaoService;
        private readonly IMapper _mapper;
        public MovimentacaoAppService(IMovimentacaoService movimentacaoService, IMapper mapper)
        {
            _mapper = mapper;
            _movimentacaoService = movimentacaoService;
        }

        public ResponseData inserir(MovimentacaoViewModel novaMovimentacao) =>
            _movimentacaoService.inserir(_mapper.Map<MovimentacaoModel>(novaMovimentacao));

    }
}
