using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Application.Handlers.Account.Queries.GetAccountProfile;
using AccountService.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AccountService.Application.Handlers.Account.Commands.CreateAccountProfile {
    internal class CreateAccountProfileCommandHandler : IRequestHandler<CreateAccountProfileCommand, Unit> {
        readonly ILogger<CreateAccountProfileCommandHandler> _logger;
        readonly IRepository<AccountProfile> _repository;
        readonly IMapper _mapper;
        public CreateAccountProfileCommandHandler(
            ILogger<CreateAccountProfileCommandHandler> logger,
            IRepository<AccountProfile> repository,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateAccountProfileCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(GetAccountProfileQueryHandler)}");
            var accountProfile = _mapper.Map<AccountProfile>(request);
            await _repository.Add(accountProfile);
            
            
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(GetAccountProfileQueryHandler)}");
            return Unit.Value;
        }
    }
}
