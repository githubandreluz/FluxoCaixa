using FluxoDeCaixa.Infra.Data.Entities;
using static FluxoDeCaixa.Domain.Model.Enumeradores;

namespace FluxoDeCaixa.Infra.Data.Context
{
    public class FluxoCaixaContext
    {
        public FluxoCaixaContext()
        {
            TabelaMovimentacao = new List<Movimentacao>();
            TabelaConsolidado = new List<Consolidado>();
            TabelaUsuario = new List<Usuario>();

            //Mocking some uers in order to test the authentication
            TabelaUsuario.AddRange(
                new Usuario[] {
                new Usuario("andre", "André Luz","andrepassword", ERole.Admin),
                new Usuario("joao", "João da Silva","joaopassword", ERole.Funcionario)
                });
        }
        public List<Movimentacao> TabelaMovimentacao { get; set; }
        public List<Consolidado> TabelaConsolidado { get; set; }
        public List<Usuario> TabelaUsuario { get; set; }
    }
}
