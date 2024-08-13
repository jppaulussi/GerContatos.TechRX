using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Services;
using Core.Entities;
using Core.Dto.TipoTelefone;
using Core.Responses;
using System.Threading.Tasks;

namespace GerContatos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoTelefoneController : ControllerBase
    {
        private readonly ITipoTelefoneService _tipoTelefoneService;

        public TipoTelefoneController(ITipoTelefoneService tipoTelefoneService)
        {
            _tipoTelefoneService = tipoTelefoneService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _tipoTelefoneService.GetAll();
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            return StatusCode(500, response.Message); // Internal Server Error
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _tipoTelefoneService.GetById(id);
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            return NotFound(response.Message); // Not Found
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTipoTelefoneDto dto)
        {
            if (dto == null)
            {
                return BadRequest("TipoTelefone cannot be null");
            }

            var response = await _tipoTelefoneService.Create(dto);
            if (response.IsSuccess)
            {
                return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response.Data);
            }
            return BadRequest(response.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTipoTelefoneDto dto)
        {
            if (dto == null)
            {
                return BadRequest("TipoTelefone cannot be null");
            }

            var response = await _tipoTelefoneService.Update(dto);
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            return NotFound(response.Message);
        }
    }
}
