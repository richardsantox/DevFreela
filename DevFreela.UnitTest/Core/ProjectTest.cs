using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTest.Core
{
    public class ProjectTest
    {
        [Fact]
        public void ProjectIsCreated_Start_Sucess()
        {
            //Arrange
            var project = new Project("Proejto A", "Descrição0", 1, 2, 1000);

            //Act
            project.Start();

            //Assert
            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
            Assert.NotNull(project.StartedAt);

            Assert.True(project.Status == ProjectStatusEnum.InProgress);
            Assert.False(project.StartedAt is null);
        }

        [Fact]
        public void ProjectIsInvalidState_Start_ThrowsException()
        {
            //Arrange
            var project = new Project("Proejto A", "Descrição0", 1, 2, 1000);
            project.Start();

            //Act + Assert
            Action? start = project.Start;

            var expection = Assert.Throws<InvalidOperationException>(start);
            Assert.Equal(Project.INVALID_STATE_MESSAGE, expection.Message);

        }
    }
}
