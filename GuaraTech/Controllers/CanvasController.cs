using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuaraTech.Models;
using GuaraTech.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace GuaraTech.Controllers
{
    public class CanvasController : Controller
    {
        private readonly ICanvasRepository _repCavas;
        public CanvasController(ICanvasRepository repCavas)
        {
            _repCavas = repCavas;
        }

        [Route("v1/canvas/create")]
        public async Task<IActionResult> CreateCanvas([FromBody] Canvas canvas)
        {
            await _repCavas.Create(canvas);
            return Ok(new { message = "Salvo com sucesso !" });
        }

        [Route("v1/canvas/edit")]
        public async Task<IActionResult> EditCanvas([FromBody] Canvas canvas)
        {
            var canvaslist = await _repCavas.GetCanvasById(canvas.Id);
            if (canvaslist == null) return NotFound();

            await _repCavas.Update(canvas);
            return Ok(new { message = "Salvo com sucesso !" });
        }

        [Route("v1/canvas/edit")]
        public async Task<Canvas> Canvas(Guid id)
        {
            var getCanvas = await _repCavas.GetCanvasById(id);
            return getCanvas;
        }
    }
}
