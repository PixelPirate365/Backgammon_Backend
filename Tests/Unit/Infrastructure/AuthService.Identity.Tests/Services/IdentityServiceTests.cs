using AuthService.Domain.Entities;
using AuthService.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace AuthService.Identity.Tests.Services {
    public class IdentityServiceTests {
        private readonly Mock<UserManager<ApplicationUser>> _userManager;
        private readonly IdentityService _identityService;
        public IdentityServiceTests() {
            _userManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            _identityService = new IdentityService(_userManager.Object);
        }
        [Fact]
        public async Task CreateUserAsync_Successfully() {
            //Arrange
            var user = new ApplicationUser() {
                Email = "orher365@gmail.com",
                UserName = "Or123123"
            };
            _userManager.Setup(x => x.CreateAsync(user, "123123Or123")).ReturnsAsync(IdentityResult.Success);
            //Act
            var result = await _identityService.CreateUserAsync(user, "123123Or123");
            //Assert
            Assert.True(result.Succeeded);
        }
        [Fact]
        public async Task CreateUserAsync_Not_Successfully() {
            //Arrange
            var user = new ApplicationUser() {
                Email = "orher365@gmail.com",
                UserName = "Or123123"
            };
            _userManager.Setup(x =>
            x.CreateAsync(user, "1233or")).ReturnsAsync(IdentityResult.Failed());
            //Act
            var result = await _identityService.CreateUserAsync(user, "1233or");
            //Assert
            Assert.False(result.Succeeded);
        }
        [Fact]
        public async Task FindByEmailAsync_Successfully() {
            var validEmail = "orwwwe@gmail.com";
            //Arrange
            _userManager.Setup(x => x.FindByEmailAsync(validEmail)).ReturnsAsync(new ApplicationUser() {
                Email = validEmail,
                UserName = "orwwwe"
            });
            //Act
            var result = await _identityService.FindByEmailAsync(validEmail);
            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task FindByEmailAsync_Not_Successfully() {
            //Arrange
            var fakeEmail = "orwwwghhe@gmail.com";
            _userManager.Setup(x => x.FindByEmailAsync(fakeEmail)).ReturnsAsync(new ApplicationUser());
            //Act
            var result = await _identityService.FindByEmailAsync(fakeEmail);
            //Assert
            Assert.Null(result.Email);
        }
        [Fact]
        public async Task ChangePasswordAsync_Successfully() {
            //Arrange
            var user = new ApplicationUser {
                Email = "orwwwwe@gmail.com",
                UserName = "orwwwe"
            };
            var currentPassword = "990880770Or";
            var newPassword = "990880770Orwwwe";
            _userManager.Setup(x => x.ChangePasswordAsync(user, currentPassword, newPassword)).ReturnsAsync(IdentityResult.Success);
            //Act
            var result = await _identityService.ChangePasswordAsync(user, currentPassword, newPassword);
            //Assert
            Assert.True(result.Succeeded);
        }
        [Fact]
        public async Task ChangePasswordAsync_NotSuccessfully() {
            //Arrange
            var user = new ApplicationUser {
                Email = "orwwwwe@gmail.com",
                UserName = "orwwwe"
            };
            var currentPassword = "990880770Or";
            var newPassword = "990880";
            _userManager.Setup(x => x.ChangePasswordAsync(user, currentPassword, newPassword)).ReturnsAsync(IdentityResult.Failed());
            //Act
            var result = await _identityService.ChangePasswordAsync(user, currentPassword, newPassword);
            //Assert
            Assert.False(result.Succeeded);
        }
        [Fact]
        public async Task CheckPasswordAsync_Successfully() {
            //Arrange
            var user = new ApplicationUser {
                Email = "orwwwwe@gmail.com",
                UserName = "orwwwe"
            };
            var currentPassword = "990880770Or";
            _userManager.Setup(x => x.CheckPasswordAsync(user, currentPassword)).ReturnsAsync(true);
            //Act
            var result = await _identityService.CheckPasswordAsync(user, currentPassword);
            //Assert
            Assert.True(result);
        }
        [Fact]
        public async Task CheckPasswordAsync_NotSuccessfully() {
            //Arrange
            var user = new ApplicationUser {
                Email = "orwwwwe@gmail.com",
                UserName = "orwwwe"
            };
            var currentPassword = "990880770or";
            _userManager.Setup(x => x.CheckPasswordAsync(user, currentPassword)).ReturnsAsync(false);
            //Act
            var result = await _identityService.CheckPasswordAsync(user, currentPassword);
            //Assert
            Assert.False(result);
        }
        [Fact]
        public async Task FindByIdAsync_Successfully() {
            //Arrange
            var user = new ApplicationUser {
                Email = "orwwwwe@gmail.com",
                UserName = "orwwwe"
            };
            Guid guid = new("8bcaff90-0af2-4772-a393-cba1c08f5249");
            _userManager.Setup(x => x.FindByIdAsync(guid.ToString())).ReturnsAsync(user);
            //Act
            var result = await _identityService.FindByIdAsync(guid.ToString());
            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task FindByIdAsync_NotSuccessfully() {
            //Arrange
            var user = new ApplicationUser();

            Guid guid = new("8bcaff90-0af2-4772-a393-cba1c08f5245");
            _userManager.Setup(x => x.FindByIdAsync(guid.ToString())).ReturnsAsync(user);
            //Act
            var result = await _identityService.FindByIdAsync(guid.ToString());
            //Assert
            Assert.Null(result.Email);
        }
    }
}
