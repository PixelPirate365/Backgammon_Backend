using AutoMapper;
using GameManagerService.Application.Handlers.FriendGame.Commands.SendGameRequest;
using GameManagerService.Common.Enums;
using GameManagerService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Application.Mappings {
    public class FriendGameRequestMappingProfile :Profile{
        public FriendGameRequestMappingProfile() {
            CreateMap<SendGameRequestCommand, FriendGameRequest>()
                .ForMember(dest=>dest.PlayerRecieverId,
                opt=>opt.MapFrom
                (src=>src.RecieverId))
                .ForMember(dest=>dest.Status, opt=>
                opt.MapFrom(src=> (int)FriendGameRequestEnum.Pending
                ));
        }
    }
}
