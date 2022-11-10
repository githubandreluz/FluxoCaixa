using static FluxoDeCaixa.Domain.Model.Enumeradores;

namespace FluxoDeCaixa.Infra.Data.Entities
{
    public class Usuario
    {
        public Usuario()
        {

        }
        public Usuario(string login, string userName, string senha, ERole userRole)
        {
            Id = Guid.NewGuid();
            Login = login;
            UserName = userName;
            Password = senha;
            UserRole = userRole;

        }
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ERole UserRole { get; set; }
    }
}
