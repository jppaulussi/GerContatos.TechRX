using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories;

public interface IRepository<T>
{
    Task<IList<T>> GetAll();
    Task<T> GetById(int id);
    Task Create(T entidade);
    Task Update(T entidade);
    Task Delete(int id);

}
