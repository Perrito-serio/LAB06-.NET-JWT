using AutoMapper;
using Lab06_MunozHerrera.Core.Interfaces;
using Lab06_MunozHerrera.DTOs.Estudiante;
using Lab06_MunozHerrera.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab06_MunozHerrera.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Toda la clase requiere autenticación
    public class EstudiantesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EstudiantesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Profesor")] // Solo Admins y Profesores pueden ver la lista
        public async Task<IActionResult> GetAll()
        {
            var estudiantes = await _unitOfWork.Repository<Estudiante>().GetAllAsync();
            var estudiantesDto = _mapper.Map<IEnumerable<EstudianteResponseDto>>(estudiantes);
            return Ok(estudiantesDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] // Solo Admins pueden crear estudiantes
        public async Task<IActionResult> Post([FromBody] EstudianteRequestDto estudianteDto)
        {
            var estudiante = _mapper.Map<Estudiante>(estudianteDto);
            
            await _unitOfWork.Repository<Estudiante>().AddAsync(estudiante);
            await _unitOfWork.CompleteAsync();

            var responseDto = _mapper.Map<EstudianteResponseDto>(estudiante);
            return CreatedAtAction(nameof(GetAll), new { id = responseDto.IdEstudiante }, responseDto);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Solo Admins pueden borrar
        public async Task<IActionResult> Delete(int id)
        {
            var estudiante = await _unitOfWork.Repository<Estudiante>().GetByIdAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }

            _unitOfWork.Repository<Estudiante>().Remove(estudiante);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
