﻿namespace DevFreela.Infrastructure.Notifications
{
    public interface IEmailService
    {
        Task SendAsync(string email, string subject, string message);
    }
}
