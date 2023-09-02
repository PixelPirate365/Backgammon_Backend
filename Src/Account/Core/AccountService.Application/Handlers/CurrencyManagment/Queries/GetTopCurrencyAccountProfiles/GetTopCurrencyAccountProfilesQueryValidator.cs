using AccountService.Application.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.CurrencyManagment.Queries.GetTopCurrencyAccountProfiles {
    public class GetTopCurrencyAccountProfilesQueryValidator:BaseValidator<GetTopCurrencyAccountProfilesQuery> {
        public GetTopCurrencyAccountProfilesQueryValidator()
        {
            RuleFor(x => x.MaximumNumberOfProfiles).GreaterThan(0)
                .LessThanOrEqualTo(200);
        }
    }
}
