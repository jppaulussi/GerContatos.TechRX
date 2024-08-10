using AutoMapper;
using Core.Dto.Regiao;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerContatos.API.Services
{
    public class RegiaoService : IRegiaoService
    {
        private readonly IRegiaoRepository _regiaoRepository;
        private readonly IMapper _mapper;

        public RegiaoService(IRegiaoRepository regiaoRepository, IMapper mapper)
        {
            _regiaoRepository = regiaoRepository;
            _mapper = mapper;
        }

        public async Task<Response<CreateRegiaoDto?>> Create(Regiao entidade)
        {
            try
            {
                await _regiaoRepository.Create(entidade);
                var dto = _mapper.Map<CreateRegiaoDto>(entidade);
                return new Response<CreateRegiaoDto?>(dto, 201, "Região criada com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<CreateRegiaoDto?>(null, 500, $"Erro ao criar Região: {ex.Message}");
            }
        }

        public async Task<Response<DeleteRegiaoDto?>> Delete(int id)
        {
            try
            {
                var regiaoExistente = await _regiaoRepository.GetById(id);

                if (regiaoExistente == null)
                    return new Response<DeleteRegiaoDto?>(null, 404, "Região não encontrada");

                await _regiaoRepository.Delete(id);
                return new Response<DeleteRegiaoDto?>(null, 202, "Região excluída com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<DeleteRegiaoDto?>(null, 500, $"Erro ao excluir Região: {ex.Message}");
            }
        }

        public async Task<PagedResponse<IList<GetAllRegiaoDto>>> GetAll()
        {
            try
            {
                var regioes = await _regiaoRepository.GetAll();
                var dtoList = _mapper.Map<IList<GetAllRegiaoDto>>(regioes);
                var count = dtoList.Count;

                return new PagedResponse<IList<GetAllRegiaoDto>>(dtoList, count, 1, 25);
            }
            catch (Exception ex)
            {
                return new PagedResponse<IList<GetAllRegiaoDto>>(null, 500, $"Erro ao buscar todas as Regiões: {ex.Message}");
            }
        }

        public async Task<Response<GetByIdRegiaoDto?>> GetById(int id)
        {
            try
            {
                var regiao = await _regiaoRepository.GetById(id);
                if (regiao == null)
                    return new Response<GetByIdRegiaoDto?>(null, 404, "Região não encontrada");

                var dto = _mapper.Map<GetByIdRegiaoDto>(regiao);
                return new Response<GetByIdRegiaoDto?>(dto);
            }
            catch (Exception ex)
            {
                return new Response<GetByIdRegiaoDto?>(null, 500, $"Erro ao buscar Região: {ex.Message}");
            }
        }

        public async Task<Response<UpdateRegiaoDto?>> Update(Regiao entidade)
        {
            try
            {
                var regiaoExistente = await _regiaoRepository.GetById(entidade.Id);

                if (regiaoExistente == null)
                    return new Response<UpdateRegiaoDto?>(null, 404, "Região não encontrada");

                await _regiaoRepository.Update(entidade);

                var dto = _mapper.Map<UpdateRegiaoDto>(entidade);
                return new Response<UpdateRegiaoDto?>(dto, 200, "Região atualizada com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<UpdateRegiaoDto?>(null, 500, $"Erro ao atualizar Região: {ex.Message}");
            }
        }
    }
}
