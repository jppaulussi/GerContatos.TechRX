using AutoMapper;
using Core.Dto.DDD;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerContatos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DDDController : ControllerBase
    {
        private readonly IDDDService _dddService;
        private readonly IMapper _mapper;

        public DDDController(IDDDService dddService, IMapper mapper)
        {
            _dddService = dddService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([FromBody] CreateDDDDto createDDDRequest)
        {
            try
            {
                if (createDDDRequest == null)
                    return BadRequest("Dados inválidos.");

                var ddd = _mapper.Map<DDD>(createDDDRequest);
                var response = await _dddService.Create(ddd);

                if (response.IsSuccess)
                {
                    return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response.Data);
                }

                return BadRequest(response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
  
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _dddService.GetById(id);

            if (response.IsSuccess)
            {
                var dddDto = _mapper.Map<GetByIdDDDDto>(response.Data);
                return Ok(dddDto);
            }

            return NotFound(response.Message);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _dddService.GetAll();

            if (response.IsSuccess)
            {
                var dddsDto = _mapper.Map<IList<GetAllDDDDto>>(response.Data);
                return Ok(dddsDto);
            }

            return BadRequest(response.Message);
        }

        [HttpGet("por-regiao/{regiaoId}")]
        [Authorize]
        public async Task<IActionResult> GetByRegiaoId(int regiaoId)
        {
            try
            {
                // Primeiro, obter todos os DDDs
                var response = await _dddService.GetAll();
                if (!response.IsSuccess)
                    return BadRequest(response.Message);

                // Filtrar os DDDs pela RegiaoId
                var dddsFiltrados = response.Data.Where(d => d.RegiaoId == regiaoId).ToList();

                if (!dddsFiltrados.Any())
                    return NotFound("Nenhum DDD encontrado para a região especificada");

                var dddsDto = _mapper.Map<IList<GetAllDDDDto>>(dddsFiltrados);
                return Ok(dddsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDDDDto updateDDDRequest)
        {
            try
            {
                if (updateDDDRequest == null || id != updateDDDRequest.Id)
                    return BadRequest("Dados inválidos.");

                var ddd = _mapper.Map<DDD>(updateDDDRequest);
                ddd.Id = id;

                var response = await _dddService.Update(ddd);

                if (response.IsSuccess)
                {
                    return Ok(response.Data);
                }

                return BadRequest(response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _dddService.Delete(id);

                if (response.IsSuccess)
                {
                    return NoContent();
                }

                return NotFound(response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
