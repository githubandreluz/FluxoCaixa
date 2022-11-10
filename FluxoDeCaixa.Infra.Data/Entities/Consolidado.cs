namespace FluxoDeCaixa.Infra.Data.Entities
{
    public class Consolidado
    {
        public DateTime Data { get; set; }
        public decimal ValorCosolidado { get; set; }
        public DateTime DataUltimoCalculo { get; set; }
    }
}
