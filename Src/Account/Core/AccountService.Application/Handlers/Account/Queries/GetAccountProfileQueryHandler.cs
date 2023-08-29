using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AccountService.Application.Handlers.Account.Queries {
    public class GetAccountProfileQueryHandler : IRequestHandler<GetAccountProfileQuery, GetAccountProfileResponse> {
        readonly ILogger<GetAccountProfileQueryHandler> _logger;
        readonly IRepository<AccountProfile> _repository;
        private readonly IMapper _mapper;
        public GetAccountProfileQueryHandler(ILogger<GetAccountProfileQueryHandler> logger,
            IRepository<AccountProfile> repository,
            IMapper mapper) {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<GetAccountProfileResponse> Handle(GetAccountProfileQuery request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(GetAccountProfileQueryHandler)}");
            var result = await _repository.TableNoTracking.ProjectTo<GetAccountProfileResponse>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(GetAccountProfileQueryHandler)}");
            return result;
        }
    }
}
