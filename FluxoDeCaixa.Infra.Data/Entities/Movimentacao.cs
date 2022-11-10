using static FluxoDeCaixa.Domain.Model.Enumeradores;

namespace FluxoDeCaixa.Infra.Data.Entities
{
    public class Movimentacao
    {
        public Guid Id { get; set; }
        public ETipoLancamento IdTipoLancamento { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataLancamento { get; set; }
        public DateTime DataCriacao { get; set; }
        public EStatus Status { get; set; }
    }
}
