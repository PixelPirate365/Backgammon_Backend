using AutoMapper;
using GameManagerService.Application.Handlers.Profile.Commands.CreatePlayer;
using GameManagerService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Application.Mappings
{
    public class PlayerMappingProfile : Profile {
        public PlayerMappingProfile() {
            CreateMap<CreatePlayerCommand, Player>();
        }
    }
}
