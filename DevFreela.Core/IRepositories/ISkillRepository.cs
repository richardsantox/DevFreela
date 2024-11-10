using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface ISkillRepository
    {
        Task<List<Skill>> GetAll(string search, int page, int size);
        Task<int> Add(Skill skill);
    }
}
