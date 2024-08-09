using AutoMapper;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.Request.Token;

namespace GerContatos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController(ITokenService _tokenService, IMapper _mapper) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GetUsuarioTokenRequest request)
        {
            var usuario = _mapper.Map<Usuario>(request);
            var token = await _tokenService.GetToken(usuario);

            if(!string.IsNullOrWhiteSpace(token))
                return Ok(token);

            return Unauthorized();
        }
    }
}
