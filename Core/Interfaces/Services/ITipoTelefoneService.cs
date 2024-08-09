using Core.Dto.TipoTelefone;
using Core.Entities;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services;

public interface ITipoTelefoneService
{
    Task<PagedResponse<IList<GetAllTipoTelefoneDto>>> GetAll();
    Task<Response<GetByIdTipoTelefoneDto?>> GetById(int id);
    Task<Response<CreateTipoTelefoneDto?>> Create(TipoTelefone entidade);
    Task<Response<UpdateTipoTelefoneDto?>> Update(TipoTelefone entidade);
    Task<Response<DeleteTipoTelefoneDto?>> Delete(int id);
}
