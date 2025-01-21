using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.Users.InsertUserSkill
{
    public class InsertUserSkillCommand : IRequest<ResultViewModel>
    {
        public int[] SkillIds { get; set; }
        public int IdUser { get; set; }
    }
}
