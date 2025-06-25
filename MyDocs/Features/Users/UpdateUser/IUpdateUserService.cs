namespace MyDocs.Features.Users.UpdateUser
{
    public interface IUpdateUserService
    {
        public Task Update(UpdateUserRequest request);
    }
}
