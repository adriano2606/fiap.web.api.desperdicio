using Fiap.Web.Api.Desperdicio.Models;
using Fiap.Web.Api.Desperdicio.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.Api.Desperdicio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImpactosController : ControllerBase
    {
        private readonly IImpactoService _service;

        public ImpactosController(IImpactoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var impactos = await _service.GetAllAsync(pageNumber, pageSize);
            return Ok(impactos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var impacto = await _service.GetByIdAsync(id);
            if (impacto == null) return NotFound();
            return Ok(impacto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Impacto impacto)
        {
            await _service.AddAsync(impacto);
            return CreatedAtAction(nameof(GetById), new { id = impacto.Id }, impacto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Impacto impacto)
        {
            if (id != impacto.Id) return BadRequest();
            await _service.UpdateAsync(impacto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
