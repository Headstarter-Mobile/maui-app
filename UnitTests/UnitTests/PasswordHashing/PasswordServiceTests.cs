using Headstarter.Services;
using Xunit;

namespace UnitTests.UnitTests.PasswordHashing
{
    public class PasswordServiceTests
    {
        [Fact]
        public async Task ShouldGenerateDifferentPasswordHashesForTheSamePassword()
        {
            // Arrange
            var passwordService = PasswordService.Instance;
            var password = "password";

            // Act
            var hashedPassword1 = await passwordService.Hash(password);
            var hashedPassword2 = await passwordService.Hash(password);

            // Assert
            Assert.NotEqual(hashedPassword1, hashedPassword2);
        }
        [Fact]
        public async Task ShouldCorrectlyVerifyThatTwoDiffferentHashesOfTheSamePasswordAreTheSamePassword()
        {
            // Arrange
            var passwordService = PasswordService.Instance;
            var password = "password";
            var hashedPassword1 = await passwordService.Hash(password);
            var hashedPassword2 = await passwordService.Hash(password);

            // Act
            var result1 = await passwordService.VerifyPassword(password, hashedPassword1);
            var result2 = await passwordService.VerifyPassword(password, hashedPassword2);

            // Assert
            Assert.True(result1);
            Assert.True(result2);
        }
    }
}
