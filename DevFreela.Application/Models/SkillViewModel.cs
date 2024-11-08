using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
    internal class SkillViewModel
    {
        public SkillViewModel(string description)
        {
            Description = description;
        }

        public string Description { get; set; }

        public static SkillViewModel FromEntity(Skill skill)
            => new(skill.Description);
    }
}
