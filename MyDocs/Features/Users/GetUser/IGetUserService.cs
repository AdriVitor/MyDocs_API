namespace MyDocs.Features.Users.GetUser
{
    public interface IGetUserService
    {
        public Task<GetUserResponse> GetById(GetUserRequest request);
    }
}
