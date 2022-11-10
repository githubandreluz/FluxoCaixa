using AutoMapper;
using FluxoDeCaixa.Application.ViewModels;
using FluxoDeCaixa.Domain.Model;

namespace FluxoDeCaixa.Application.Mapper
{
    public class ProfileConfig : Profile
    {
        public ProfileConfig()
        {
            CreateMap<MovimentacaoViewModel, MovimentacaoModel>()
                .ForMember(x => x.DataCriacao, opt => opt.MapFrom(x => DateTime.Now))
                .ReverseMap();

            CreateMap<AutenticadoViewModel, AutenticadoModel>()
                .ReverseMap();
            CreateMap<LoginViewModel, LoginModel>()
                .ReverseMap();

        }
    }
}
