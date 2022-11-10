using FluxoDeCaixa.Application.Interfaces;
using FluxoDeCaixa.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FluxoDeCaixa.API.Controllers
{
    public class AuthController : CustomBaseController
    {
        private readonly ILoginAppService _loginAppService;
        public AuthController(ILoginAppService loginAppService)
        {
            _loginAppService = loginAppService;
        }

        ///<sumary>
        ///Realiza a autenticação e retorna o token juntamente com os dados básicos do usuário
        ///</sumary>
        [SwaggerOperation(
            Summary = "Autenticação do usuário",
            OperationId = "Login"
        )]
        [SwaggerResponse(200, "Autenticado com sucesso, o token foi gerado", typeof(AutenticadoViewModel))]
        [SwaggerResponse(400, "Falha na autenticação")]
        [HttpPost]
        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginViewModel login)
            => returnToHttp(_loginAppService.Login(login));
    }
}
