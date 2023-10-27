using AutoMapper;
using ScootersMc.App.ViewModels;
using ScootersMc.Business.Models;
using ScootersMc.Data.Mappings;

namespace ScootersMc.App.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<MembroMc, MembroMcViewModel>().ReverseMap();
            CreateMap<ContatoEmergencia, ContatoEmergenciaViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
        }
    }
}
