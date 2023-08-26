using AuthService.Domain.Entities;
using AuthService.Identity.Models;
using AuthService.Identity.Services;
using AuthService.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json.Linq;
using Xunit;

namespace AuthService.Identity.Tests.Services {
    public class TokenServiceTests {
        private readonly Mock<UserManager<ApplicationUser>> _userManager;
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly TokenService _tokenService;
        private readonly Mock<Repository<RefreshToken>> _refreshTokenRepository;
        public TokenServiceTests() {
            _userManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            _jwtSettings = Options.Create(new JwtSettings {
                Secret = "123123123124152312349214215342657fxgbXFsF32RASFSADF4R23",
                TokenLifeTime = TimeSpan.FromMinutes(50)
            });
            _refreshTokenRepository = new Mock<Repository<RefreshToken>>();
            _tokenService = new TokenService(_jwtSettings!,  _refreshTokenRepository.Object);
        }
        [Fact]
        public async Task GenerateTokenAsync_Successfully() {
            //Arange
            var user = new ApplicationUser { Id = "testId", UserName = "testUsername" };
            var refreshToken = new RefreshToken {
                JwtId = "cf0088d2-11b3-4530-b6f1-554fcdc0a774",
                UserId = "c6f65059-b887-406a-b5bc-df58545b4a4c",
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };
            _refreshTokenRepository.Setup(x => x.Add(refreshToken)).ReturnsAsync(true);

            //Act
            var result = await _tokenService.GenerateUserTokenAsync(user);

            //Asert
            Assert.NotNull(result);
        }
    }
}
