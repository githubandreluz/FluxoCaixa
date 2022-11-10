using AutoMapper;
using FluxoDeCaixa.Domain.Interfaces.Infra;
using FluxoDeCaixa.Domain.Model;
using FluxoDeCaixa.Infra.Data.Context;
using FluxoDeCaixa.Infra.Data.Entities;
using static FluxoDeCaixa.Domain.Model.Enumeradores;

namespace FluxoDeCaixa.Infra.Data.Repositories
{
    public class ConsolidadoRepository : IConsolidadoRepository
    {
        private readonly FluxoCaixaContext _context;
        private readonly IMapper _mapper;
        public ConsolidadoRepository(FluxoCaixaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<DateTime> GetDataPendentesCalcular()
        {
            return _context.TabelaMovimentacao.Where(x => x.Status == EStatus.Pendente).Select(x => x.DataLancamento.Date).Distinct();
        }
        public IEnumerable<ConsolidadoModel> GetConsolidadoData(IEnumerable<DateTime> datas)
        {
            return _context.TabelaMovimentacao.Where(x => datas.Contains(x.DataLancamento.Date))
                .GroupBy(x => x.DataLancamento.Date)
                .Select(x => new ConsolidadoModel
                {
                    Data = x.First().DataLancamento.Date,
                    ValorCosolidado = x.Sum(y => y.IdTipoLancamento == ETipoLancamento.Credito ? y.Valor : (y.Valor * -1)),
                    DataUltimoCalculo = DateTime.Now
                });

        }
        public IEnumerable<ConsolidadoModel> GetConsolidado(int ano, int? mes = null)
        {
            var query = _context.TabelaConsolidado.OrderBy(x => x.Data).AsQueryable();
            query = query.Where(x => x.Data.Year == ano);
            if (mes != null)
                query = query.Where(x => x.Data.Month == mes);

            return _mapper.Map<IEnumerable<ConsolidadoModel>>(query);
        }
        public bool PossuiDataConsolidada(DateTime data)
        {
            return _context.TabelaConsolidado.Any(x => x.Data.Date == data.Date);
        }
        public void UpdateConsolidado(ConsolidadoModel consolidado)
        {
            var index = _context.TabelaConsolidado.FindIndex(x => x.Data.Date == consolidado.Data.Date);
            _context.TabelaConsolidado[index] = _mapper.Map<Consolidado>(consolidado);
        }
        public void InserirConsolidado(ConsolidadoModel consolidado)
        {
            _context.TabelaConsolidado.Add(_mapper.Map<Consolidado>(consolidado));
        }
        public void AlterarDatasParaCalculado(DateTime data, DateTime dataProcessamento)
        {
            var regs = _context.TabelaMovimentacao.Where(x => x.DataLancamento.Date == data.Date && x.Status == EStatus.Pendente && x.DataCriacao <= dataProcessamento).ToList();
            regs.ForEach(x => x.Status = EStatus.Calculado);
        }
    }
}
