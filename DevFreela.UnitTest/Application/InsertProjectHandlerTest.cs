using DevFreela.Application.Commands.Projects.InsertProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTest.Fakes;
using FluentAssertions;
using MediatR;
using Moq;
using NSubstitute;

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

            //var command = new InsertProjectCommand()
            //{
            //    Title = "Projeto teste",
            //    Description = "Descrição Teste",
            //    TotalCost = 1000,
            //    IdClient = 1,
            //    IdFreelancer = 2
            //};

            var command = FakeDataHelper.CreateFakerInsertProjectCommand();

            var handler = new InsertProjectHandler(repository, mediator);

            //Act 
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);

            result.IsSuccess.Should().BeTrue();

            Assert.Equal(ID, result.Data);

            result.Data.Should().Be(ID);

            await repository.Received(1).Add(Arg.Any<Project>());
        }

        [Fact]
        public async Task InputDataAreOk_Insert_Sucess_Moq()
        {
            //Arrange
            const int ID = 1;

            //var mock = new Mock<IProjectRepository>();
            //mock.Setup(r => r.Add(It.IsAny<Project>()).Returns(ID));

            var repository = 
                Mock.Of<IProjectRepository>(r => r.Add(It.IsAny<Project>()) == Task.FromResult(1));

            var mediator = Mock.Of<IMediator>();

            //var command = new InsertProjectCommand()
            //{
            //    Title = "Projeto teste",
            //    Description = "Descrição Teste",
            //    TotalCost = 1000,
            //    IdClient = 1,
            //    IdFreelancer = 2
            //};

            var command = FakeDataHelper.CreateFakerInsertProjectCommand();

            var handler = new InsertProjectHandler(repository, mediator);

            //Act 
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(ID, result.Data);

            //mock.Verify(m => m.Add(It.IsAny<Project>()), Times.Once());

            Mock.Get(repository).Verify(m => m.Add(It.IsAny<Project>()), Times.Once()); 
        }
    }
}
