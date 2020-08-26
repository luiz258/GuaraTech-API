using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuaraTech.DTO;
using GuaraTech.Repository.IRepository;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GuaraTech.Controllers
{
    [Route("v1/Course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _repCou;
        public CourseController(ICourseRepository repCou)
        {
            _repCou = repCou;
        }

        [Route("/list")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CourseController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        
        [Route("/CreateCourse")]
        [HttpPost]
        public async Task<ActionResult> CreateCourse([FromBody]CourseDto course) {

         await  _repCou.Create(course);
          return Ok();
        }

        // PUT api/<CourseController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CourseController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
