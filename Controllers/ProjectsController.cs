﻿using DevFreela.API.Models;
using DevFreela.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase    
    {
        private readonly FreelanceTotalCostConfig _config;
        private readonly IConfigService _configService;
        public ProjectsController(
            IOptions<FreelanceTotalCostConfig> options,
            IConfigService configService)
        {
            _config = options.Value;
            _configService = configService;
        }

        [HttpGet]
        public IActionResult Get(string search = "")
        {
            return Ok(_configService.GetValue());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(CreateProjectInputModel model)
        {
            if (model.TotalCost < _config.Minimum || model.TotalCost > _config.Maximum)
            {
                return BadRequest("Numero fora dos limites.");
            }

            return CreatedAtAction(nameof(GetById), new { id = 1 }, model); 
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel model)
        {
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id) 
        {
            return NoContent();
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostCoomment(int id, CreateProjectCommentInputModel model)
        {
            return NoContent();
        }
    }
}
