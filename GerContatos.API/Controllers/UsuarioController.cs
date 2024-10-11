using Core.Entities;
using Model.Enums;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Core.Request.User;
using Core.Dto.Usuarios;

namespace GerContatos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController(IUsuarioService _usuarioService,IMapper _mapper) : ControllerBase
    {

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            var usuario =  await _usuarioService.GetById(id);

            if (usuario.IsSuccess)
            {
                var usuarioDto = _mapper.Map<UsuarioGetByIdDto>(usuario.Data);
                return Ok(usuarioDto);
            }

            return BadRequest(usuario);
        }
        
        [HttpPost]
        [Authorize(Roles = PermissaoSistema.Administrador)]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserRequest request)
        {
            try
            {
                var usuario = _mapper.Map<Usuario>(request);
                await _usuarioService.Create(usuario);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
