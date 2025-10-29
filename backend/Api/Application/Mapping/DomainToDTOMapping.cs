using Api.Models;
using AutoMapper;

namespace Api.Application.Mapping;

public class DomainToDTOMapping : Profile {
  public DomainToDTOMapping() {
    CreateMap<Product, ProductDTO>()
        .ForMember(dest => dest.Name, m => m.MapFrom(orig => orig.Name));
  }
}

