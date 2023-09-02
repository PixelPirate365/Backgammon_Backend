using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.CurrencyManagment.Commands.UpdateAccountCurrency {
    public class UpdateAccountCurrencyResponse {
        public Guid Id { get; set; }
        public string AccountNickname { get; set; }
        public string Currency { get; set; }
        public int TotalAmount { get; set; }
    }
}
