using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.Login
{
    public class LoginCommand : IRequest<ResultViewModel<LoginViewModel>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
