using AccountService.Application.Handlers.Account.Queries;
using AccountService.Domain.Entities;
using AutoMapper;

namespace AccountService.Application.Mappings
{
    public class AccountMappingProfile:Profile {
        public AccountMappingProfile() {
            CreateMap<AccountProfile,GetAccountProfileResponse>();
        }
    }
}
