using DevFreela.Application.Models;
using DevFreela.Core.IRepositories;
using DevFreela.Infrastructure.Auth;
using MediatR;

namespace DevFreela.Application.Commands.Users.InsertUser
{
    internal class InsertUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel<int>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public InsertUserHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<ResultViewModel<int>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var hash = _authService.ComputeHash(request.Password);

            request.Password = hash;

            var user = request.ToEntity();

            await _userRepository.Add(user);

            return ResultViewModel<int>.Success(user.Id);
        }
    }
}
