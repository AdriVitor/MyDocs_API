namespace MyDocs.Features.Authentication.Login
{
    public interface ILoginService
    {
        public Task<LoginResponse> GenerateToken(LoginRequest loginRequest);
    }
}
