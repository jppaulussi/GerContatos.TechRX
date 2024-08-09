using Core.Dto.Usuarios;
using Core.Entities;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services;

public interface IUsuarioService 
{
    Task<PagedResponse<IList<GetAllUsuarioDto>>> GetAll();
    Task<Response<UsuarioGetByIdDto?>> GetById(int id);
    Task<Response<CreateUsuarioDto?>> Create(Usuario entidade);
    Task<Response<UpdateUsuarioDto?>> Update(Usuario entidade);
    Task<Response<DeleteUsuarioDto?>> Delete(int id);
    Task<PagedResponse<IList<GetAllByTokenDto>>> GetAllToken();
}

