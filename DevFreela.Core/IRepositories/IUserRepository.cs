﻿using DevFreela.Core.Entities;

namespace DevFreela.Core.IRepositories
{
    public interface IUserRepository
    {
        public Task<User?> GetById(int id);
        public Task<int> Add(User user);
        public Task AddUserSkill(List<UserSkill> userSkills);
        public Task<User?> GetByLogin(string email, string password);
        public Task<User?> GetByEmail(string email);
        public Task SaveChanges();
    }
}
