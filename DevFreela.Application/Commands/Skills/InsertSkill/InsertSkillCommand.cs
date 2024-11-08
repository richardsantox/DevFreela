﻿using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.Skills.InsertSkill
{
    public class InsertSkillCommand : IRequest<ResultViewModel>
    {
        public string Description { get; set; }

        public Skill ToEntity() => new (Description);
    }
}