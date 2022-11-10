using FluxoDeCaixa.Domain.Interfaces.Infra;
using FluxoDeCaixa.Infra.Data.Repositories;

namespace FluxoDeCaixa.IoC.Injectors
{
    public class RepositoriesConfig
    {
        public static Dictionary<Type, Type> GetInjectors()
        {
            return new Dictionary<Type, Type>
            {
                {typeof(IMovimentacaoRepository), typeof(MovimentacaoRepository) },
                {typeof(IConsolidadoRepository), typeof(ConsolidadoRepository)},
                {typeof(ILoginRepository), typeof(LoginRepository) }
            };
        }
    }
}
