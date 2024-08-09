using Core.Interfaces.Services;
using Core.Entities;
using Core.Responses;
using Core.Dto.DDD;

namespace GerContatos.API.Services
{
    public class DDDService : IDDDService
    {
        public Task<Response<CreateDDDDto?>> Create(DDD entidade)
        {
            throw new NotImplementedException();
        }

        public Task<Response<DeleteDDDDto?>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<IList<GetAllDDDDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Response<GetByIdDDDDto?>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<UpdateDDDDto?>> Update(DDD entidade)
        {
            throw new NotImplementedException();
        }
    }
}
