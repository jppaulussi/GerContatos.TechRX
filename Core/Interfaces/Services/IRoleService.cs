using Core.Dto.Role;
using Core.Entities;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services;

public interface IRoleService
{
    Task<PagedResponse<IList<GetAllRoleDto>>> GetAll();
    Task<Response<GetByIdRoleDto?>> GetById(int id);
    Task<Response<CreateRoleDto?>> Create(Role entidade);
    Task<Response<UpdateRoleDto?>> Update(Role entidade);
    Task<Response<DeleteRoleDto?>> Delete(int id);
}
