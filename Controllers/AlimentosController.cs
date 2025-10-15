using Fiap.Web.Api.Desperdicio.Models;
using Fiap.Web.Api.Desperdicio.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.Api.Desperdicio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlimentosController : ControllerBase
    {
        private readonly IAlimentoService _service;

        public AlimentosController(IAlimentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var alimentos = await _service.GetAllAsync(pageNumber, pageSize);
            return Ok(alimentos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var alimento = await _service.GetByIdAsync(id);
            if (alimento == null) return NotFound();
            return Ok(alimento);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Alimento alimento)
        {
            await _service.AddAsync(alimento);
            return CreatedAtAction(nameof(GetById), new { id = alimento.Id }, alimento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Alimento alimento)
        {
            if (id != alimento.Id) return BadRequest();
            await _service.UpdateAsync(alimento);
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
