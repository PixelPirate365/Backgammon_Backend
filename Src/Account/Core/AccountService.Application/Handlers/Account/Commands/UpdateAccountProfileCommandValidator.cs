﻿using FluentValidation;

namespace AccountService.Application.Handlers.Account.Commands
{
    public class UpdateAccountProfileCommandValidator:AbstractValidator<UpdateAccountCommandResponse> {
        public UpdateAccountProfileCommandValidator()
        {
            RuleFor(x => x.Nickname).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Image).NotEmpty();
            RuleFor(x => x.BirthDate).NotEmpty()
                .LessThan(DateTime.UtcNow.AddYears(-16));
            RuleFor(x => x.Gender).NotEmpty().IsInEnum();




        }
    }
}
