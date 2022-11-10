using static FluxoDeCaixa.Domain.Model.Enumeradores;

namespace FluxoDeCaixa.Application.ViewModels
{
    public class MovimentacaoViewModel
    {
        /// <summary>
        /// 1 - para Crédito, 2 - para Débito
        /// </summary>
        public ETipoLancamento IdTipoLancamento { get; set; }
        /// <summary>
        /// Valor do lançamento, apenas valores positivos maior que 0 (zero)
        /// </summary>
        public decimal Valor { get; set; }
        /// <summary>
        /// Data do lançamento no formato yyyy-mm-dd
        /// </summary>
        public DateTime DataLancamento { get; set; }
    }
}
