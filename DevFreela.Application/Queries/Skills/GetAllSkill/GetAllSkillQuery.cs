using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.Skills.GetAllSkill
{
    public class GetAllSkillQuery : IRequest<ResultViewModel<List<SkillViewModel>>>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string Seach { get; set; }

        public GetAllSkillQuery(int page, int size, string seach)
        {
            Page = page;
            Size = size;
            Seach = seach;
        }
    }
}
