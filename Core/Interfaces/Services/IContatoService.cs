using Core.Dto.Contato;
using Core.Entities;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services;

public interface IContatoService
{
    Task<PagedResponse<IList<GetAllContatoDto>>> GetAll();
    Task<Response<GetByIdContatoDto?>> GetById(int id);
    Task<Response<CreateContatoDto?>> Create(Contato entidade);
    Task<Response<UpdateContatoDto?>> Update(Contato entidade);
    Task<Response<DeleteContatoDto?>> Delete(int id);
}
