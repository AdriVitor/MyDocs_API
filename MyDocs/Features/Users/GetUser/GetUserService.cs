using Microsoft.EntityFrameworkCore;
using MyDocs.Infraestructure.Persistence;
using MyDocs.Models;
using MyDocs.Shared.Services.UserService;

namespace MyDocs.Features.Users.GetUser
{
    public class GetUserService : IGetUserService
    {
        private readonly IUserService _userService;

        public GetUserService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetUserResponse> GetById(GetUserRequest request)
        {
            request.ValidateProperties();

            User user = await _userService.GetUser(request.IdUser);

            return ConvertEntityInResponse(user);
        }

        private GetUserResponse ConvertEntityInResponse(User user)
        {
            return new GetUserResponse
            {
                Id = user.Id,
                Name = user.Name,
                CPF = user.CPF,
                DateOfBirth = user.DateOfBirth,
                Phone = user.Phone,
            };
        }
    }
}
