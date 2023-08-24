using FluentValidation;

namespace AuthService.Application.Handlers.User.Queries.AuthenticateUser {
    public class AuthenticateUserQueryValidator : AbstractValidator<AuthenticateUserQuery> {
        public AuthenticateUserQueryValidator() {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        }
    }
}
