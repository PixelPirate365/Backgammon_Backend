using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Persistence.Data;
using AuthService.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace AuthService.Persistence.Tests.Repositories
{
    public class RefreshTokenRepositoryTests {
        private readonly Mock<ICurrentUserService> _currentUserService;
        public RefreshTokenRepositoryTests() {
            _currentUserService = new Mock<ICurrentUserService>();

        }
        [Fact]
        public async Task CreateToken_Successfully() {
            //arrange
            bool result = false;
            var inputPlan = GetTokens().First();

            var options =
                new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "temp_AuthenticationDb").Options;
            //act
            using (var context = new ApplicationDbContext(options, _currentUserService.Object)) {
                context.Database.EnsureDeleted();
                var repository = new Repository<RefreshToken>(context);
                result = await repository.Add(inputPlan);

            }
            //assert
            Assert.True(result);

        }
        [Fact]
        public async Task DeleteToken_Successfully() {
            //arrange
            bool isDeleted = false;
            var inputToken = GetTokens().First();

            var options =
                new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "temp_AuthenticationDb").Options;

            //act
            using (var context =
                new ApplicationDbContext(options, _currentUserService.Object)) {
                context.Database.EnsureDeleted();
                var repository = new Repository<RefreshToken>(context);
                await repository.Add(inputToken);
                isDeleted = await repository.Delete(inputToken);

            }
            //assert
            Assert.True(isDeleted);

        }
        [Fact]
        public async Task GetTokenById_Successfully() {
            //arrange
            var expectedToken = GetTokens().First();

            var inputToken = expectedToken;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "temp_AuthenticationDb").Options;

            using (var context = new ApplicationDbContext(options, _currentUserService.Object)) {
                context.Database.EnsureDeleted();
                var repository = new Repository<RefreshToken>(context);
                await repository.Add(inputToken);
            }

            //act
            RefreshToken actualResult;

            using (var context = new ApplicationDbContext(options, _currentUserService.Object)) {
                var repository = new Repository<RefreshToken>(context);
                actualResult = await repository.TableNoTracking.FirstOrDefaultAsync(x => x.JwtId == expectedToken.JwtId);
            }

            //assert
            Assert.Equal(expectedToken.UserId, actualResult.UserId);
        }
            private List<RefreshToken> GetTokens() {
            var result = new List<RefreshToken>() { new RefreshToken {
            JwtId="cf0088d2-11b3-4530-b6f1-554fcdc0a774",
            UserId="c6f65059-b887-406a-b5bc-df58545b4a4c",
            CreatedDate=DateTime.UtcNow,
            ExpiryDate=DateTime.UtcNow.AddMonths(12),
            },
            new RefreshToken {
            JwtId="9b7713b0-b735-4748-8fdd-47ba7f06a8a5",
            UserId="c6f65059-b887-406a-b5bc-df58545b4a4c",
            CreatedDate=DateTime.UtcNow,
            ExpiryDate=DateTime.UtcNow.AddMonths(12),
            } };
            return result;
        }
    }
}
