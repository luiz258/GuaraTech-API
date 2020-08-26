using GuaraTech.DTO;
using GuaraTech.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Repository
{
    public class CourseRepository : ICourseRepository
    {
        public Task Create(CourseDto course)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CourseDto>> ListCourse()
        {
            throw new NotImplementedException();
        }
    }
}
