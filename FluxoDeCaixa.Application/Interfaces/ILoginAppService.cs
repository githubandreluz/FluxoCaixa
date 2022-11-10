using FluxoDeCaixa.Application.ViewModels;
using FluxoDeCaixa.Domain.Model;

namespace FluxoDeCaixa.Application.Interfaces
{
    public interface ILoginAppService
    {
        ResponseData<AutenticadoViewModel> Login(LoginViewModel login);
    }
}
