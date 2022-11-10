using static FluxoDeCaixa.Domain.Model.Enumeradores;

namespace FluxoDeCaixa.Domain.Model
{
    public class AutenticadoModel
    {
        public string UserName { get; set; }
        public ERole UserRole { get; set; }
        public string Token { get; set; }
        public DateTime DataAutenticado { get; set; }
    }
}
