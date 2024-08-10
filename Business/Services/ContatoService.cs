using AutoMapper;
using Core.Dto.Contato;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Responses;

namespace GerContatos.API.Services;

public class ContatoService(IContatoRepository _contatoRepository, IMapper _mapper,IDDDService _dddService, IRegiaoService _regiaoService ) : IContatoService

   
{
    public async Task<Response<CreateContatoDto?>> Create(Contato entidade)
    {

        try
        {
            // Obtém a região e faz o mapeamento do DTO para a entidade
            var regiaoDto = await _regiaoService.GetById(entidade.RegiaoId);
            if (regiaoDto?.Data == null)
                return new Response<CreateContatoDto?>(null, 404, "Região não encontrada");

            var regiaoEntity = _mapper.Map<Regiao>(regiaoDto.Data);
            entidade.Regiao = regiaoEntity;

            // Obtém o DDD e faz o mapeamento do DTO para a entidade
            var dddDto = await _dddService.GetById(entidade.RegiaoId);
            if (dddDto?.Data == null)
                return new Response<CreateContatoDto?>(null, 404, "DDD não encontrado");

            var dddEntity = _mapper.Map<DDD>(dddDto.Data);
            entidade.Regiao.DDDs = new List<DDD> { dddEntity };

            // Continua o processo de criação do contato
            await _contatoRepository.Create(entidade);

            // Mapeia a entidade criada para o DTO
            var contatoDto = _mapper.Map<CreateContatoDto>(entidade);
            return new Response<CreateContatoDto?>(contatoDto, 201, "Contato criado com sucesso");
        }
        catch (Exception ex)
        {
            // Tratamento de exceções
            return new Response<CreateContatoDto?>(null, 500, $"Erro ao criar contato: {ex.Message}");
        }
    }



    public async Task<Response<DeleteContatoDto?>> Delete(int id)
    {
        try
        {
            var contatoExistente = await _contatoRepository.GetById(id);

            if (contatoExistente == null) return new Response<DeleteContatoDto?>(null, 404, "Contato nao encontrado");

            await _contatoRepository.Delete(id);
            return new Response<DeleteContatoDto?>(null, 202, "Contato Excluido com Sucesso");
        }
        catch
        {
            return new Response<DeleteContatoDto?>(null, 505, "Nao foi possivel excluir o contato");
        }
    }

    public async Task<PagedResponse<IList<GetAllContatoDto>>> GetAll()
    {
        try
        {
            var contatos = await _contatoRepository.GetAll( );
            var contatosDto = _mapper.Map<List<GetAllContatoDto>>(contatos);
            var count = contatosDto.Count;

            return new PagedResponse<IList<GetAllContatoDto>>(contatosDto, count, 1, 25);
        }
        catch
        {
            return new PagedResponse<IList<GetAllContatoDto>>(null, 500, "Nao foi possivel buscar todos os contatos");
        }
    }

    public async Task<Response<GetByIdContatoDto?>> GetById(int id)
    {
        try
        {
            var contato = await _contatoRepository.GetById(id);
            if (contato == null)
                return new Response<GetByIdContatoDto?>(null, 404, "Contato nao encontrado");

            var contatoDto = _mapper.Map<GetByIdContatoDto>(contato);
            return new Response<GetByIdContatoDto?>(contatoDto);
        }
        catch
        {
            return new Response<GetByIdContatoDto?>(null, 500, "Nao foi possivel buscar o contato");
        }
    }
    

    public async Task<Response<UpdateContatoDto?>> Update(Contato entidade)
    {
        try
        {
            // Obtém a região e faz o mapeamento do DTO para a entidade
            var regiaoDto = await _regiaoService.GetById(entidade.RegiaoId);
            if (regiaoDto?.Data == null)
                return new Response<UpdateContatoDto?>(null, 404, "Região não encontrada");

            var regiaoEntity = _mapper.Map<Regiao>(regiaoDto.Data);
            entidade.Regiao = regiaoEntity;

            // Obtém o DDD e faz o mapeamento do DTO para a entidade
            var dddDto = await _dddService.GetById(entidade.RegiaoId);
            if (dddDto?.Data == null)
                return new Response<UpdateContatoDto?>(null, 404, "DDD não encontrado");

            var dddEntity = _mapper.Map<DDD>(dddDto.Data);
            entidade.Regiao.DDDs = new List<DDD> { dddEntity };

            // Continua o processo de criação do contato
            await _contatoRepository.Update(entidade);

            // Mapeia a entidade criada para o DTO
            var contatoDto = _mapper.Map<UpdateContatoDto>(entidade);
            return new Response<UpdateContatoDto?>(contatoDto, 201, "Contato criado com sucesso");
        }
        catch (Exception ex)
        {
            // Tratamento de exceções
            return new Response<UpdateContatoDto?>(null, 500, $"Erro ao criar contato: {ex.Message}");
        }
    }
}
