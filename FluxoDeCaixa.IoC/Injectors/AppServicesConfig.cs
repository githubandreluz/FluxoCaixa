using FluxoDeCaixa.Application.AppServices;
using FluxoDeCaixa.Application.Interfaces;

namespace FluxoDeCaixa.IoC.Injectors
{
    public class AppServicesConfig
    {
        public static Dictionary<Type, Type> GetInjectors()
        {
            return new Dictionary<Type, Type>()
            {
                {typeof(IMovimentacaoAppService), typeof(MovimentacaoAppService) },
                {typeof(IConsultaConsolidadoAppService), typeof(ConsultaConsolidadoAppService) },
                {typeof(ILoginAppService), typeof(LoginAppService) }
            };
        }
    }
}
