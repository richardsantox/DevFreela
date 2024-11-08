using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.Skills.GetAllSkill
{
    internal class GetAllSkillHandler : IRequestHandler<GetAllSkillQuery, ResultViewModel<List<SkillViewModel>>>
    {
        private readonly ISkillRepository _skillRepository;

        public GetAllSkillHandler(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async Task<ResultViewModel<List<SkillViewModel>>> Handle(GetAllSkillQuery request, CancellationToken cancellationToken)
        {
            var skills = await _skillRepository.GetAll(request.Seach, request.Page, request.Size);

            var model = skills.Select(SkillViewModel.FromEntity).ToList();

            return ResultViewModel<List<SkillViewModel>>.Success(model);
        }
    }
}
