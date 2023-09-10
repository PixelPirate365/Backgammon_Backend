using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.Account.Queries.GetAccountByUser {
    public class GetAccountByUserResponse {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
    }
}
