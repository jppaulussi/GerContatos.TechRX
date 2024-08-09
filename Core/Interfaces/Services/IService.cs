using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services;

public interface IService<T>
{
    Task<PagedResponse<IList<T>>> GetAll();
    Task<Response<T?>> GetById(int id);
    Task<Response<T?>> Create(T entidade);
    Task<Response<T?>> Update(T entidade);
    Task<Response<T?>> Delete(int id);

}
