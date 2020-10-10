using GuaraTech.DTO;
using GuaraTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Repository.IRepository
{
    public interface ILessonRepository
    {
        Task Create(Lesson lessons);
        Task Delete(Guid id);
        Task<IEnumerable<LessonDto>> ListLessons(Guid CourseId);
    }
}
