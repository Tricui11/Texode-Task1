using AutoMapper;
using InfoCards.Api.Contract.DTOs;
using InfoCards.WebApi.Models;

namespace InfoCards.WebApi.MapperProfiles {
  public class MappingProfile : Profile {
    public MappingProfile() {
      CreateMap<InfoCard, InfoCardDto>();
    }
  }
}
