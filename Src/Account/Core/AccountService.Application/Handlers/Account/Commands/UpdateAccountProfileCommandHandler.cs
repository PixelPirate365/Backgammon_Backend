using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Application.Handlers.Account.Queries;
using AccountService.Common.Helpers;
using AccountService.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AccountService.Application.Handlers.Account.Commands {
    public class UpdateAccountProfileCommandHandler : IRequestHandler<UpdateAccountProfileCommand, UpdateAccountCommandResponse> {

        readonly ILogger<UpdateAccountProfileCommandHandler> _logger;
        readonly IMapper _mapper;
        readonly IRepository<AccountProfile> _repository;
        public UpdateAccountProfileCommandHandler(ILogger<UpdateAccountProfileCommandHandler> logger,
            IMapper mapper, IRepository<AccountProfile> repository) {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }
        
        public async Task<UpdateAccountCommandResponse> Handle(UpdateAccountProfileCommand request,
            CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(GetAccountProfileQueryHandler)}");
            var accountProfile = await _repository.Table.FirstOrDefaultAsync();
            var mapAccountProfile = _mapper.Map<AccountProfile>(accountProfile);
            accountProfile.Image = ImageHelper.SaveImage(request.Image);

            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(GetAccountProfileQueryHandler)}");
            throw new NotImplementedException();
        }
    }
}
