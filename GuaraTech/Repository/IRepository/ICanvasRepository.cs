using GuaraTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Repository.IRepository
{
    public interface ICanvasRepository
    {
        Task Create(Canvas canvas);
        Task Update(Canvas canvas);
        Task<Canvas> GetCanvasById(Guid id);
    }
}
