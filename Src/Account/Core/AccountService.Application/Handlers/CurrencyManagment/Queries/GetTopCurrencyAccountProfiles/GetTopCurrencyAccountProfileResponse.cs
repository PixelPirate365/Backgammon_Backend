using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.CurrencyManagment.Queries.GetTopCurrencyAccountProfiles {
    public class GetTopCurrencyAccountProfileResponse {
        public Guid Id { get; set; }
        public string AccountNickname { get; set; }
        public string Currency { get; set; }
        public int TotalAmount { get; set; }
    }
}
