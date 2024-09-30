using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProject
{
    public class GetAllProjectQuery : IRequest<ResultViewModel<List<ProjectItemViewModel>>>
    {
        public GetAllProjectQuery(int page, int size, string seach)
        {
            Page = page;
            Size = size;
            Seach = seach;
        }

        public int Page { get; set; }
        public int Size { get; set; }
        public string Seach { get; set; }
    }
}
