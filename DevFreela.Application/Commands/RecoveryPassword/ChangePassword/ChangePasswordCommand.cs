using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.RecoveryPassword.ChangePassword
{
    public class ChangePasswordCommand : IRequest<ResultViewModel>
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string NewPassword { get; set; }
    }
}
