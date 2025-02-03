using DevFreela.Application.Commands.Projects.InsertProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.UnitTest.Application
{
    public class InsertProjectHandlerTest
    {
        [Fact]
        public async Task InputDataAreOk_Insert_Sucess_NSubstitute()
        {
            //Arrange
            const int ID = 1;

            var repository = Substitute.For<IProjectRepository>();
            repository.Add(Arg.Any<Project>()).Returns(Task.FromResult(1));

            var mediator = Substitute.For<IMediator>();

            var command = new InsertProjectCommand()
            {
                Title = "Projeto teste",
                Description = "Descrição Teste",
                TotalCost = 1000,
                IdClient = 1,
                IdFreelancer = 2
            };

            var handler = new InsertProjectHandler(repository, mediator);

            //Act 
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(ID, result.Data);
            await repository.Received(1).Add(Arg.Any<Project>());
        }
    }
}
