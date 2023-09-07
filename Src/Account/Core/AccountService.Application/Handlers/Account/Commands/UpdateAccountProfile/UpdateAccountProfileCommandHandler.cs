﻿using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Application.Handlers.Account.Queries.GetAccountProfile;
using AccountService.Application.Interfaces;
using AccountService.Application.Interfaces.Transaction;
using AccountService.Common.Helpers;
using AccountService.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AccountService.Application.Handlers.Account.Commands.UpdateAccountProfile
{
    public class UpdateAccountProfileCommandHandler : IRequestHandler<UpdateAccountProfileCommand, UpdateAccountResponse>
    {

        readonly ILogger<UpdateAccountProfileCommandHandler> _logger;
        readonly IMapper _mapper;
        readonly IRepository<AccountProfile> _repository;
        readonly ITransactionService _transactionService;
        readonly ICurrentUserService _currentUserService;
        public UpdateAccountProfileCommandHandler(ILogger<UpdateAccountProfileCommandHandler> logger,
            IMapper mapper,
            IRepository<AccountProfile> repository,
            ITransactionService transactionService,
            ICurrentUserService currentUserService)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
            _transactionService = transactionService;
            _currentUserService = currentUserService;
        }

        public async Task<UpdateAccountResponse> Handle(UpdateAccountProfileCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(GetAccountProfileQueryHandler)}");
            var accountProfile = await _repository.Table.FirstOrDefaultAsync(x=>x.UserId == _currentUserService.UserId);
            using (var fileTrans = _transactionService.CreateAsyncTransactionScope())
            {
                var mapAccountProfile = _mapper.Map<AccountProfile>(accountProfile);
                if (mapAccountProfile.Image != null)
                {
                    ImageHelper.DeleteImage(mapAccountProfile.Image);
                }
                accountProfile.Image = ImageHelper.SaveImage(request.Image);
                using (var trans = _transactionService.CreateAsyncTransactionScope())
                {
                    await _repository.Update(accountProfile);
                    trans.Complete();
                }
                fileTrans.Complete();
            }
            var result = _mapper.Map<UpdateAccountResponse>(request);
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(GetAccountProfileQueryHandler)}");
            return result;
        }
    }
}
