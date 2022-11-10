using FluxoDeCaixa.Domain.Model;

namespace FluxoDeCaixa.Domain.Interfaces.Services
{
    public interface ILoginService
    {
        ResponseData<AutenticadoModel> Autenticar(LoginModel login);
    }
}
