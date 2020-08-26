using GuaraTech.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Repository.IRepository
{
    public interface ICourseRepository
    {
        Task Create(CourseDto course);
       Task<IEnumerable<CourseDto>> ListCourse();
    }
}
