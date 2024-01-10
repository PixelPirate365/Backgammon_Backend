using AutoMapper;
using GameManagerService.Application.Handlers.FriendGame.Commands.SendGameRequest;
using GameManagerService.Common.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Application.Mappings {
    public class RecieverBalanceMappingProfile : Profile {

        public RecieverBalanceMappingProfile()
        {
            CreateMap<SendGameRequestCommand, CheckRecieverBalanceModel>();
        }
    }
}
