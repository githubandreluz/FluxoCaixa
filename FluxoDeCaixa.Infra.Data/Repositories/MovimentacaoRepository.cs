using AutoMapper;
using FluxoDeCaixa.Domain.Interfaces.Infra;
using FluxoDeCaixa.Domain.Model;
using FluxoDeCaixa.Infra.Data.Context;
using FluxoDeCaixa.Infra.Data.Entities;

namespace FluxoDeCaixa.Infra.Data.Repositories
{
    public class MovimentacaoRepository : IMovimentacaoRepository
    {

        private readonly FluxoCaixaContext _context;
        private readonly IMapper _mapper;

        public MovimentacaoRepository(IMapper mapper, FluxoCaixaContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Inserir(MovimentacaoModel novaMovimentacao)
        {
            _context.TabelaMovimentacao.Add(_mapper.Map<Movimentacao>(novaMovimentacao));
        }

    }
}
