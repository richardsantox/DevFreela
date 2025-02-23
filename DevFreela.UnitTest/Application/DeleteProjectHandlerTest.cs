﻿using DevFreela.Application.Commands.Projects.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTest.Fakes;
using FluentAssertions;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTest.Application
{
    public class DeleteProjectHandlerTest
    {
        [Fact]
        public async Task ProjectExist_Delete_Sucess_NSbustitute()
        {
            //Arrange 
            //var project = new Project("Projeto Teste", "Descrição Teste", 1, 2, 10000);

            var project = FakeDataHelper.CreateFakerProject();

            var repository = Substitute.For<IProjectRepository>();
            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
            repository.Update(Arg.Any<Project>()).Returns(Task.CompletedTask);

            var handler = new DeleteProjectHandler(repository);

            var command = new DeleteProjectCommnad(1);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeTrue();

            await repository.Received(1).GetById(1);
            await repository.Received(1).Update(Arg.Any<Project>());
        }

        [Fact]
        public async Task ProjectDoesNotExist_Delete_Error_NSubstitute()
        {
            //Arrange
            var repository = Substitute.For<IProjectRepository>();
            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)null));

            var handler = new DeleteProjectHandler(repository);

            var command = new DeleteProjectCommnad(1);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.False(result.IsSuccess);
            result.IsSuccess.Should().BeFalse();

            Assert.Equal(DeleteProjectHandler.PROJECT_NOT_FOUND_MESSAGE, result.Message);
            result.Message.Should().BeEquivalentTo(
                DeleteProjectHandler.PROJECT_NOT_FOUND_MESSAGE);

            await repository.Received(1).GetById(Arg.Any<int>());
            await repository.DidNotReceive().Update(Arg.Any<Project>());
        }

        [Fact]
        public async Task ProjectExist_Delete_Sucess_Moq()
        {
            //Arrange 
            //var project = new Project("Projeto Teste", "Descrição Teste", 1, 2, 10000);

            var project = FakeDataHelper.CreateFakerProject();

            var repository = Mock.Of<IProjectRepository>(p =>
                p.GetById(It.IsAny<int>()) == Task.FromResult(project) &&
                p.Update(It.IsAny<Project>()) == Task.CompletedTask);

            var handler = new DeleteProjectHandler(repository);

            var command = new DeleteProjectCommnad(1);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeTrue();

            Mock.Get(repository).Verify(r => r.GetById(1), Times.Once);
            Mock.Get(repository).Verify(r => r.Update(It.IsAny<Project>()), Times.Once);  
        }

        [Fact]
        public async Task ProjectDoesNotExist_Delete_Error_Moq()
        {
            //Arrange
            var repository = Mock.Of<IProjectRepository>(r =>
                r.GetById(It.IsAny<int>()) == Task.FromResult((Project?)  null));


            var handler = new DeleteProjectHandler(repository);

            var command = new DeleteProjectCommnad(1);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.False(result.IsSuccess);
            result.IsSuccess.Should().BeFalse();

            Assert.Equal(DeleteProjectHandler.PROJECT_NOT_FOUND_MESSAGE, result.Message);
            result.Message.Should().BeEquivalentTo(
                DeleteProjectHandler.PROJECT_NOT_FOUND_MESSAGE);

            Mock.Get(repository).Verify(r => r.GetById(1), Times.Once);
            Mock.Get(repository).Verify(r => r.Update(It.IsAny<Project>()), Times.Never);
        }
    }
}
