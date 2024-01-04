using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Common.Constants;
using AccountService.Domain.Entities;
using AccountService.MessageBus.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AccountService.Application.Handlers.Account.Commands.DeleteAccount {
    internal class DeleteAccountProfileCommandHandler : IRequestHandler<DeleteAccountProfileCommand, bool> {
        readonly ILogger<DeleteAccountProfileCommandHandler> _logger;
        readonly IRepository<AccountProfile> _repository;
        readonly IRabbitMQMessageSender _messageSender;
        public DeleteAccountProfileCommandHandler(
            ILogger<DeleteAccountProfileCommandHandler> logger,
            IRepository<AccountProfile> repository,
            IRabbitMQMessageSender messageSender) {
            _logger = logger;
            _repository = repository;
            _messageSender = messageSender;
        }
        public async Task<bool> Handle(DeleteAccountProfileCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(DeleteAccountProfileCommandHandler)}");
            
            var account = await _repository.Table.FirstOrDefaultAsync(x=>x.UserId == request.UserId);
            var result = await _repository.Delete(account);
            _messageSender.SendMessage(request.UserId, EventNameConstants.PlayerDeletionEvent);
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(DeleteAccountProfileCommandHandler)}");

            return result;
        }
    }
}
