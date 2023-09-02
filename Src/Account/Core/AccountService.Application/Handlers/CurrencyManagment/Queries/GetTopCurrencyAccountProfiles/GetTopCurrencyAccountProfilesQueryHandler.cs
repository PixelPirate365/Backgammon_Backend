using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Application.Handlers.CurrencyManagment.Commands.CollectDailyLoginRewards;
using AccountService.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AccountService.Application.Handlers.CurrencyManagment.Queries.GetTopCurrencyAccountProfiles {
    public class GetTopCurrencyAccountProfilesQueryHandler : IRequestHandler<GetTopCurrencyAccountProfilesQuery, List<GetTopCurrencyAccountProfileResponse>> {
        readonly ILogger<CollectDailyLoginRewardCommandHandler> _logger;
        readonly IMapper _mapper;
        readonly IRepository<AccountProfileCurrency> _accountProfileRepository;
        public GetTopCurrencyAccountProfilesQueryHandler(
            ILogger<CollectDailyLoginRewardCommandHandler> logger,
            IMapper mapper,
            IRepository<AccountProfileCurrency> accountProfileRepository
            ) {
            _accountProfileRepository = accountProfileRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<List<GetTopCurrencyAccountProfileResponse>> Handle(GetTopCurrencyAccountProfilesQuery request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(GetTopCurrencyAccountProfilesQueryHandler)}");
            var result = await _accountProfileRepository.TableNoTracking.Include(
                x => x.AccountProfile).Include(
                x => x.Currency)
                .OrderByDescending
                (x => x.TotalAmount)
                .ProjectTo<GetTopCurrencyAccountProfileResponse>(_mapper.ConfigurationProvider)
                .Take(request.MaximumNumberOfProfiles)
                .ToListAsync();

            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(GetTopCurrencyAccountProfilesQueryHandler)}");
            return result;
        }
    }
}
