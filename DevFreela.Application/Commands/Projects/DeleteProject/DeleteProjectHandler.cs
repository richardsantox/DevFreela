﻿using DevFreela.Application.Models;
using MediatR;
using DevFreela.Core.Repositories;

namespace DevFreela.Application.Commands.Projects.DeleteProject
{
    internal class DeleteProjectHandler : IRequestHandler<DeleteProjectCommnad, ResultViewModel>
    {
        private readonly IProjectRepository _repository;

        public DeleteProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(DeleteProjectCommnad request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.Id);

            if (project is null)
            {
                return ResultViewModel.Error("Projeto não encontrado.");
            }

            project.SetAsDeleted();
            await _repository.Update(project);

            return ResultViewModel.Success();
        }
    }
}
