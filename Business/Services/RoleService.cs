using Core.Interfaces.Services;
using Core.Entities;
using Core.Responses;
using Core.Dto.Role;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Interfaces.Repositories;
using Core.Enums;

namespace GerContatos.API.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public Task<PagedResponse<IList<GetAllRoleDto>>> GetAll()
        {
            var roles = _roleRepository.GetAll().Result; // Utilizando .Result para obter o resultado da Task
            var roleDtos = _mapper.Map<IList<GetAllRoleDto>>(roles);
            return Task.FromResult(new PagedResponse<IList<GetAllRoleDto>>(roleDtos));
        }

        public Task<Response<GetByIdRoleDto?>> GetById(int id)
        {
            var role = _roleRepository.GetById(id).Result; // Utilizando .Result para obter o resultado da Task
            if (role == null)
            {
                return Task.FromResult(new Response<GetByIdRoleDto?>(default, 404, "Role not found"));
            }
            var roleDto = _mapper.Map<GetByIdRoleDto>(role);
            return Task.FromResult(new Response<GetByIdRoleDto?>(roleDto));
        }

        public Task<Response<CreateRoleDto?>> Create(Role entidade)
        {
            if (entidade == null)
            {
                return Task.FromResult(new Response<CreateRoleDto?>(default, 400, "Role cannot be null"));
            }

            if (!Enum.IsDefined(typeof(ERole), entidade.Tipo))
            {
                return Task.FromResult(new Response<CreateRoleDto?>(default, 400, "Invalid role type"));
            }

            _roleRepository.Create(entidade).Wait(); // Usando .Wait() para esperar a Task ser concluída
            var roleDto = _mapper.Map<CreateRoleDto>(entidade); // O DTO para criação geralmente é o mesmo que o DTO retornado
            return Task.FromResult(new Response<CreateRoleDto?>(roleDto, 201)); // Código 201 para criação
        }

        public Task<Response<UpdateRoleDto?>> Update(Role entidade)
        {
            if (entidade == null)
            {
                return Task.FromResult(new Response<UpdateRoleDto?>(default, 400, "Role cannot be null"));
            }

            var existingRole = _roleRepository.GetById(entidade.Id).Result; // Utilizando .Result para obter o resultado da Task
            if (existingRole == null)
            {
                return Task.FromResult(new Response<UpdateRoleDto?>(default, 404, "Role not found"));
            }

            existingRole.Tipo = entidade.Tipo;
            _roleRepository.Update(existingRole).Wait(); // Usando .Wait() para esperar a Task ser concluída
            var roleDto = _mapper.Map<UpdateRoleDto>(existingRole);
            return Task.FromResult(new Response<UpdateRoleDto?>(roleDto));
        }

        public Task<Response<DeleteRoleDto?>> Delete(int id)
        {
            var role = _roleRepository.GetById(id).Result; // Utilizando .Result para obter o resultado da Task
            if (role == null)
            {
                return Task.FromResult(new Response<DeleteRoleDto?>(default, 404, "Role not found"));
            }

            _roleRepository.Delete(id).Wait(); // Usando .Wait() para esperar a Task ser concluída
            var roleDto = _mapper.Map<DeleteRoleDto>(role);
            return Task.FromResult(new Response<DeleteRoleDto?>(roleDto, 204)); // Código 204 para exclusão bem-sucedida sem conteúdo
        }
    }
}
