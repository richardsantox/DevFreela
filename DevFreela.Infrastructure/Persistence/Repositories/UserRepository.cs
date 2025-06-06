﻿using DevFreela.Core.Entities;
using DevFreela.Core.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _context;

        public UserRepository(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users
                .Include(u => u.Skills)
                    .ThenInclude(s => s.Skill)
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<int> Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task AddUserSkill(List<UserSkill> userSkills)
        {
            await _context.AddRangeAsync(userSkills);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByLogin(string email, string password)
        {
            return await _context.Users
                .SingleOrDefaultAsync(u => 
                    u.Email == email && u.Password == password);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users
                .SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        } 
    }
}
