using Core.Interfaces.Services;
using Core.Entities;
using Core.Responses;
using Core.Dto.TipoTelefone;
using AutoMapper;
using Core.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Interfaces.Repositories;

namespace GerContatos.API.Services
{
    public class TipoTelefoneService : ITipoTelefoneService
    {
        private readonly IRepository<TipoTelefone> _tipoTelefoneRepository;
        private readonly IMapper _mapper;

        public TipoTelefoneService(IRepository<TipoTelefone> tipoTelefoneRepository, IMapper mapper)
        {
            _tipoTelefoneRepository = tipoTelefoneRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IList<GetAllTipoTelefoneDto>>> GetAll()
        {
            var tipoTelefones = await _tipoTelefoneRepository.GetAll();
            var tipoTelefoneDtos = _mapper.Map<IList<GetAllTipoTelefoneDto>>(tipoTelefones);
            return new PagedResponse<IList<GetAllTipoTelefoneDto>>(tipoTelefoneDtos);
        }

        public async Task<Response<GetByIdTipoTelefoneDto?>> GetById(int id)
        {
            var tipoTelefone = await _tipoTelefoneRepository.GetById(id);
            if (tipoTelefone == null)
            {
                return new Response<GetByIdTipoTelefoneDto?>(default, 404, "TipoTelefone not found");
            }
            var tipoTelefoneDto = _mapper.Map<GetByIdTipoTelefoneDto>(tipoTelefone);
            return new Response<GetByIdTipoTelefoneDto?>(tipoTelefoneDto);
        }

        public async Task<Response<CreateTipoTelefoneDto?>> Create(TipoTelefone entidade)
        {
            if (entidade == null)
            {
                return new Response<CreateTipoTelefoneDto?>(default, 400, "TipoTelefone cannot be null");
            }

            // Validar se o Tipo é válido
            if (!Enum.IsDefined(typeof(ETipoTelefone), entidade.Tipo))
            {
                return new Response<CreateTipoTelefoneDto?>(default, 400, "Invalid Tipo value");
            }

            await _tipoTelefoneRepository.Create(entidade);
            var tipoTelefoneDto = _mapper.Map<CreateTipoTelefoneDto>(entidade);
            return new Response<CreateTipoTelefoneDto?>(tipoTelefoneDto, 201);
        }

        public async Task<Response<UpdateTipoTelefoneDto?>> Update(TipoTelefone entidade)
        {
            if (entidade == null)
            {
                return new Response<UpdateTipoTelefoneDto?>(default, 400, "TipoTelefone cannot be null");
            }

            // Validar se o Tipo é válido
            if (!Enum.IsDefined(typeof(ETipoTelefone), entidade.Tipo))
            {
                return new Response<UpdateTipoTelefoneDto?>(default, 400, "Invalid Tipo value");
            }

            var existingTipoTelefone = await _tipoTelefoneRepository.GetById(entidade.Id);
            if (existingTipoTelefone == null)
            {
                return new Response<UpdateTipoTelefoneDto?>(default, 404, "TipoTelefone not found");
            }

            existingTipoTelefone.Tipo = entidade.Tipo;
            await _tipoTelefoneRepository.Update(existingTipoTelefone);
            var tipoTelefoneDto = _mapper.Map<UpdateTipoTelefoneDto>(existingTipoTelefone);
            return new Response<UpdateTipoTelefoneDto?>(tipoTelefoneDto);
        }

        public async Task<Response<DeleteTipoTelefoneDto?>> Delete(int id)
        {
            var tipoTelefone = await _tipoTelefoneRepository.GetById(id);
            if (tipoTelefone == null)
            {
                return new Response<DeleteTipoTelefoneDto?>(default, 404, "TipoTelefone not found");
            }

            await _tipoTelefoneRepository.Delete(id);
            var tipoTelefoneDto = _mapper.Map<DeleteTipoTelefoneDto>(tipoTelefone);
            return new Response<DeleteTipoTelefoneDto?>(tipoTelefoneDto, 204);
        }
    }
}
