using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Application.Interfaces;
using AccountService.Common.Enums;
using AccountService.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AccountService.Application.Handlers.FriendRequests.Queries.GetFriendsRequest {
    public class GetFriendsRequestQueryHandler : IRequestHandler<GetFriendsRequestQuery, List<GetFriendRequestResponse>> {

        readonly ILogger<GetFriendsRequestQueryHandler> _logger;
        readonly IRepository<FriendRequest> _repository;
        readonly IMapper _mapper;
        readonly ICurrentUserService _currentUserService;
        readonly IRepository<AccountProfile> _profileRepository;
        public GetFriendsRequestQueryHandler(
            ILogger<GetFriendsRequestQueryHandler> logger,
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

        public async Task<List<GetFriendRequestResponse>> Handle(GetFriendsRequestQuery request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(GetFriendsRequestQueryHandler)}");
            var currentProfile = _profileRepository.TableNoTracking.FirstOrDefault(x => x.UserId == _currentUserService.UserId);
            var result = await _repository.TableNoTracking.Include(x => x.SenderProfile)
                .Where(x => 
                x.RecieverProfileId == currentProfile.Id
                && x.Status == (int)FriendRequestStatusEnum.Pending)
                .ProjectTo<GetFriendRequestResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(GetFriendsRequestQueryHandler)}");
            return result;
        }
    }
}
