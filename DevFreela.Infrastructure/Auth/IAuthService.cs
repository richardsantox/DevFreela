namespace DevFreela.Infrastructure.Auth
{
    internal interface IAuthService
    {
        string ComputeHash(string password);
        string GenerateToken(string email, string role);
    }
}
