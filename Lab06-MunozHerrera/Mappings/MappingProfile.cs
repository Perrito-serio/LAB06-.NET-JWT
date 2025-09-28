using AutoMapper;
using Lab06_MunozHerrera.DTOs.Estudiante;
using Lab06_MunozHerrera.Models;

namespace Lab06_MunozHerrera.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeo de Entidad a DTO de Respuesta
            CreateMap<Estudiante, EstudianteResponseDto>();

            // Mapeo de DTO de Petición a Entidad
            CreateMap<EstudianteRequestDto, Estudiante>();
        }
    }
}