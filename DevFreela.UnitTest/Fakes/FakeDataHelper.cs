using Bogus;
using DevFreela.Application.Commands.Projects.DeleteProject;
using DevFreela.Application.Commands.Projects.InsertProject;
using DevFreela.Core.Entities;

namespace DevFreela.UnitTest.Fakes
{
    public class FakeDataHelper
    {
        private static readonly Faker _faker = new();

        public static Project CreateFakerProjectV1()
        {
            return new Project(
                _faker.Commerce.ProductName(),
                _faker.Lorem.Sentence(),
                _faker.Random.Int(1, 100),
                _faker.Random.Int(1, 100),
                _faker.Random.Decimal(1000, 10000));
        }

        private static readonly Faker<Project> _projecFaker = new Faker<Project>()
            .CustomInstantiator(f => new Project(
                f.Commerce.ProductName(),
                f.Lorem.Sentence(),
                f.Random.Int(1, 100),
                f.Random.Int(1, 100),
                f.Random.Decimal(1000, 10000)
                ));

        private static readonly Faker<InsertProjectCommand> _insertProjectCommandFaker = new Faker<InsertProjectCommand>()
            .RuleFor(c => c.Title, f => f.Commerce.ProductName())
            .RuleFor(c => c.Description, f => f.Lorem.Sentence())
            .RuleFor(c => c.IdFreelancer, f => f.Random.Int(1, 100))
            .RuleFor(c => c.IdClient, f => f.Random.Int(1, 100))
            .RuleFor(c => c.TotalCost, f => f.Random.Decimal(1000, 10000));

        public static Project CreateFakerProject() => _projecFaker.Generate();

        public static List<Project> CreateFakerProjectList() => _projecFaker.Generate(3);

        public static InsertProjectCommand CreateFakerInsertProjectCommand()
            => _insertProjectCommandFaker.Generate();

        public static DeleteProjectCommnad CreateFakerDeleteProjectCommand(int id)
            => new(id);
    }
}
