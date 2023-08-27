using AuthService.Application.Handlers.User.Queries.AuthenticateUser;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AuthService.Application.Tests.Handlers.User.Queries.AuthenticateUser {
    public class AuthenticateUserQueryValidatorTests {
        private readonly AuthenticateUserQueryValidator _queryValidator;
        public AuthenticateUserQueryValidatorTests()
        {
            _queryValidator = new AuthenticateUserQueryValidator();
        }
        [Fact]
        public void AuthenticateUserQuery_ValidQuery() {
            //Arrange
            var query = new AuthenticateUserQuery() {
                Email="test@gmail.com",
                Password="Testing123",      
            };
            //Act
            var result = _queryValidator.TestValidate(query);
            //Assert
            Assert.True(result.IsValid);
        }
        [Fact]
        public void AuthenticateUserQuery_InvalidQuery() {
            //Arrange
            var query = new AuthenticateUserQuery() {
                Email = "testemail",
                Password = "Test12345"
            };
            //Act
            var result = _queryValidator.TestValidate(query);
            //Assert

            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count > 0);
        }

    }
}
