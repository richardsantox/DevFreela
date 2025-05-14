using DevFreela.Application.Commands.Login;
using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.RecoveryPassword.RequestRecoveryPassword
{
    public class PasswordRecoveryRequestCommand : IRequest<ResultViewModel>
    {
        public string Email { get; set; }
    }
}
