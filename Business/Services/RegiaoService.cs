using Core.Interfaces.Services;
using Core.Entities;
using Core.Responses;
using Core.Dto.Regiao;

namespace GerContatos.API.Services
{
    public class RegiaoService : IRegiaoService
    {
        public Task<Response<CreateRegiaoDto?>> Create(Regiao entidade)
        {
            throw new NotImplementedException();
        }

        public Task<Response<DeleteRegiaoDto?>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<IList<GetAllRegiaoDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Response<GetByIdRegiaoDto?>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<UpdateRegiaoDto?>> Update(Regiao entidade)
        {
            throw new NotImplementedException();
        }
    }
}
