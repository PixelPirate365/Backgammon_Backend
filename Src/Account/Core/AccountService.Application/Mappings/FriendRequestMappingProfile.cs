using AccountService.Application.Handlers.FriendRequests.Commands.SendFriendRequest;
using AccountService.Application.Handlers.FriendRequests.Queries.GetFriendsRequest;
using AccountService.Common.Enums;
using AccountService.Domain.Entities;
using AutoMapper;

namespace AccountService.Application.Mappings
{
    public class FriendRequestMappingProfile : Profile {
        public FriendRequestMappingProfile()
        {
            CreateMap<SendFriendRequestCommand, FriendRequest>()
                .ForMember(dest => dest.Status,
                opt => opt.MapFrom(
                    src => FriendRequestStatusEnum.Pending));
            CreateMap<FriendRequest,GetFriendRequestResponse>()
                .ForMember(dest => dest.SenderName,
                opt=> opt.MapFrom(
                    src=>src.SenderProfile.Nickname));
        }
    }
}
