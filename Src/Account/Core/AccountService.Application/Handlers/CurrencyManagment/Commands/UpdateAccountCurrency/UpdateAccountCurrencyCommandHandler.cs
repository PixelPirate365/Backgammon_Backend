using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Application.Handlers.CurrencyManagment.Commands.CollectDailyLoginRewards;
using AccountService.Application.Interfaces;
using AccountService.Common.Constants;
using AccountService.Domain.Entities;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AccountService.Application.Handlers.CurrencyManagment.Commands.UpdateAccountCurrency {
    public class UpdateAccountCurrencyCommandHandler : IRequestHandler<UpdateAccountCurrencyCommand, UpdateAccountCurrencyResponse> {
        readonly ILogger<CollectDailyLoginRewardCommandHandler> _logger;
        readonly IMapper _mapper;
        readonly IRepository<AccountProfileCurrency> _accountProfileCurrencyRepository;
        readonly ICurrentUserService _currentUserService;
        public UpdateAccountCurrencyCommandHandler(
            ILogger<CollectDailyLoginRewardCommandHandler> logger,
            IMapper mapper,
            IRepository<AccountProfileCurrency> accountProfileCurrencyRepository,
            ICurrentUserService currentUserService
            ) {
            _accountProfileCurrencyRepository = accountProfileCurrencyRepository;
            _logger = logger;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<UpdateAccountCurrencyResponse> Handle(UpdateAccountCurrencyCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(UpdateAccountCurrencyCommandHandler)}");
            var result = await _accountProfileCurrencyRepository.TableNoTracking.Include(
                x => x.AccountProfile)
                .Include(x=>x.Currency)
                .FirstOrDefaultAsync( x=>x.AccountProfile.UserId == _currentUserService.UserId);
            result.TotalAmount = request.HasWon ? request.Amount + result.TotalAmount : result.TotalAmount - request.Amount;
            if(result.TotalAmount < 0) {
                throw new ValidationException(ResponseMessageConstants.CurrencyLowerThanZero);
            }
            await _accountProfileCurrencyRepository.Update(result);
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(UpdateAccountCurrencyCommandHandler)}");

            return new UpdateAccountCurrencyResponse() {
                Id = result.Id,
                AccountNickname = result.AccountProfile.Nickname,
                TotalAmount = (int)result.TotalAmount,
                Currency = result.Currency.Name
            };
        }
    }
}
