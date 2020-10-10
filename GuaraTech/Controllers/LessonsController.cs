using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using GuaraTech.DTO;
using GuaraTech.Euns;
using GuaraTech.Models;
using GuaraTech.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;


namespace GuaraTech.Controllers
{
   
    public class LessonsController : Controller
    {
        private readonly ILessonRepository _repLessons;
        public LessonsController(ILessonRepository repLessons)
        {
            _repLessons = repLessons;
        }

        [Route("v1/lessons/listLessons{idCourses}")]
        [HttpGet]
        public async Task<IEnumerable<LessonDto>> ListLessons(Guid idCourses)
        {
            var lessons = await _repLessons.ListLessons(idCourses);
            return lessons;
        }

        [Route("v1/lessons/create")]
        [HttpPost]
        public async Task<ActionResult> CreateLesson([FromBody] LessonDto lessonDto)
        {
            var lesson = new Lesson { IdCourse = lessonDto.IdCourse,
                Title = lessonDto.Title,
                Datails = lessonDto.Datails,
                Duration = lessonDto.Duration,
                OrderVideo = lessonDto.OrderVideo,
                UrlVideo = lessonDto.UrlVideo };

            await _repLessons.Create(lesson);

            return Ok(new { message = "Salvo com suceeso " });

        }

        [Route("v1/lessons/delete/{id}")]
        [HttpPost]
        public async Task<ActionResult> DeleteLesson(Guid id)
        {

            try
            {
                await _repLessons.Delete(id);

                return Ok(new { message = "Salvo com suceeso " });
            }
            catch(Exception e)
            {

                return NotFound(new { message = "Error" });
            }
        }
    }
}
