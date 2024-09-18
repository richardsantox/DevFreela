﻿using DevFreela.API.Entities;

namespace DevFreela.API.Models
{
    public class ProjectItemViewModel
    {
        public ProjectItemViewModel(int id, string title, string description, string clientName, string freelancerName, decimal totalCost)
        {
            Id = id;
            Title = title;
            Description = description;
            ClientName = clientName;
            FreelancerName = freelancerName;
            TotalCost = totalCost;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; set; }
        public string ClientName { get; private set; }
        public string FreelancerName { get; private set; }
        public decimal TotalCost { get; private set; }

        public static ProjectItemViewModel FromEntity(Project project)
            => new(project.Id, project.Title, project.Description, project.Client.FullName,
                    project.Freelancer.FullName, project.TotalCost);
    }
}
