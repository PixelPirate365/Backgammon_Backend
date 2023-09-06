using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AccountService.Application.Handlers.Account.Queries.GetAccountProfile
{
    public class GetAccountProfileQueryHandler : IRequestHandler<GetAccountProfileQuery, GetAccountProfileResponse>
    {
        readonly ILogger<GetAccountProfileQueryHandler> _logger;
        readonly IRepository<AccountProfile> _repository;
        readonly IMapper _mapper;
        readonly ICurrentUserService _currentUserService;

        public GetAccountProfileQueryHandler(ILogger<GetAccountProfileQueryHandler> logger,
            IRepository<AccountProfile> repository,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public async Task<GetAccountProfileResponse> Handle(GetAccountProfileQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(GetAccountProfileQueryHandler)}");
            var result = await _repository.TableNoTracking
                .Where(x=>x.UserId == _currentUserService.UserId)
                .ProjectTo<GetAccountProfileResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(GetAccountProfileQueryHandler)}");
            return result;
        }
    }
}
