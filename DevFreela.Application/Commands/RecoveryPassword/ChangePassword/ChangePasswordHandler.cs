using DevFreela.Application.Models;
using DevFreela.Core.IRepositories;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.Notifications;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace DevFreela.Application.Commands.RecoveryPassword.ChangePassword
{
    internal class ChangePasswordHandler
        : IRequestHandler<ChangePasswordCommand, ResultViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IEmailService _emailService;
        private readonly IAuthService _authService;

        public ChangePasswordHandler(
            IUserRepository userRepository,
            IMemoryCache memoryCache,
            IEmailService emailService,
            IAuthService authService)
        {
            _userRepository = userRepository;
            _memoryCache = memoryCache;
            _emailService = emailService;
            _authService = authService;
        }

        public async Task<ResultViewModel> Handle(
            ChangePasswordCommand request, 
            CancellationToken cancellationToken)
        {
            var cacheKey = $"RecoveryCode:{request.Email}";

            if (!_memoryCache.TryGetValue(cacheKey, out string? code)
                || code != request.Code)
            {
                return ResultViewModel.Error("Codigo inválido.");
            }

            _memoryCache.Remove(cacheKey);

            var user = await _userRepository.GetByEmail(request.Email);

            if (user is null)
                return ResultViewModel.Error("Usuário não encontrado.");

            var hash = _authService.ComputeHash(request.NewPassword);

            user.UpdatePassword(hash);
            await _userRepository.SaveChanges();

            return ResultViewModel.Success();
        }
    }
}
