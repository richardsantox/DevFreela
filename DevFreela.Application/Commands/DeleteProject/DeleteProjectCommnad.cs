using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.DeleteProject
{
    public class DeleteProjectCommnad : IRequest<ResultViewModel>
    {
        public DeleteProjectCommnad(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
