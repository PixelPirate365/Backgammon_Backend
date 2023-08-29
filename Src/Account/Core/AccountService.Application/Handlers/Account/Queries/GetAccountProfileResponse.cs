using AccountService.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.Account.Queries {
    public class GetAccountProfileResponse {
        public Guid Id { get; set; }
        public string? Nickname { get; set; }
        public string? ProfileDescription { get; set; }
        public DateTime? BirthDate { get; set; }
        public GenderEnum Gender { get; set; }
        public string? Image { get; set; }
    }
}
