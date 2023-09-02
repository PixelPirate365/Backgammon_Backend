using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.CurrencyManagment.Queries.GetTopCurrencyAccountProfiles {
    public class GetTopCurrencyAccountProfilesQuery :IRequest<List<GetTopCurrencyAccountProfileResponse>>{
        public int MaximumNumberOfProfiles { get; set; }
    }
}
