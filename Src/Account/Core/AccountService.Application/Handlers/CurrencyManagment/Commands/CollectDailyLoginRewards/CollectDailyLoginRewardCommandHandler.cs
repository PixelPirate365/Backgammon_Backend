using AccountService.Application.Handlers.Account.Queries.GetAccountProfile;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.CurrencyManagment.Commands.CollectDailyLoginRewards {
    public class CollectDailyLoginRewardCommandHandler : IRequestHandler<CollectDailyLoginRewardCommand, AccountTotalCoinsResponse> {
        readonly ILogger<CollectDailyLoginRewardCommandHandler> _logger;
        readonly IMapper _mapper;

        public CollectDailyLoginRewardCommandHandler(ILogger<CollectDailyLoginRewardCommandHandler> logger,
           IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<AccountTotalCoinsResponse> IRequestHandler<CollectDailyLoginRewardCommand, AccountTotalCoinsResponse>.Handle(CollectDailyLoginRewardCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(CollectDailyLoginRewardCommandHandler)}");


            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(CollectDailyLoginRewardCommandHandler)}");

            throw new NotImplementedException();
        }
    }
}
