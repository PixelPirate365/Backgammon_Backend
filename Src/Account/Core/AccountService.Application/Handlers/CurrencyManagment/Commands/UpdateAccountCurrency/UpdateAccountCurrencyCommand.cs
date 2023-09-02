using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.CurrencyManagment.Commands.UpdateAccountCurrency {
    public class UpdateAccountCurrencyCommand:IRequest<UpdateAccountCurrencyResponse> {
        public bool HasWon { get; set; }
        public int Amount { get; set; }
    }
}
