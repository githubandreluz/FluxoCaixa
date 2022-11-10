using FluxoDeCaixa.Autenticacao;
using FluxoDeCaixa.Domain.Interfaces.Services;
using FluxoDeCaixa.Domain.Services;

namespace FluxoDeCaixa.IoC.Injectors
{
    public class ServicesConfig
    {
        public static Dictionary<Type, Type> GetInjectors()
        {
            return new Dictionary<Type, Type>
            {
                { typeof(IMovimentacaoService), typeof(MovimentacaoService)},
                { typeof(IConsultaConsolidadoService), typeof(ConsultaConsolidadoService) },
                { typeof(IConsolidadoService), typeof(ConsolidadoService)},
                { typeof(ILoginService), typeof(LoginService) }
            };
        }
    }
}
