using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Application.Handlers.Account.Queries.GetAccountByUser;
using AccountService.Common.Constants;
using AccountService.Common.EventModels;
using AccountService.Domain.Entities;
using AccountService.MessageBus.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.Account.Queries.GetAccountBalance {
    public class GetAccountBalanceQueryHandler : IRequestHandler<GetAccountBalanceQuery, Unit> {
        readonly ILogger<GetAccountBalanceQueryHandler> _logger;
        readonly IRepository<AccountProfileCurrency> _accountProfileCurrencyRepository;
        readonly IMapper _mapper;
        readonly RabbitMQMessageSender _rabbitMQMessageSender;
        public GetAccountBalanceQueryHandler(
            ILogger<GetAccountBalanceQueryHandler> logger,
            IRepository<AccountProfileCurrency> accountProfileCurrencyRepository,
            IMapper mapper,
            RabbitMQMessageSender rabbitMQMessageSender)
        {
            _logger = logger;
            _mapper = mapper;
            _accountProfileCurrencyRepository = accountProfileCurrencyRepository;
            _rabbitMQMessageSender = rabbitMQMessageSender;
        }

        public async Task<Unit> Handle(GetAccountBalanceQuery request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(GetAccountBalanceQueryHandler)}");
            var accountProfileCurrency = await _accountProfileCurrencyRepository.TableNoTracking
                .Where(x=> x.AccountProfileId == request.RecieverId)
                .ProjectTo<SendRecieverBalanceModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            accountProfileCurrency.SenderId = request.SenderId;
            accountProfileCurrency.BetAmount = request.BetAmount;
            _rabbitMQMessageSender.SendMessage(accountProfileCurrency, EventNameConstants.SendRecieverBalanceEvent);
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(GetAccountBalanceQueryHandler)}");
            return Unit.Value;
        }
    }
}
