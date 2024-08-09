using Core.Interfaces.Services;
using Core.Entities;
using Core.Responses;
using Core.Dto.Role;

namespace GerContatos.API.Services
{
    public class RoleService : IRoleService
    {
        public Task<Response<CreateRoleDto?>> Create(Role entidade)
        {
            throw new NotImplementedException();
        }

        public Task<Response<DeleteRoleDto?>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<IList<GetAllRoleDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Response<GetByIdRoleDto?>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<UpdateRoleDto?>> Update(Role entidade)
        {
            throw new NotImplementedException();
        }
    }
}
