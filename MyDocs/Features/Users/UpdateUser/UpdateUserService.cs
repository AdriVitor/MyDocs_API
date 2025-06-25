using MyDocs.Infraestructure.Persistence;
using MyDocs.Models;
using MyDocs.Shared.Services.UserService;

namespace MyDocs.Features.Users.UpdateUser
{
    public class UpdateUserService : IUpdateUserService
    {
        private readonly Context _context;
        private readonly IUserService _userService;
        public UpdateUserService(Context context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task Update(UpdateUserRequest request)
        {
            request.ValidateProperties();

            User user = await _userService.GetUser(request.IdUser);

            user.Id = request.IdUser;
            user.Name = request.Name;
            user.CPF = request.CPF;
            user.DateOfBirth = request.DateOfBirth;
            user.Phone = request.Phone;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
