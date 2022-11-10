namespace FluxoDeCaixa.Domain.Model
{
    public class Enumeradores
    {
        public enum ETipoLancamento
        {
            Credito = 1,
            Debito = 2
        }
        public enum EStatus
        {
            Pendente = 0,
            Calculado = 1
        }
        public enum ERole
        {
            Admin = 1,
            Funcionario = 2
        }
    }
}
