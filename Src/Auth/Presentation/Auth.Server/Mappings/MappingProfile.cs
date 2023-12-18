using Auth.Server.Entities;
using Auth.Server.Quickstart.ViewModels;
using AutoMapper;

namespace Auth.Server.Mappings {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<UserRegistrationModel, User>()
                .ForMember(dest =>
                dest.UserName, opt =>
                opt.MapFrom(src => src.Email));
        }
    }
}
