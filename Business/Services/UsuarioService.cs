using Core.Interfaces.Services;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Responses;
using Core.Request.User;
using Core.Dto.Usuarios;
using AutoMapper;
using Core.Dto.Role;

namespace GerContatos.API.Services
{
    public class UsuarioService(IUsuarioRepository _usuarioRepository, IMapper _mapper) : IUsuarioService
    {
        public async Task<Response<CreateUsuarioDto?>> Create(Usuario entidade)
        {
            try
            {
                await _usuarioRepository.Create(entidade);
                var dto = _mapper.Map<CreateUsuarioDto>(entidade);
                return new Response<CreateUsuarioDto?>(dto, 201, "Usuário cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<CreateUsuarioDto?>(null, 500, $"Não foi possível cadastrar o usuário: {ex.Message}");
            }
        }

        public async Task<Response<DeleteUsuarioDto?>> Delete(int id)
        {
            try
            {
                var usuarioExistente = await _usuarioRepository.GetById(id);

                if (usuarioExistente == null)
                    return new Response<DeleteUsuarioDto?>(null, 404, "Usuário não encontrado");

                await _usuarioRepository.Delete(id);
                return new Response<DeleteUsuarioDto?>(null, 204, "Usuário excluído com sucesso");
            }
            catch { return new Response<DeleteUsuarioDto?>(null, 500, "Não foi possível deletar esse usuário"); }
        }

        public async Task<PagedResponse<IList<GetAllUsuarioDto>>> GetAll()
        {

            try { 
            var listUsuarios = (await _usuarioRepository.GetAll()).ToList();
            var count = listUsuarios.Count;

                var listUsuariosDto = _mapper.Map<List<GetAllUsuarioDto>>(listUsuarios);

                return new PagedResponse<IList<GetAllUsuarioDto>>(
                listUsuariosDto,
                count,
                1,      // currentPage
                25);    // pageSize
            }
            catch
            {
                return new PagedResponse<IList<GetAllUsuarioDto>>(null, 500, "Não foi possível consultar os usuários");
            }
        }

        public async Task<PagedResponse<IList<GetAllByTokenDto>>> GetAllToken()
        {
            try
            {
                var listUsuarios = await _usuarioRepository.GetAll();
                var count = listUsuarios.Count;

                var listUsuariosDto = _mapper.Map<List<GetAllByTokenDto>>(listUsuarios);

                return new PagedResponse<IList<GetAllByTokenDto>>(
                listUsuariosDto,
                count,
                1,      // currentPage
                25);    // pageSize
            }
            catch(Exception ex)
            {
                return new PagedResponse<IList<GetAllByTokenDto>>(null, 500, "Não foi possível consultar os usuários");
            }
        }

        public async Task<Response<UsuarioGetByIdDto?>> GetById(int id)
        {
            try
            {
                var usuario = await _usuarioRepository.GetById(id);
                var usuarioDto = _mapper.Map<UsuarioGetByIdDto>(usuario);

                if (usuario == null)
                    return new Response<UsuarioGetByIdDto?>(null, 404, "Não foi possível localizar o usuário informado");

                return new Response<UsuarioGetByIdDto?>(usuarioDto);
            }
            catch
            {
                return new Response<UsuarioGetByIdDto?>(null, 500, "Não foi possível buscar o usuário");
            }
        }

        public async Task<Response<UpdateUsuarioDto?>> Update(Usuario entidade)
        {
            try
            {
                await _usuarioRepository.Update(entidade);
                var usuarioDto = _mapper.Map<UpdateUsuarioDto>(entidade);

                return new Response<UpdateUsuarioDto?>(usuarioDto, 200, "Usuário atualizado com sucesso");
            }
            catch
            {
                return new Response<UpdateUsuarioDto?>(null, 500, "Não foi possível atualizar o usuário");
            }
        }
    }
}
