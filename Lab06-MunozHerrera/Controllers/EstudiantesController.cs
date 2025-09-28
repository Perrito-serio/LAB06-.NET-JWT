    using Lab06_MunozHerrera.Core.Interfaces;
    using Lab06_MunozHerrera.DTOs.Estudiante;
    using Lab06_MunozHerrera.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    namespace Lab06_MunozHerrera.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        [Authorize] // ¡Toda la clase requiere autenticación!
        public class EstudiantesController : ControllerBase
        {
            private readonly IUnitOfWork _unitOfWork;
            
            public EstudiantesController(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            [HttpGet]
            [Authorize(Roles = "Admin,Profesor")] // Solo Admins y Profesores pueden ver
            public async Task<IActionResult> GetAll()
            {
                var estudiantes = await _unitOfWork.Repository<Estudiante>().GetAllAsync();
                // Aquí usaríamos AutoMapper para convertir la lista a DTOs
                return Ok(estudiantes);
            }

            [HttpPost]
            [Authorize(Roles = "Admin")] // Solo Admins pueden crear
            public async Task<IActionResult> Post([FromBody] EstudianteRequestDto estudianteDto)
            {
                var estudiante = new Estudiante
                {
                    Nombre = estudianteDto.Nombre,
                    Edad = estudianteDto.Edad,
                    Direccion = estudianteDto.Direccion,
                    Telefono = estudianteDto.Telefono,
                    Correo = estudianteDto.Correo
                };
                
                await _unitOfWork.Repository<Estudiante>().AddAsync(estudiante);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction(nameof(GetAll), new { id = estudiante.IdEstudiante }, estudiante);
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
    
