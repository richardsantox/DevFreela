using MediatR;

namespace DevFreela.Application.Notification.ProjectCreated
{
    internal class FreelancerNotificationHandler : INotificationHandler<ProjectCreatedNotification>
    {
        public Task Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Notificando os freelnacers sobre o projeto {notification.Title}");   

            return Task.CompletedTask;
        }
    }
}
