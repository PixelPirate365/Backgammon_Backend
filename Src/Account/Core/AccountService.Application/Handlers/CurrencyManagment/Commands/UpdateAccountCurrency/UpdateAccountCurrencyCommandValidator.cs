using AccountService.Application.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.CurrencyManagment.Commands.UpdateAccountCurrency {
    public class UpdateAccountCurrencyCommandValidator:BaseValidator<UpdateAccountCurrencyCommand> {

        public UpdateAccountCurrencyCommandValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }
}
