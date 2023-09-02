using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Application.Interfaces.Transaction;
using AccountService.Common.Constants;
using AccountService.Common.Utilities;
using AccountService.Domain.Entities;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AccountService.Application.Handlers.CurrencyManagment.Commands.CollectDailyLoginRewards {
    public class CollectDailyLoginRewardCommandHandler : IRequestHandler<CollectDailyLoginRewardCommand, AccountTotalCoinsResponse> {
        readonly ILogger<CollectDailyLoginRewardCommandHandler> _logger;
        readonly IMapper _mapper;
        readonly IRepository<AccountDailyReward> _rewardsRepository;
        readonly IRepository<AccountProfileCurrency> _accountCurrencyRepository;
        readonly IRepository<AccountProfile> _accountProfileRepository;
        readonly IRepository<Currency> _currencyRepository;
        readonly ITransactionService _transactionService;
        public CollectDailyLoginRewardCommandHandler(ILogger<CollectDailyLoginRewardCommandHandler> logger,
           IMapper mapper,
           IRepository<AccountDailyReward> rewardsRepository,
           IRepository<AccountProfileCurrency> accountCurrencyRepository,
           ITransactionService transactionService,
           IRepository<AccountProfile> accountProfileRepository,
           IRepository<Currency> currencyRepository) {
            _logger = logger;
            _mapper = mapper;
            _rewardsRepository = rewardsRepository;
            _accountCurrencyRepository = accountCurrencyRepository;
            _accountProfileRepository = accountProfileRepository;
            _transactionService = transactionService;
            _currencyRepository = currencyRepository;
        }
        //handle seperation
        public async Task<AccountTotalCoinsResponse> Handle(CollectDailyLoginRewardCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(CollectDailyLoginRewardCommandHandler)}");
            if (await IsAlreadyRewarded()) {
                throw new ValidationException(ResponseMessageConstants.DailyCurrencyAlreadyRewarded);
            }
            var accountProfile = await _accountProfileRepository.TableNoTracking.FirstOrDefaultAsync(
                x => x.UserId == Guid.Parse("D895BED5-FB48-42AC-BB26-D1B72324AE44"));

            var result = await _accountCurrencyRepository.Table
                .Include(x => x.AccountProfile)
                .FirstOrDefaultAsync(x => x.AccountProfile.UserId ==
                Guid.Parse("D895BED5-FB48-42AC-BB26-D1B72324AE44"));
            AccountProfileCurrency accountCurrency = new();
            if (result == null) {
                var currentCurrency = await _currencyRepository.TableNoTracking.FirstOrDefaultAsync();
                accountCurrency = new AccountProfileCurrency() {
                    AccountProfileId = accountProfile.Id,
                    CurrencyId = currentCurrency.Id,
                    TotalAmount = RandomGenerator.GetRandomReward()
                };
            }
            else {
                result.TotalAmount += RandomGenerator.GetRandomReward();
            }
            using (var trans = _transactionService.CreateAsyncTransactionScope()) {
                await _rewardsRepository.Add(new AccountDailyReward() {
                    AccountProfileId = accountProfile.Id
                });
                if (result != null) {
                    await _accountCurrencyRepository.Update(result);
                }
                else {
                    await _accountCurrencyRepository.Add(accountCurrency);
                }
                trans.Complete();
            }
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(CollectDailyLoginRewardCommandHandler)}");
            return new AccountTotalCoinsResponse() {
                TotalCoins = result != null ? (int)result.TotalAmount : (int)accountCurrency.TotalAmount
            };
        }
        private async Task<bool> IsAlreadyRewarded() {
            var isAllreadyRewarded = await _rewardsRepository.Table
               .Include(x => x.AccountProfile) //hard coded
               .AnyAsync(x => x.AccountProfile.UserId == Guid.Parse("D895BED5-FB48-42AC-BB26-D1B72324AE44")
               && x.CreatedAt.Date == DateTime.Today);
            return isAllreadyRewarded;
        }
    }
}
