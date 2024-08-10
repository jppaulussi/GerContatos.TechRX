using AutoMapper;
using Core.Dto.DDD;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerContatos.API.Services
{
    public class DDDService : IDDDService
    {
        private readonly IDDDRepository _dddRepository;
        private readonly IMapper _mapper;

        public DDDService(IDDDRepository dddRepository, IMapper mapper)
        {
            _dddRepository = dddRepository;
            _mapper = mapper;
        }

        public async Task<Response<CreateDDDDto?>> Create(DDD entidade)
        {
            try
            {
                await _dddRepository.Create(entidade);
                var dto = _mapper.Map<CreateDDDDto>(entidade);
                return new Response<CreateDDDDto?>(dto, 201, "DDD criado com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<CreateDDDDto?>(null, 500, $"Erro ao criar DDD: {ex.Message}");
            }
        }

        public async Task<Response<DeleteDDDDto?>> Delete(int id)
        {
            try
            {
                var dddExistente = await _dddRepository.GetById(id);

                if (dddExistente == null)
                    return new Response<DeleteDDDDto?>(null, 404, "DDD não encontrado");

                await _dddRepository.Delete(id);
                return new Response<DeleteDDDDto?>(null, 202, "DDD excluído com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<DeleteDDDDto?>(null, 500, $"Erro ao excluir DDD: {ex.Message}");
            }
        }

        public async Task<PagedResponse<IList<GetAllDDDDto>>> GetAll()
        {
            try
            {
                var ddds = await _dddRepository.GetAll();
                var dtoList = _mapper.Map<IList<GetAllDDDDto>>(ddds);
                var count = dtoList.Count;

                return new PagedResponse<IList<GetAllDDDDto>>(dtoList, count, 1, 25);
            }
            catch (Exception ex)
            {
                return new PagedResponse<IList<GetAllDDDDto>>(null, 500, $"Erro ao buscar todos os DDDs: {ex.Message}");
            }
        }

        public async Task<Response<GetByIdDDDDto?>> GetById(int id)
        {
            try
            {
                var ddd = await _dddRepository.GetById(id);
                if (ddd == null)
                    return new Response<GetByIdDDDDto?>(null, 404, "DDD não encontrado");

                var dto = _mapper.Map<GetByIdDDDDto>(ddd);
                return new Response<GetByIdDDDDto?>(dto);
            }
            catch (Exception ex)
            {
                return new Response<GetByIdDDDDto?>(null, 500, $"Erro ao buscar DDD: {ex.Message}");
            }
        }

        public async Task<Response<UpdateDDDDto?>> Update(DDD entidade)
        {
            try
            {
                var dddExistente = await _dddRepository.GetById(entidade.Id);

                if (dddExistente == null)
                    return new Response<UpdateDDDDto?>(null, 404, "DDD não encontrado");

                await _dddRepository.Update(entidade);

                var dto = _mapper.Map<UpdateDDDDto>(entidade);
                return new Response<UpdateDDDDto?>(dto, 200, "DDD atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<UpdateDDDDto?>(null, 500, $"Erro ao atualizar DDD: {ex.Message}");
            }
        }
    }
}
