using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.IRepositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.Users.InsertUserSkill
{
    internal class InsertUserSkillHandler : IRequestHandler<InsertUserSkillCommand, ResultViewModel>
    {

        private readonly DevFreelaDbContext _context;
        private readonly IUserRepository _userRepository;

        public InsertUserSkillHandler(DevFreelaDbContext context, 
            IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<ResultViewModel> Handle(InsertUserSkillCommand request, CancellationToken cancellationToken)
        {
            var userSkills = request.SkillIds
                .Select(s => new UserSkill(request.IdUser, s)).ToList();

            await _userRepository.AddUserSkill(userSkills);

            return ResultViewModel.Success();
        }
    }
}
