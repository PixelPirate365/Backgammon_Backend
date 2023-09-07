using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AccountService.Application.Handlers.FriendRequests.Commands.SendFriendRequest {
    public class SendFriendRequestCommandHandler : IRequestHandler<SendFriendRequestCommand, bool> {
        readonly ILogger<SendFriendRequestCommandHandler> _logger;
        readonly IRepository<FriendRequest> _repository;
        readonly IMapper _mapper;
        readonly ICurrentUserService _currentUserService;
        public SendFriendRequestCommandHandler(
            ILogger<SendFriendRequestCommandHandler> logger,
            IMapper mapper,
            IRepository<FriendRequest> repository,
            ICurrentUserService currentUserService) {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
            _currentUserService = currentUserService;

        }
        public async Task<bool> Handle(SendFriendRequestCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(SendFriendRequestCommandHandler)}");
            var friendRequest = _mapper.Map<FriendRequest>(request);
            friendRequest.SenderProfileId = _currentUserService.UserId ?? Guid.Empty;
            await _repository.Add(friendRequest);
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(SendFriendRequestCommandHandler)}");

            return true;
        }
    }
}
