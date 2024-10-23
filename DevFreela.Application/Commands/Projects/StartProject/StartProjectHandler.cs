using DevFreela.Application.Models;
using MediatR;
using DevFreela.Core.Repositories;

namespace DevFreela.Application.Commands.Projects.StartProject
{
    internal class StartProjectHandler : IRequestHandler<StartProjectCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;

        public StartProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.Id);

            if (project is null)
            {
                return ResultViewModel.Error("Projeto não encontrado.");
            }

            project.Start();
            await _repository.Update(project);

            return ResultViewModel.Success();
        }
    }
}
