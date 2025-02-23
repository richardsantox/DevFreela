using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.UnitTest.Fakes;
using FluentAssertions;

namespace DevFreela.UnitTest.Core
{
    public class ProjectTest
    {
        [Fact]
        public void ProjectIsCreated_Start_Sucess()
        {
            //Arrange
            //var project = new Project("Proejto A", "Descrição0", 1, 2, 1000);

            var project = FakeDataHelper.CreateFakerProject();

            //Act
            project.Start();

            //Assert
            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
            project.Status.Should().Be(ProjectStatusEnum.InProgress);

            Assert.NotNull(project.StartedAt);
            project.StartedAt.Should().NotBeNull();

            Assert.True(project.Status == ProjectStatusEnum.InProgress);
            Assert.False(project.StartedAt is null);
        }

        [Fact]
        public void ProjectIsInvalidState_Start_ThrowsException()
        {
            //Arrange
            //var project = new Project("Proejto A", "Descrição0", 1, 2, 1000);

            var project = FakeDataHelper.CreateFakerProject();
            project.Start();

            //Act + Assert
            Action? start = project.Start;

            var expection = Assert.Throws<InvalidOperationException>(start);
            Assert.Equal(Project.INVALID_STATE_MESSAGE, expection.Message);

            start.Should()
                .Throw<InvalidOperationException>()
                .WithMessage(Project.INVALID_STATE_MESSAGE);

        }
    }
}
