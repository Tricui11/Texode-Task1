using AutoMapper;
using InfoCards.Api.Contract.DTOs;
using InfoCards.Api.Contract.Models;

namespace InfoCards.Api.BusinessLogic.Abstract {
  public class MappingProfile : Profile {
    public MappingProfile() {
      CreateMap<InfoCard, InfoCardDto>();
    }
  }
}
