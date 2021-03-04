using GuaraTech.DTO;
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
        Task Delete(Guid IdCanvas);
        Task<CanvasGetBlock> GetCanvasById(Guid id);
        Task<CanvasDetailsDto> GetDetailsCanvas(Guid id);
        Task<IEnumerable<CanvasCardListDto>> CanvasCardList(Guid id);
        Task<Canvas> GetCanvasBlock(Guid id);




    }
}

