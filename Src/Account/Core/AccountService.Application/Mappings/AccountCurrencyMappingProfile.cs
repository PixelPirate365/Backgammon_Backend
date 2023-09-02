using AccountService.Application.Handlers.CurrencyManagment.Queries.GetTopCurrencyAccountProfiles;
using AccountService.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Mappings {
    public class AccountCurrencyMappingProfile:Profile {
        public AccountCurrencyMappingProfile()
        {
            CreateMap<AccountProfileCurrency, GetTopCurrencyAccountProfileResponse>()
                .ForMember(dest=>dest.AccountNickname,
                opt=>opt
                .MapFrom(src=>src.AccountProfile.Nickname))
                .ForMember(dest => dest.Currency,
                opt=>opt.MapFrom(
                    src=>src.Currency.Name))
                .ForMember(dest=>dest.TotalAmount,
                opt=>opt
                .MapFrom(src=>(int)src.TotalAmount));
        }
    }
}
