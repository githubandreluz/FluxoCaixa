using static FluxoDeCaixa.Domain.Model.Enumeradores;

namespace FluxoDeCaixa.Domain.Model
{
    public class UsuarioModel
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ERole UserRole { get; set; }
    }
}
