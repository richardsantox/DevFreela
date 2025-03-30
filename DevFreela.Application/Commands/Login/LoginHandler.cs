using DevFreela.Application.Models;
using DevFreela.Core.IRepositories;
using DevFreela.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DevFreela.Application.Commands.Login
{
    public class LoginHandler 
        : IRequestHandler<LoginCommand, ResultViewModel<LoginViewModel>>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public LoginHandler(
            IAuthService authService, 
            IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<ResultViewModel<LoginViewModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var hash = _authService.ComputeHash(request.Password);

            var user = await _userRepository.GetByLogin(request.Email, hash);

            if (user is null)
                return ResultViewModel<LoginViewModel>
                    .Error("Erro de login.");

            var token = _authService.GenerateToken(user.Email, user.Role);

            return ResultViewModel<LoginViewModel>
                .Success(new LoginViewModel(token));
        }
    }
}
