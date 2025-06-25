using MyDocs.Features.Users.Create;

namespace MyDocs.Features.Users.CreateUser
{
    public interface ICreateUserService
    {
        public Task AddUser(CreateUserRequest request);
    }
}
