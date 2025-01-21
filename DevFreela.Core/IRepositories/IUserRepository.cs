using DevFreela.Core.Entities;

namespace DevFreela.Core.IRepositories
{
    public interface IUserRepository
    {
        public Task<User?> GetById(int id);
        public Task<int> Add(User user);
        public Task AddUserSkill(List<UserSkill> userSkills);
    }
}
