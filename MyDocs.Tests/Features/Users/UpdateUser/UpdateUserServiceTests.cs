using Moq;
using MyDocs.Features.Users.UpdateUser;
using MyDocs.Shared.Services.UserService;
using MyDocs.Tests.Shared;

namespace MyDocs.Tests.Features.Users.UpdateUser
{
    public class UpdateUserServiceTests
    {
        [Fact]
        public async Task UpdateUser_ShouldThrowArgumentNullException_WhenUserIsNull()
        {
            using var context = MemoryDatabase.Create();

            var userServiceMock = new Mock<IUserService>();
            var userId = 99;

            userServiceMock.Setup(x => x.GetUser(userId))
                .ThrowsAsync(new ArgumentNullException("Usuário não encontrado"));

            var service = new UpdateUserService(context, userServiceMock.Object);

            var request = new UpdateUserRequest
            {
                IdUser = userId,
                Name = "Novo Nome",
                CPF = "11111111111",
                DateOfBirth = new DateTime(1990, 1, 1),
                Phone = "999999999"
            };

            await Assert.ThrowsAsync<ArgumentNullException>(() => service.Update(request));
        }

        [Fact]
        public async Task UpdateUser_ShouldUpdateUser_WhenUserExists()
        {
            using var context = MemoryDatabase.Create();

            int userId = 3;
            var existingUser = GenerateModelsService.CreateUser(userId, "Nome Antigo", "00000000000", new DateTime(1985, 5, 5), "11988888888");

            context.Users.Add(existingUser);
            await context.SaveChangesAsync();

            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.GetUser(userId))
                .ReturnsAsync(existingUser);

            var service = new UpdateUserService(context, userServiceMock.Object);

            var request = new UpdateUserRequest
            {
                IdUser = userId,
                Name = "Nome Atualizado",
                CPF = "11111111111",
                DateOfBirth = new DateTime(1990, 1, 1),
                Phone = "11999999999"
            };

            await service.Update(request);

            var updatedUser = await context.Users.FindAsync(userId);
            Assert.NotNull(updatedUser);
            Assert.Equal("Nome Atualizado", updatedUser.Name);
            Assert.Equal("11111111111", updatedUser.CPF);
            Assert.Equal(new DateTime(1990, 1, 1), updatedUser.DateOfBirth);
            Assert.Equal("11999999999", updatedUser.Phone);
        }

    }
}
