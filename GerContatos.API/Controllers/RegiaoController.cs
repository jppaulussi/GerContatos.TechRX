using AutoMapper;
using Core.Dto.Regiao;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerContatos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegiaoController : ControllerBase
    {
        private readonly IRegiaoService _regiaoService;
        private readonly IMapper _mapper;

        public RegiaoController(IRegiaoService regiaoService, IMapper mapper)
        {
            _regiaoService = regiaoService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([FromBody] CreateRegiaoDto createRegiaoDto)
        {
            try
            {
                if (createRegiaoDto == null)
                    return BadRequest("Dados inválidos.");

                var regiao = _mapper.Map<Regiao>(createRegiaoDto);
                var response = await _regiaoService.Create(regiao);

                if (response.IsSuccess)
                {
                    return CreatedAtAction(nameof(GetById), new { id = response.Data!.Id }, response.Data);
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
            var response = await _regiaoService.GetById(id);

            if (response.IsSuccess)
            {
                var regiaoDto = _mapper.Map<GetByIdRegiaoDto>(response.Data);
                return Ok(regiaoDto);
            }

            return NotFound(response.Message);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var response = await _regiaoService.GetAll();

            if (response.IsSuccess)
            {
                var regioesDto = _mapper.Map<IList<GetAllRegiaoDto>>(response.Data);
                return Ok(regioesDto);
            }

            return BadRequest(response.Message);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRegiaoDto updateRegiaoDto)
        {
            try
            {
                if (updateRegiaoDto == null || id != updateRegiaoDto.Id)
                    return BadRequest("Dados inválidos.");

                var regiao = _mapper.Map<Regiao>(updateRegiaoDto);
                regiao.Id = id;

                var response = await _regiaoService.Update(regiao);

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
                var response = await _regiaoService.Delete(id);

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
