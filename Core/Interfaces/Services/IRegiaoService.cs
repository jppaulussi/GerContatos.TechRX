using Core.Dto.Regiao;
using Core.Entities;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services;

public interface IRegiaoService
{
    Task<PagedResponse<IList<GetAllRegiaoDto>>> GetAll();
    Task<Response<GetByIdRegiaoDto?>> GetById(int id);
    Task<Response<CreateRegiaoDto?>> Create(Regiao entidade);
    Task<Response<UpdateRegiaoDto?>> Update(Regiao entidade);
    Task<Response<DeleteRegiaoDto?>> Delete(int id);
}
