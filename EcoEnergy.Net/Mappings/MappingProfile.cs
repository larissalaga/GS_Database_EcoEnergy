using AutoMapper;
using EcoEnergy.Dtos;
using EcoEnergy.Models;

namespace EcoEnergy.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeamento de entidades para Dtos
            CreateMap<EmpresaDtos, Empresa>();
            CreateMap<Empresa, EmpresaDtos>();
            CreateMap<UsuarioDtos, Usuario>();
            CreateMap<Usuario, UsuarioDtos>();
            CreateMap<EnderecoDtos, Endereco>();
            CreateMap<Endereco, EnderecoDtos>();
            CreateMap<OrcamentoDtos, Orcamento>();
            CreateMap<Orcamento, OrcamentoDtos>();
            CreateMap<SimulacaoDtos, Simulacao>();
            CreateMap<Simulacao, SimulacaoDtos>();


        }
    }
}
