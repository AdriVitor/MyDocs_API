using Microsoft.EntityFrameworkCore;
using MyDocs.Infraestructure.Persistence;
using MyDocs.Models;

namespace MyDocs.Shared.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly Context _context;
        public UserService(Context context)
        {
            _context = context;
        }

        public async Task<User> GetUser(int idUser)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == idUser);

            return user ?? throw new ArgumentNullException("Usuário não encontrado"); ;
        }
    }
}
