namespace DevFreela.Application.Commands.Login
{
    public class LoginViewModel
    {
        public LoginViewModel(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
