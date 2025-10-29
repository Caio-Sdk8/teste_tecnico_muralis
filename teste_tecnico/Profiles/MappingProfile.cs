using AutoMapper;
using teste_tecnico.Domains;
using teste_tecnico.DTOs;

namespace teste_tecnico.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cliente, ClienteDto>();
            CreateMap<Endereco, EnderecoDto>();
            CreateMap<Contato, ContatoDto>();
        }
    }
}
