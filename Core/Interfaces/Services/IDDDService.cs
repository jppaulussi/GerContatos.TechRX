using Core.Dto.DDD;
using Core.Entities;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services;

public interface IDDDService
{
    Task<PagedResponse<IList<GetAllDDDDto>>> GetAll();
    Task<Response<GetByIdDDDDto?>> GetById(int id);
    Task<Response<CreateDDDDto?>> Create(DDD entidade);
    Task<Response<UpdateDDDDto?>> Update(DDD entidade);
    Task<Response<DeleteDDDDto?>> Delete(int id);
}
