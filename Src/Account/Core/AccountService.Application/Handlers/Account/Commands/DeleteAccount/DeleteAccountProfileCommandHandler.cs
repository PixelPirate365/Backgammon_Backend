using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AccountService.Application.Handlers.Account.Commands.DeleteAccount {
    internal class DeleteAccountProfileCommandHandler : IRequestHandler<DeleteAccountProfileCommand, bool> {
        readonly ILogger<DeleteAccountProfileCommandHandler> _logger;
        readonly IRepository<AccountProfile> _repository;
        public DeleteAccountProfileCommandHandler(
            ILogger<DeleteAccountProfileCommandHandler> logger,
            IRepository<AccountProfile> repository) {
            _logger = logger;
            _repository = repository;
        }
        public async Task<bool> Handle(DeleteAccountProfileCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(DeleteAccountProfileCommandHandler)}");
            
            var account = await _repository.Table.FirstOrDefaultAsync(x=>x.UserId == request.UserId);
            var result = await _repository.Delete(account);
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(DeleteAccountProfileCommandHandler)}");

            return result;
        }
    }
}
