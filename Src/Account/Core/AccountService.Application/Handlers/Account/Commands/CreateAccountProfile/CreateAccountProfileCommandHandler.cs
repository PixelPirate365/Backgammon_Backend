using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Application.Handlers.Account.Queries.GetAccountProfile;
using AccountService.Common.Constants;
using AccountService.Common.Dtos;
using AccountService.Domain.Entities;
using AccountService.MessageBus.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AccountService.Application.Handlers.Account.Commands.CreateAccountProfile {
    internal class CreateAccountProfileCommandHandler : IRequestHandler<CreateAccountProfileCommand, Unit> {
        readonly ILogger<CreateAccountProfileCommandHandler> _logger;
        readonly IRepository<AccountProfile> _repository;
        readonly IMapper _mapper;
        readonly IRabbitMQMessageSender _rabbitMQMessageSender;
        public CreateAccountProfileCommandHandler(
            ILogger<CreateAccountProfileCommandHandler> logger,
            IRepository<AccountProfile> repository,
            IMapper mapper,
            IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _rabbitMQMessageSender = rabbitMQMessageSender;
        }
        public async Task<Unit> Handle(CreateAccountProfileCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(GetAccountProfileQueryHandler)}");
            var accountProfile = _mapper.Map<AccountProfile>(request);
            await _repository.Add(accountProfile);
            _rabbitMQMessageSender.SendMessage(new PlayerInfoDto { AccountId = accountProfile.Id , UserId = accountProfile.UserId }, EventNameConstants.PlayerCreationEvent);
            
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(GetAccountProfileQueryHandler)}");
            return Unit.Value;
        }
    }
}
