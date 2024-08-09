using Core.Dto.Contato;
using Core.Entities;
using Core.Interfaces.Services;
using Core.Responses;

namespace GerContatos.API.Services;

public class ContatoService : IContatoService
{
    public Task<Response<CreateContatoDto?>> Create(Contato entidade)
    {
        throw new NotImplementedException();
    }

    public Task<Response<DeleteContatoDto?>> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResponse<IList<GetAllContatoDto>>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Response<GetByIdContatoDto?>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Response<UpdateContatoDto?>> Update(Contato entidade)
    {
        throw new NotImplementedException();
    }
}
