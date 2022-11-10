using FluxoDeCaixa.Domain.Interfaces.Infra;
using FluxoDeCaixa.Domain.Interfaces.Services;
using FluxoDeCaixa.Domain.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FluxoDeCaixa.Autenticacao
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IConfiguration _configuration;
        public LoginService(ILoginRepository loginRepository, IConfiguration configuration)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
        }

        private ResponseData ValidarSenha(LoginModel login, UsuarioModel usuario)
        {
            var response = new ResponseData();

            if (usuario == null)
            {
                response.addError("Usuário não existe");
                return response;
            }

            if (login.Login != usuario.Login)
                response.addError("Login inválido!");

            if (login.Password != usuario.Password)
                response.addError("Senha incorreta!");

            return response;
        }
        private string GerarToken(UsuarioModel usuario)
        {
            var secaoJwt = _configuration.GetSection("JwtToken");
            var key = Encoding.ASCII.GetBytes(secaoJwt.GetValue<string>("SecretKey"));
            var issuer = secaoJwt.GetValue<string>("Issuer");
            var ExpireHours = secaoJwt.GetValue<int>("ExpireHours");

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = new JwtSecurityToken(
                    claims: new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.Login),
                        new Claim(ClaimTypes.Name, usuario.UserName),
                        new Claim(ClaimTypes.Sid, usuario.Id.ToString()),
                        new Claim(ClaimTypes.Role, usuario.UserRole.ToString())
                    },
                    expires: DateTime.UtcNow.AddHours(ExpireHours),
                    issuer: issuer,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                );

            return tokenHandler.WriteToken(token);

        }
        private AutenticadoModel CriarCedencial(UsuarioModel usuario)
        {
            return new AutenticadoModel
            {
                DataAutenticado = DateTime.Now,
                Token = GerarToken(usuario),
                UserName = usuario.UserName,
                UserRole = usuario.UserRole
            };
        }
        public ResponseData<AutenticadoModel> Autenticar(LoginModel login)
        {
            var response = new ResponseData<AutenticadoModel>();
            var usuario = _loginRepository.GetUsuarioByLogin(login.Login);

            response.addError(ValidarSenha(login, usuario).AllErrors);

            if (response.HasErrors)
                return response;

            response.Response = CriarCedencial(usuario);
            return response;
        }
    }
}