using FluxoDeCaixa.Domain.Model;

namespace FluxoDeCaixa.Domain.Interfaces.Infra
{
    public interface ILoginRepository
    {
        UsuarioModel? GetUsuarioByLogin(string login);
    }
}
