using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GuaraTech.DTO;
using GuaraTech.Euns;
using GuaraTech.Models;
using GuaraTech.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GuaraTech.Controllers
{
    [Route("v1/Course")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _repCou;
        public CoursesController(ICourseRepository repCou)
        {
            _repCou = repCou;
        }

        [Route("/list")]
        [HttpGet]
        public async Task<IEnumerable<CourseDto>> ListCourse()
        {
            var courses = await _repCou.ListCourse();
            return courses;
        }

       
        [Route("/CreateCourse")]
        [HttpPost]
        public async Task<ActionResult> CreateCourse([FromBody]CourseDto courseDto)
        {
            if (!ModelState.IsValid) return NotFound();
            var course = new Course{ Title = courseDto.Title, Details = courseDto.Details, StateCourse = StateCourseEnuns.Disabled };
            await  _repCou.Create(course);
            return Ok();
        }

        // PUT api/<CourseController>/5
        [HttpPut("/edit")]
        public async Task<ActionResult> Put([FromBody] CourseDto courseDto)
        {
            var GetCourse = await _repCou.GetCourseById(courseDto.Id);

            if (GetCourse == null) return Ok(new { message = "Usuário não encontrado !" });

            await _repCou.Edit(courseDto);
            return Ok(new { message = "Salvo com sucesso !" });
        }

        // DELETE api/<CourseController>/5
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var GetCourse = await _repCou.GetCourseById(id);

            if (GetCourse == null) return Ok(new { message = "Usuário não encontrado !" });

            await _repCou.Delete(id);
            return Ok(new { message = "Salvo com sucesso !" });
        }
    }
}
