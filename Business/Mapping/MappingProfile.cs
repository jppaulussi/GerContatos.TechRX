using AutoMapper;
using Core.Dto.Contato;
using Core.Dto.DDD;
using Core.Dto.Regiao;
using Core.Dto.Role;
using Core.Dto.TipoTelefone;
using Core.Dto.Usuarios;
using Core.Entities;
using Core.Request.Contact;
using Core.Request.Region;
using Core.Request.User;
using Model.Dtos.Request.Token;
using Model.Dtos.Request.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GetUsuarioTokenRequest, Usuario>();

        //mapeamento dos requests que vem da api
        CreateMap<CreateUserRequest, Usuario>();
        CreateMap<Usuario, CreateUserRequest>();


        CreateMap<UpdateUserRequest, Usuario>();
        CreateMap<DeleteUserRequest, Usuario>();
        CreateMap<GetUserByIdRequest, Usuario>();
        CreateMap<GetAllRequest, Usuario>();

        CreateMap<CreateContactRequest, Contato>().ForMember(dest => dest.Ddd, opt => opt.Ignore())
    .ForMember(dest => dest.TipoTelefone, opt => opt.Ignore())
    .ForMember(dest => dest.Usuario, opt => opt.Ignore());

        CreateMap<UpdateContactRequest, Contato>();
        CreateMap<DeleteContactRequest, Contato>();
        CreateMap<GetByIdContactRequest, Contato>();
        CreateMap<GetAllContactRequest, Contato>();


        CreateMap<CreateRegionRequest, Regiao>();
        CreateMap<UpdateRegionRequest, Regiao>();
        CreateMap<DeleteRegionRequest, Regiao>();
        CreateMap<GetByIdRegionRequest, Regiao>();
        CreateMap<GetAllRegionRequest, Regiao>();


        CreateMap<GetAllByTokenDto, Usuario>();



        // dtos para os serviços para enviar de volta para a api

        CreateMap<Usuario, CreateUsuarioDto>();
        CreateMap<Usuario, UpdateUsuarioDto>();
        CreateMap<Usuario, DeleteUsuarioDto>();
        CreateMap<Usuario, GetAllUsuarioDto>();
        CreateMap<Usuario, UsuarioGetByIdDto>();

        CreateMap<Contato, CreateContatoDto>();
        CreateMap<Contato, UpdateContatoDto>();

        CreateMap<Contato, DeleteContatoDto>();
        CreateMap<Contato, GetByIdContatoDto>();
        CreateMap<Contato, GetAllContatoDto>();






        CreateMap<Regiao, CreateRegiaoDto>().ReverseMap();
        CreateMap<Regiao, UpdateRegiaoDto>().ReverseMap();
        CreateMap<Regiao, DeleteRegiaoDto>().ReverseMap();
        CreateMap<Regiao, GetAllRegiaoDto>().ReverseMap();
        CreateMap<Regiao, GetByIdRegiaoDto>().ReverseMap();


        CreateMap<DDD, CreateDDDDto>();
        CreateMap<CreateDDDDto, DDD>(); 
        CreateMap<DDD, UpdateDDDDto>();
        CreateMap<DDD, DeleteDDDDto>();
        CreateMap<DDD, GetAllDDDDto>();
        CreateMap<DDD, GetByIdRoleDto>();
        CreateMap<DDD, GetByIdDDDDto>();
        CreateMap<GetByIdDDDDto, DDD>();

        CreateMap<Role, CreateRoleDto>();
        CreateMap<Role, UpdateRoleDto>();
        CreateMap<Role, DeleteRoleDto>();
        CreateMap<Role, GetAllRoleDto>();
        CreateMap<Role, GetByIdRoleDto>();



       // CreateMap<TipoTelefone, CreateTipoTelefoneDto>();
        CreateMap<TipoTelefone, UpdateTipoTelefoneDto>();
        CreateMap<TipoTelefone, DeleteTipoTelefoneDto>();
        CreateMap<TipoTelefone, GetAllTipoTelefoneDto>();
        CreateMap<TipoTelefone, GetByIdTipoTelefoneDto>();
        CreateMap<TipoTelefone, CreateTipoTelefoneDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo));
        CreateMap<CreateTipoTelefoneDto, TipoTelefone>();





        CreateMap<Usuario, GetAllByTokenDto>();
    }
}
