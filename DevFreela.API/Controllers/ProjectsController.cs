using DevFreela.Infrastructure.Persistence;
using DevFreela.Application.Models;
using Microsoft.AspNetCore.Mvc;
using DevFreela.Application.Services;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase    
    {
        private readonly DevFreelaDbContext _context;
        private readonly IProjectService _service;

        public ProjectsController(DevFreelaDbContext context, IProjectService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        public IActionResult Get(string search = "", int page = 0, int size = 3)
        {
            var result = _service.GetAll(page, size);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
        {
            var result = _service.GetById(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(CreateProjectInputModel model)
        {
            var result = _service.Insert(model);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, model); 
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel model)
        {
            var result = _service.Update(model);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id) 
        {
            var result = _service.Delete(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var result = _service.Start(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var result = _service.Complete(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostCoomment(int id, CreateProjectCommentInputModel model)
        {
            var result = _service.InsertCommet(id, model);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }
    }
}
