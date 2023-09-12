using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Application.Interfaces;
using AccountService.Common.Enums;
using AccountService.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AccountService.Application.Handlers.Friends.Queries.GetFriends {
    public class GetFriendsQueryHandler : IRequestHandler<GetFriendsQuery, GetFriendResponse> {

        readonly ILogger<GetFriendsQueryHandler> _logger;
        readonly ICurrentUserService _currentUserService;
        readonly IRepository<FriendRequest> _friendRequestRepository;
        readonly IRepository<AccountProfile> _accountProfileRepository;
        readonly IMapper _mapper;
        public GetFriendsQueryHandler(
            ILogger<GetFriendsQueryHandler> logger,
            ICurrentUserService currentUserService,
            IRepository<FriendRequest> friendRequestRepository,
            IRepository<AccountProfile> accountProfileRepository,
            IMapper mapper) {
            _logger = logger;
            _currentUserService = currentUserService;
            _friendRequestRepository = friendRequestRepository;
            _accountProfileRepository = accountProfileRepository;
            _mapper = mapper;

        }

        public async Task<GetFriendResponse> Handle(GetFriendsQuery request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(GetFriendsQueryHandler)}");
            var onlineFriendIds = await _friendRequestRepository.Table.Where(x =>
            x.SenderProfile.UserId == _currentUserService.UserId && x.Status == (int)FriendRequestStatusEnum.Accepted)
                .Select(x => x.RecieverProfile.UserId).ToListAsync();
            var accountProfile = await _accountProfileRepository.TableNoTracking.Where(x => x.UserId == _currentUserService.UserId)
                .ProjectTo<GetLoggedInProfile>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
                
            var result = new GetFriendResponse() {
                OnlineFriendIds = onlineFriendIds,
                GetLoggedInProfile = accountProfile
            };
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(GetFriendsQueryHandler)}");
            return result;
        }
    }
}
