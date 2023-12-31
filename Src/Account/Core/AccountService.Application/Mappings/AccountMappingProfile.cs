﻿using AccountService.Application.Handlers.Account.Commands.CreateAccountProfile;
using AccountService.Application.Handlers.Account.Commands.UpdateAccountProfile;
using AccountService.Application.Handlers.Account.Queries.GetAccountByUser;
using AccountService.Application.Handlers.Account.Queries.GetAccountProfile;
using AccountService.Application.Handlers.Friends.Queries.GetFriends;
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
            CreateMap<CreateAccountProfileCommand, AccountProfile>();
            CreateMap<UpdateAccountProfileCommand, AccountProfile>();
            CreateMap<AccountProfile, GetAccountByUserResponse>();
            CreateMap<AccountProfile, GetLoggedInProfile>().ForMember(dest =>dest.AccountId,
                opt=>opt.MapFrom(
                    src=>src.Id));
        }
    }
}
