using MyDocs.Models;

namespace MyDocs.Shared.Services.UserCredential
{
    public interface IUserCredentialService
    {
        public Task<UserCredentials> FindUserCredential(int idUser);
    }
}
