using Api.Models;
using AutoMapper;

namespace Api.Application.Mapping;

public class DomainToDTOMapping : Profile {
  public DomainToDTOMapping() {
    CreateMap<User, UserDTO>();

    CreateMap<Post, PostResponseDto>();
  }
}

