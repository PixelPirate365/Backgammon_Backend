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
        readonly IRepository<AccountProfile> _profileRepository;
        readonly IMapper _mapper;
        readonly ICurrentUserService _currentUserService;
        public SendFriendRequestCommandHandler(
            ILogger<SendFriendRequestCommandHandler> logger,
            IMapper mapper,
            IRepository<FriendRequest> repository,
            ICurrentUserService currentUserService,
            IRepository<AccountProfile> profileRepository) {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
            _currentUserService = currentUserService;
            _profileRepository = profileRepository;

        }
        public async Task<bool> Handle(SendFriendRequestCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(SendFriendRequestCommandHandler)}");
            var friendRequest = _mapper.Map<FriendRequest>(request);
            var currentProfile = _profileRepository.TableNoTracking.FirstOrDefault(x => x.UserId == _currentUserService.UserId);
            friendRequest.SenderProfileId = currentProfile.Id;
            await _repository.Add(friendRequest);
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(SendFriendRequestCommandHandler)}");

            return true;
        }
    }
}
