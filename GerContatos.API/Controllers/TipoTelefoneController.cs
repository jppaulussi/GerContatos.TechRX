using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Services;
using Core.Entities;
using Core.Dto.TipoTelefone;
using Core.Responses;
using System.Threading.Tasks;
using AutoMapper;

namespace GerContatos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoTelefoneController : ControllerBase
    {
        private readonly ITipoTelefoneService _tipoTelefoneService;
        private readonly IMapper _mapper;

        public TipoTelefoneController(ITipoTelefoneService tipoTelefoneService, IMapper mapper)
        {
            _tipoTelefoneService = tipoTelefoneService;
            _mapper = mapper; // Adicione esta linha

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
        public async Task<IActionResult> Create([FromBody] CreateTipoTelefoneDto createTipoTelefoneDto)
        {
            if (createTipoTelefoneDto == null)
            {
                return BadRequest("TipoTelefone cannot be null");
            }

            // Mapeia o DTO para a entidade
            var tipoTelefone = _mapper.Map<TipoTelefone>(createTipoTelefoneDto);

            // Chama o serviço para criar o TipoTelefone
            var result = await _tipoTelefoneService.Create(tipoTelefone);

            // Verifica se a operação foi bem-sucedida
            if (result.IsSuccess)
            {
                // Mapeia a entidade criada para o DTO
                var tipoTelefoneDto = _mapper.Map<CreateTipoTelefoneDto>(result.Data);
                // Retorna o status de criado com a localização do recurso
                return CreatedAtAction(nameof(GetById), new { id = tipoTelefoneDto.Id }, tipoTelefoneDto);
            }

            // Se a operação falhou, retorna o erro apropriado
            return BadRequest(result.Message);
        }



        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTipoTelefoneDto dto)
        {
            if (dto == null)
            {
                return BadRequest("TipoTelefone cannot be null");
            }

            var tipoTelefone = _mapper.Map<TipoTelefone>(dto);
            var response = await _tipoTelefoneService.Update(tipoTelefone);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            return NotFound(response.Message);
        }

    }
}
