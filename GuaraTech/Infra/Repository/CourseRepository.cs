using Dapper;
using GuaraTech.DTO;
using GuaraTech.Infra;
using GuaraTech.Models;
using GuaraTech.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DBContext _db;

        public CourseRepository(DBContext db)
        {
            _db = db;
        }
        public Task Create(Course course)
        {
            return _db.Connection.ExecuteAsync("INSERT INTO COURSE (Id, Title, Details, StateCourse) VALUES (@Id, @Title, @Details, @StateCourse)",
                new {
                    @Id = course.Id,
                    @Title = course.Title,
                    @Details = course.Details,
                    @StateCourse = course.StateCourse
                });
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Edit(CourseDto course)
        {
            throw new NotImplementedException();
        }

        public Task<CourseDto> GetCourseById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CourseDto>> ListCourse()
        {
            return _db.Connection.QueryAsync<CourseDto>("SELECT * FROM COURSE");
        }
    }
}
