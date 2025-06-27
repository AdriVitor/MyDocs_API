using Microsoft.EntityFrameworkCore;
using MyDocs.Infraestructure.Persistence;
using MyDocs.Models;

namespace MyDocs.Shared.Services.UserCredential
{
    public class UserCredentialService : IUserCredentialService
    {
        private readonly Context _context;
        public UserCredentialService(Context context)
        {
            _context = context;
        }

        public async Task<UserCredentials> FindUserCredential(int idUser)
        {
            var userCredential = await _context
                                    .UsersCredentials
                                    .FirstOrDefaultAsync(uc => uc.IdUser == idUser);

            return userCredential;
        }
    }
}
