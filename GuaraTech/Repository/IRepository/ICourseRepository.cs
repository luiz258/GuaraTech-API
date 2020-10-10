using GuaraTech.DTO;
using GuaraTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Repository.IRepository
{
    public interface ICourseRepository
    {
       Task Create(Course course);
       Task Edit(CourseDto course);
       Task Delete(Guid id);
       Task<IEnumerable<CourseDto>> ListCourse();
       Task<CourseDto> GetCourseById(Guid Id);
    }
}
