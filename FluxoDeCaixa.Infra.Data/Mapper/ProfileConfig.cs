using AutoMapper;
using FluxoDeCaixa.Domain.Model;
using FluxoDeCaixa.Infra.Data.Entities;

namespace FluxoDeCaixa.Infra.Data.Mapper
{
    public class ProfileConfig : Profile
    {
        public ProfileConfig()
        {
            CreateMap<Movimentacao, MovimentacaoModel>()
                .ReverseMap();
            CreateMap<Consolidado, ConsolidadoModel>()
                .ReverseMap();
            CreateMap<Usuario, UsuarioModel>()
                .ReverseMap();
        }
    }
}
