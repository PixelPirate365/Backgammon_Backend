using AccountService.Application.Handlers.Account.Queries;
using AccountService.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Mappings {
    public class AccountMappingProfile:Profile {
        public AccountMappingProfile() {
            CreateMap<AccountProfile,GetAccountProfileResponse>();
        }
    }
}
