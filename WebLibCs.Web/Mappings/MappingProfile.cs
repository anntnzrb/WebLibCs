using AutoMapper;
using WebLibCs.Core.DTOs;
using WebLibCs.Core.Entities;
using WebLibCs.Web.ViewModels.Autor;
using WebLibCs.Web.ViewModels.Libro;

namespace WebLibCs.Web.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Entity to DTO mappings
        CreateMap<Autor, AutorDto>()
            .ForMember(dest => dest.LibrosCount, opt => opt.MapFrom(src => src.Libros.Count));

        CreateMap<Libro, LibroDto>()
            .ForMember(dest => dest.AutorNombre, opt => opt.MapFrom(src => src.Autor.Nombre));

        // DTO to Entity mappings
        CreateMap<CreateAutorDto, Autor>();
        CreateMap<UpdateAutorDto, Autor>();
        CreateMap<CreateLibroDto, Libro>();
        CreateMap<UpdateLibroDto, Libro>();

        // ViewModel to DTO mappings
        CreateMap<CreateAutorViewModel, CreateAutorDto>();
        CreateMap<EditAutorViewModel, UpdateAutorDto>();
        CreateMap<CreateLibroViewModel, CreateLibroDto>();
        CreateMap<EditLibroViewModel, UpdateLibroDto>();

        // DTO to ViewModel mappings
        CreateMap<AutorDto, EditAutorViewModel>()
            .ForMember(dest => dest.ImagenRutaActual, opt => opt.MapFrom(src => src.ImagenRuta));
        CreateMap<LibroDto, EditLibroViewModel>()
            .ForMember(dest => dest.ImagenRutaActual, opt => opt.MapFrom(src => src.ImagenRuta));
        CreateMap<LibroDto, DeleteLibroViewModel>();
        CreateMap<LibroDto, LibroDetailsViewModel>();
        CreateMap<AutorDto, DeleteAutorViewModel>();
        CreateMap<AutorDto, DetailsAutorViewModel>();

        // PagedResult mappings
        CreateMap<WebLibCs.Core.Common.PagedResult<Libro>, WebLibCs.Core.DTOs.PagedResultDto<LibroDto>>();
    }
}