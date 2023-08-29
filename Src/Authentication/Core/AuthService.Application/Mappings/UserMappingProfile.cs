using AuthService.Application.Handlers.User.Commands.CreateUser;
using AuthService.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Mappings {
    public class UserMappingProfile : Profile {
        public UserMappingProfile()
        {
            CreateMap<CreateUserCommand, ApplicationUser>();
        }
    }
}
