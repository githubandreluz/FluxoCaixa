using static FluxoDeCaixa.Domain.Model.Enumeradores;

namespace FluxoDeCaixa.Domain.Model
{
    public class MovimentacaoModel
    {
        public MovimentacaoModel()
        {
            Id = Guid.NewGuid();
            Status = EStatus.Pendente;
        }
        public Guid Id { get; set; }
        public ETipoLancamento IdTipoLancamento { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataLancamento { get; set; }
        public DateTime DataCriacao { get; set; }
        public EStatus Status { get; set; }

    }
}
