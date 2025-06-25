using MyDocs.Models;

namespace MyDocs.Shared.Services.UserService
{
    public interface IUserService
    {
        public Task<User> GetUser(int idUser);
    }
}
