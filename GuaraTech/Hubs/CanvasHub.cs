using GuaraTech.DTO.Canvas.Postit;
using GuaraTech.Euns;
using GuaraTech.Infra.Repository.IRepository;
using GuaraTech.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Hubs
{
    public class CanvasHub:Hub
    {
        private readonly ICanvasPostitRepository _repPostit;
        public CanvasHub(ICanvasPostitRepository repPostit)
        {
            _repPostit = repPostit;
        }

        public async Task SendMessage(Guid idCanvas)
        {
            var list = await _repPostit.ListPostit(idCanvas);

            var problem = list.Where(x => x.TypeBlockCanvas.Equals(CanvasEnuns.Problem));
            var solution = list.Where(x => x.TypeBlockCanvas.Equals(CanvasEnuns.Solution));
            var keyMetrics = list.Where(x => x.TypeBlockCanvas.Equals(CanvasEnuns.KeyMetrics));
            var uniqueValueProposition = list.Where(x => x.TypeBlockCanvas.Equals(CanvasEnuns.UniqueValueProposition));
            var unfairAdvantage = list.Where(x => x.TypeBlockCanvas.Equals(CanvasEnuns.UnfairAdvantage));
            var channels = list.Where(x => x.TypeBlockCanvas.Equals(CanvasEnuns.Channels));
            var customerSegments = list.Where(x => x.TypeBlockCanvas.Equals(CanvasEnuns.CustomerSegments));
            var cost = list.Where(x => x.TypeBlockCanvas.Equals(CanvasEnuns.Cost));
            var revenue = list.Where(x => x.TypeBlockCanvas.Equals(CanvasEnuns.Revenue));


            var canvas = new ListCanvasByBlockDto();
            canvas.Problem.AddRange(problem);
            canvas.Solution.AddRange(solution);
            canvas.KeyMetrics.AddRange(keyMetrics);
            canvas.UniqueValueProposition.AddRange(uniqueValueProposition);
            canvas.UnfairAdvantage.AddRange(unfairAdvantage);
            canvas.Channels.AddRange(channels);
            canvas.CustomerSegments.AddRange(customerSegments);
            canvas.Cost.AddRange(cost);
            canvas.Revenue.AddRange(revenue);

            await Clients.All.SendAsync("Canvas", canvas);
        }

        public async Task PostCanvas(CanvasPostit entity)
        {
        
            await Clients.All.SendAsync("Postit", entity);

        }

    }
}
