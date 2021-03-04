using GuaraTech.Euns;
using GuaraTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Infra.Repository.IRepository
{
    public interface ICanvasPostitRepository
    {
        Task Create(CanvasPostit course);
        Task Edit(CanvasPostit course);
        Task Delete(Guid id);
        Task<IEnumerable<CanvasPostit>> GetPostitByTypeBlock(Guid IdCanvas, CanvasEnuns typeBlock);
        Task<IEnumerable<ListPostitDto>> ListPostit(Guid IdCanvas);
       
    }
}
