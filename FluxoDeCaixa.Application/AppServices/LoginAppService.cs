using AutoMapper;
using FluxoDeCaixa.Application.Interfaces;
using FluxoDeCaixa.Application.ViewModels;
using FluxoDeCaixa.Domain.Interfaces.Services;
using FluxoDeCaixa.Domain.Model;

namespace FluxoDeCaixa.Application.AppServices
{
    public class LoginAppService : ILoginAppService
    {
        private readonly ILoginService _loginService;
        private readonly IMapper _mapper;
        public LoginAppService(ILoginService loginService, IMapper mapper)
        {
            _loginService = loginService;
            _mapper = mapper;
        }

        public ResponseData<AutenticadoViewModel> Login(LoginViewModel login)
        {
            var response = new ResponseData<AutenticadoViewModel>();

            var result = _loginService.Autenticar(_mapper.Map<LoginModel>(login));

            if (result.HasErrors)
                return response.addError(result.AllErrors);

            response.Response = _mapper.Map<AutenticadoViewModel>(result.Response);

            return response;
        }

    }
}
