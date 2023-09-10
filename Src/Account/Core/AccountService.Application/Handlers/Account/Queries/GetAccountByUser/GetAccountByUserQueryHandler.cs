using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Application.Handlers.Account.Queries.GetAccountProfile;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.Account.Queries.GetAccountByUser {
    public class GetAccountByUserQueryHandler : IRequestHandler<GetAccountByUserQuery, GetAccountByUserResponse> {

        readonly ILogger<GetAccountByUserQueryHandler> _logger;
        readonly IRepository<AccountProfile> _repository;
        readonly IMapper _mapper;

        public GetAccountByUserQueryHandler(ILogger<GetAccountByUserQueryHandler> logger,
            IRepository<AccountProfile> repository,
            IMapper mapper) {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }



        public async Task<GetAccountByUserResponse> Handle(GetAccountByUserQuery request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(GetAccountByUserQueryHandler)}");
            var result = await _repository.TableNoTracking
                .Where(x => x.UserId == request.UserId)
                .ProjectTo<GetAccountByUserResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(GetAccountByUserQueryHandler)}");
            return result;
        }
    }
}
