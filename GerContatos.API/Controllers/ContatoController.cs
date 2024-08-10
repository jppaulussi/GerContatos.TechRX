using AutoMapper;
using Core.Dto.Contato;
using Core.Dto.Usuarios;
using Core.Entities;
using Core.Enums;
using Core.Interfaces.Services;
using Core.Request.User;
using GerContatos.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerContatos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatoController(IContatoService _contatoService, IMapper _mapper, IDDDService _dddService) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateContatoDto createContatoDto)
        {
            try
            {
                var contato = _mapper.Map<Contato>(createContatoDto);
                var response = await _contatoService.Create(contato);

                if (response.IsSuccess)
                {
                    return CreatedAtAction(nameof(GetById), new { id = response.Data!.Id }, response.Data);
                }

                return BadRequest(response.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _contatoService.GetById(id);

            if (response.IsSuccess)
            {
                var contatoDto = _mapper.Map<GetByIdContatoDto>(response.Data);
                return Ok(contatoDto);
            }

            return BadRequest(response.Message);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var response = await _contatoService.GetAll();

            if (response.IsSuccess)
            {
                var contatosDto = _mapper.Map<IList<GetAllContatoDto>>(response.Data);
                return Ok(contatosDto);
            }

            return BadRequest(response.Message);
        }

        [HttpGet("contatos/por-ddd/{dddId}")]
        public async Task<IActionResult> GetContatosPorDDD(int dddId)
        {
            // Passo 1: Obter o DDD usando a DDDService
            var dddResponse = await _dddService.GetById(dddId);
            if (dddResponse?.Data == null)
                return NotFound("DDD não encontrado");

            // Passo 2: Usar o RegiaoId obtido para filtrar os contatos
            var regiaoId = dddResponse.Data.RegiaoId;
            var todosContatos = await _contatoService.GetAll();

            // Passo 3: Filtrar os contatos pelo RegiaoId associado ao DDD
            var contatosFiltrados = todosContatos.Data.Where(c => c.RegiaoId == regiaoId).ToList();

            if (!contatosFiltrados.Any())
                return NotFound("Nenhum contato encontrado para o DDD especificado");

            // Passo 4: Retornar os contatos filtrados
            return Ok(contatosFiltrados);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateContatoDto updateContatoDto)
        {
            try
            {
                var contato = _mapper.Map<Contato>(updateContatoDto);
                contato.Id = id;
                var response = await _contatoService.Update(contato);

                if (response.IsSuccess)
                {
                    return Ok(response.Data);
                }

                return BadRequest(response.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}



