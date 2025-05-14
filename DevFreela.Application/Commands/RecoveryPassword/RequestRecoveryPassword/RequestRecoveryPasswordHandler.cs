using DevFreela.Infrastructure.Notifications;
using DevFreela.Application.Models;
using DevFreela.Core.IRepositories;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace DevFreela.Application.Commands.RecoveryPassword.RequestRecoveryPassword
{
    internal class RequestRecoveryPasswordHandler :
        IRequestHandler<PasswordRecoveryRequestCommand, ResultViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IEmailService _emailService;

        public RequestRecoveryPasswordHandler(
            IUserRepository userRepository, 
            IMemoryCache memoryCache, 
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _memoryCache = memoryCache;
            _emailService = emailService;
        }

        public async Task<ResultViewModel> Handle(PasswordRecoveryRequestCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(request.Email);

            if (user is null)
                return ResultViewModel.Error("Usuário não encontrado.");

            var code = new Random().Next(100000, 999999).ToString();

            var cacheKey = $"RecoveryCode:{request.Email}";

            _memoryCache.Set(cacheKey, code, TimeSpan.FromMinutes(10));

            await _emailService.SendAsync(user.Email, "Código de Recuperação",
                $"Seu Código de recuperação é: {code}");

            return ResultViewModel.Success();
        }
    }
}
