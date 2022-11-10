namespace FluxoDeCaixa.Application.ViewModels
{
    public class AutenticadoViewModel
    {
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string Token { get; set; }
        public DateTime DataAutenticado { get; set; }
    }
}
