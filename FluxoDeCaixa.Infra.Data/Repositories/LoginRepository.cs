using AutoMapper;
using FluxoDeCaixa.Domain.Interfaces.Infra;
using FluxoDeCaixa.Domain.Model;
using FluxoDeCaixa.Infra.Data.Context;

namespace FluxoDeCaixa.Infra.Data.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly FluxoCaixaContext _context;
        private readonly IMapper _mapper;
        public LoginRepository(FluxoCaixaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public UsuarioModel? GetUsuarioByLogin(string login) =>
            _mapper.Map<UsuarioModel>(_context.TabelaUsuario.FirstOrDefault(x => x.Login == login));

    }
}
