using AccountService.Application.Handlers.Account.Commands;
using AccountService.Application.Handlers.Account.Queries;
using AccountService.Common.Settings;
using AccountService.Domain.Entities;
using AutoMapper;

namespace AccountService.Application.Mappings
{
    public class AccountMappingProfile:Profile {
        public AccountMappingProfile() {
            CreateMap<AccountProfile,GetAccountProfileResponse>()
                .ForMember(dest => dest.Image,
                opt => opt.MapFrom(
                    src => $"{AccountApiSettings.ApiBaseUrl}{src.Image}"));
            CreateMap<UpdateAccountProfileCommand, UpdateAccountResponse>();
        }
    }
}
