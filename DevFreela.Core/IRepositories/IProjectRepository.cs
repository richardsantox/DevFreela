﻿using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAll(string search, int page, int size);
        Task<Project?> GetDetailsById(int id);
        Task<Project?> GetById(int id);
        Task<int> Add(Project project);
        Task Update(Project project);
        Task AddComment(ProjectComment comment);
        Task<bool> Exist(int id);
    }
}
