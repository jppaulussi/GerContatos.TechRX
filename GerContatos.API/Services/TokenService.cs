using Core.Interfaces.Services;
using Core.Entities;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Enums;

namespace GerContatos.API.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IUsuarioService _usuarioService;

    public TokenService(IConfiguration configuration, IUsuarioService usuarioService)
    {
        _configuration = configuration;
        _usuarioService = usuarioService;
    }

    public async Task<string> GetToken(Usuario usuario)
    {
        try
        {

     
            var usuarioExistente = (await _usuarioService.GetAllToken()).Data!.Where(usu => usu.Email == usuario.Email && usu.Password == usuario.Password).FirstOrDefault();

            if (usuarioExistente == null)
                return string.Empty;

            //variável responsável por gerar o token
            var tokenHandler = new JwtSecurityTokenHandler();
            string roleName = Enum.GetName(typeof(ERole), usuarioExistente.RoleId)!;
            //recuperando a chave secreta
            var chaveCriptografia = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretJWT")!);

            //propriedades do token
            var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuarioExistente.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64),
            new Claim(ClaimTypes.NameIdentifier, usuarioExistente.Id.ToString()),
            new Claim(ClaimTypes.Name, usuarioExistente.Email),
            new Claim(ClaimTypes.Email, usuarioExistente.Email),
            new Claim(ClaimTypes.Role, roleName),
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chaveCriptografia),
                                     SecurityAlgorithms.HmacSha256Signature)
            };

            //Cria o token 
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        catch
        {
            return string.Empty;
        }



    }
}
