using DevFreela.Application.Models;
using DevFreela.Core.IRepositories;
using DevFreela.Infrastructure.Notifications;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace DevFreela.Application.Commands.RecoveryPassword.ValidateRecoveryCode
{
    internal class ValidateRecoveryCodeHandler :
        IRequestHandler<ValidateRecoveryCodeCommand, ResultViewModel>
    {
        private readonly IMemoryCache _memoryCache;

        public ValidateRecoveryCodeHandler(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<ResultViewModel> Handle(
            ValidateRecoveryCodeCommand request, 
            CancellationToken cancellationToken)
        {
            var cacheKey = $"RecoveryCode:{request.Email}";

            if (!_memoryCache.TryGetValue(cacheKey, out string? code) 
                || code != request.Code)
            {
                return ResultViewModel.Error("Codigo inválido.");
            }

            return ResultViewModel.Success();
        }
    }
}
