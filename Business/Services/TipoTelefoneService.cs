using Core.Interfaces.Services;
using Core.Entities;
using Core.Responses;
using Core.Dto.TipoTelefone;

namespace GerContatos.API.Services
{
    public class TipoTelefoneService : ITipoTelefoneService
    {
        public Task<Response<CreateTipoTelefoneDto?>> Create(TipoTelefone entidade)
        {
            throw new NotImplementedException();
        }

        public Task<Response<DeleteTipoTelefoneDto?>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<IList<GetAllTipoTelefoneDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Response<GetByIdTipoTelefoneDto?>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<UpdateTipoTelefoneDto?>> Update(TipoTelefone entidade)
        {
            throw new NotImplementedException();
        }
    }
}
