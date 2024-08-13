using Core.Interfaces.Services;
using Core.Dto.Role;
using Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GerContatos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _roleService.GetAll();
            if (response.Data == null || response.Data.Count == 0)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _roleService.GetById(id);
            if (!response.IsSuccess)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleDto createRoleDto)
        {
            if (createRoleDto == null)
            {
                return BadRequest("Invalid role data.");
            }

            var role = new Core.Entities.Role
            {
                Tipo = createRoleDto.Tipo // Supondo que Tipo é um Enum que você está recebendo no DTO
            };

            var response = await _roleService.Create(role);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }
            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRoleDto updateRoleDto)
        {
            if (updateRoleDto == null || id != updateRoleDto.Id)
            {
                return BadRequest("Invalid role data.");
            }

            var role = new Core.Entities.Role
            {
                Id = id,
                Tipo = updateRoleDto.Tipo // Supondo que Tipo é um Enum que você está recebendo no DTO
            };

            var response = await _roleService.Update(role);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _roleService.Delete(id);
            if (!response.IsSuccess)
            {
                return NotFound(response.Message);
            }
            return NoContent(); // NoContent indica uma exclusão bem-sucedida sem retorno de dados
        }
    }
}
