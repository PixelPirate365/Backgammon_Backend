using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Application.Handlers.FriendRequests.Commands.SendFriendRequest;
using AccountService.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AccountService.Application.Handlers.FriendRequests.Commands.DeleteFriend {
    public class DeleteFriendCommandHandler : IRequestHandler<DeleteFriendCommand, bool> {
        readonly ILogger<DeleteFriendCommandHandler> _logger;
        readonly IRepository<FriendRequest> _repository;
        readonly IMapper _mapper;
        public DeleteFriendCommandHandler(ILogger<DeleteFriendCommandHandler> logger,
            IMapper mapper,
            IRepository<FriendRequest> repository) {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<bool> Handle(DeleteFriendCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(DeleteFriendCommandHandler)}");
            var friend = await _repository.TableNoTracking.FirstOrDefaultAsync(f => f.Id == request.Id);
            await _repository.Delete(friend);
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(DeleteFriendCommandHandler)}");
            return true;
        }
    }
}
