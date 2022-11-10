namespace FluxoDeCaixa.Domain.Model
{
    public class ConsolidadoModel
    {
        public DateTime Data { get; set; }
        public decimal ValorCosolidado { get; set; }
        public DateTime DataUltimoCalculo { get; set; }
    }
}
