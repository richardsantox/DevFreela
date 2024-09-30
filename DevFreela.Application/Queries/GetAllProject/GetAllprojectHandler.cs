using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllProject
{
    public class GetAllprojectHandler : IRequestHandler<GetAllProjectQuery, ResultViewModel<List<ProjectItemViewModel>>>
    {
        private readonly DevFreelaDbContext _context;

        public GetAllprojectHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetAllProjectQuery request, CancellationToken cancellationToken)
        {
            var projects = await _context.Projects
              .Include(p => p.Client)
              .Include(p => p.Freelancer)
              .Where(p => !p.IsDeleted && request.Seach == ""
                  || p.Title.Contains(request.Seach)
                  || p.Description.Contains(request.Seach))
              .Skip(request.Page * request.Size)
              .Take(request.Size)
              .ToListAsync();

            var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

            return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
        }
    }
}
