using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using GuaraTech.DTO;
using GuaraTech.DTO.Canvas.Postit;
using GuaraTech.Euns;
using GuaraTech.Hubs;
using GuaraTech.Infra.Repository.IRepository;
using GuaraTech.Models;
using GuaraTech.Models.Euns;
using GuaraTech.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GuaraTech.Controllers
{
    public class CanvasController : Controller
    {
        private readonly ICanvasRepository _repCanvas;
        private readonly IAccountRepository _repAccount;
        private readonly ICanvasTeamRepository _repTeam;
        private readonly ICanvasPostitRepository _repPostit;
        protected readonly IHubContext<CanvasHub> _canvasHub;
        private readonly IHttpContextAccessor _accessor;
        public CanvasController(ICanvasRepository repCavas, 
            [NotNull] IHubContext<CanvasHub> canvasHub, 
            ICanvasTeamRepository repTeam,
            IAccountRepository repAccount,
            IHttpContextAccessor accessor,
            ICanvasRepository repCanvas,
            ICanvasPostitRepository repPostit
            )
        {
             _repTeam = repTeam;
            _repAccount = repAccount; 
            _repCanvas = repCanvas;
            _canvasHub = canvasHub;
            _repPostit = repPostit;
            _accessor = accessor;
        }


        [HttpGet]
        [Route("v1/canvas-get/{idCanvas}")]
        [Authorize]
        public async Task<CanvasDetailsDto> GetCanvasById(Guid idCanvas)
        {
            var getCanvasById = await _repCanvas.GetDetailsCanvas(idCanvas);
            return getCanvasById;
        }

        [HttpGet]
        [Route("v1/canvas-card-list")]
        [Authorize]
        public async Task<IEnumerable<CanvasCardListDto>> CanvasCardList()
        {
            var id = Guid.Parse(_accessor.HttpContext.User.Claims.Where(a => a.Type == "Id").FirstOrDefault().Value);
            var getCanvas = await _repCanvas.CanvasCardList(id);
            return getCanvas;
        }



        [HttpPost]
        [Route("v1/canvas/create")]
        [Authorize]
        public async Task<IActionResult> CreateCanvas([FromBody] CanvasCreateDto canvasDto)
        {
            //var b = User.Claims;
             //var UserId = User.Claims.Where(x => x.Type == "nameid").FirstOrDefault().Value;
            var Userid = _accessor.HttpContext.User.Claims.Where(a =>a.Type == "Id").FirstOrDefault().Value;
            var id = Guid.Parse(Userid);

            var user = await _repAccount.GetUserById(id);

            if (user == null) return NotFound(new { message = "Usuario não cadastrado!", success = false });

            var canvas = new Canvas { Id = canvasDto.Id, UserId = id, Title = canvasDto.Title, CanvasState = ECanvasState.listed, IsPrivate = true, DateCreated = DateTime.Now, DescriptionCanvas = canvasDto.DescriptionCanvas};
            var teamCanvas = new TeamCanvas { IdCanvas = canvasDto.Id, IdUserGuests = id };
            
            await _repCanvas.Create(canvas);
            await _repTeam.AddUserTeam(teamCanvas);
           
            return Ok(new { id= canvasDto.Id, message = "Criado com sucesso !", success = true });
        }

        [HttpPut]
        [Route("v1/canvas/edit")]
        [Authorize]
        public async Task<IActionResult> EditCanvas([FromBody] CanvasEditDto entity)
        {
            var Userid = _accessor.HttpContext.User.Claims.Where(a => a.Type == "Id").FirstOrDefault().Value;
            var id = Guid.Parse(Userid);

            var user = await _repAccount.GetUserById(id);

            if (user == null) return NotFound(new { message = "Usuario não cadastrado!", success = false });

            var canvas = new Canvas { Id = entity.IdCanvas, UserId = id, Title = entity.Title };
         
            await _repCanvas.Update(canvas);

            return Ok(new { message = "Salvo com sucesso !", success = true });
        }

        [HttpDelete]
        [Route("v1/canvas/delete/{IdCanvas}")]
        [Authorize]
        public async Task<IActionResult> DeleteCanvas(Guid IdCanvas)
        {
            var Userid = _accessor.HttpContext.User.Claims.Where(a => a.Type == "Id").FirstOrDefault().Value;
            var id = Guid.Parse(Userid);

            var user = await _repAccount.GetUserById(id);

            if (user == null) return NotFound(new { message = "Usuario não cadastrado!", success = false });

            await _repCanvas.Delete(IdCanvas);
          
            return Ok(new { message = "Criado com sucesso !", success = true });
        }



        [HttpPost]
        [Route("v1/canvas/create-postit")]
        [Authorize]
        public async Task<IActionResult> CreatePostitCanvas([FromBody] CreatedPostitCanvasDto entity)
        {
            var Userid = _accessor.HttpContext.User.Claims.Where(a => a.Type == "Id").FirstOrDefault().Value;
                var id = Guid.Parse(Userid);

            var user = await _repAccount.GetUserById(id);
                if (user == null) return NotFound(new { message = "Usuario não cadastrado!", success = false });
            
        var canvas = new CanvasPostit { Id = Guid.NewGuid(), IdCanvas = entity.IdCanvas, DescriptionPostit = entity.Description, ColorPostit = entity.PostitColor, CanvasTypeBlock = (CanvasEnuns)entity.TypeBlockCanvas, DateCreated = DateTime.Now};
          
            await _repPostit.Create(canvas);
            //await _canvasHub.Clients.All.SendAsync("Postit", canvas);   

            return Ok(new { id = canvas.Id, message = "Criado com sucesso !", success = true, canvas = canvas });
        }


        [HttpPut]
        [Route("v1/canvas/edit-postit")]
        [Authorize]
        public async Task<IActionResult> EditPostitCanvas([FromBody] CreatedPostitCanvasDto entity)
        {
            var Userid = _accessor.HttpContext.User.Claims.Where(a => a.Type == "Id").FirstOrDefault().Value;
            var id = Guid.Parse(Userid);

            var user = await _repAccount.GetUserById(id);
            if (user == null) return NotFound(new { message = "Usuario não cadastrado!", success = false });

            var canvas = new CanvasPostit { Id = entity.Id, IdCanvas = entity.IdCanvas, DescriptionPostit = entity.Description, ColorPostit = entity.PostitColor, CanvasTypeBlock = (CanvasEnuns)entity.TypeBlockCanvas };

            await _repPostit.Edit(canvas);
            return Ok(new { message = "Salvo com sucesso!", success = true });
        }

        [HttpDelete]
        [Route("v1/canvas/delete-postit/{IdPostit}")]
        [Authorize]
        public async Task<IActionResult> DeltePostitCanvas(Guid IdPostit)
        {
            var Userid = _accessor.HttpContext.User.Claims.Where(a => a.Type == "Id").FirstOrDefault().Value;
            var id = Guid.Parse(Userid);

            var user = await _repAccount.GetUserById(id);
            if (user == null) return NotFound(new { message = "Usuario não cadastrado!", success = false });
            
            await _repPostit.Delete(IdPostit);
            return Ok(new { message = "Deletado com sucesso!", success = true });
        }

        [HttpGet]
        [Route("v1/canvas/get-postit/{IdCanvas}/{typeCanvas}")]
        [Authorize]
        public async Task<IEnumerable<CanvasPostit>> GetPostitCanvas( Guid IdCanvas, CanvasEnuns typeCanvas)
        {

            return await _repPostit.GetPostitByTypeBlock(IdCanvas, typeCanvas);
        }

        [HttpGet]
        [Route("v1/canvas/list-postit/{IdCanvas}")]
        public async Task<IEnumerable<ListPostitDto>> ListPostitCanvas(Guid IdCanvas)
        {
            var list = await _repPostit.ListPostit(IdCanvas);

            return list;
        }

        [HttpGet]
        [Route("v2/canvas/list-postit/{IdCanvas}")]
        public async Task<IActionResult> ListPostitCanvasV2(Guid IdCanvas)
        {
            var list = await _repPostit.ListPostit(IdCanvas);

            var problem = list.Where(x => x.CanvasTypeBlock.Equals(CanvasEnuns.Problem));
            var solution = list.Where(x => x.CanvasTypeBlock.Equals((int)CanvasEnuns.Solution));
            var keyMetrics = list.Where(x => x.CanvasTypeBlock.Equals((int)CanvasEnuns.KeyMetrics));
            var uniqueValueProposition = list.Where(x => x.CanvasTypeBlock.Equals((int)CanvasEnuns.UniqueValueProposition));
            var unfairAdvantage = list.Where(x => x.CanvasTypeBlock.Equals((int)CanvasEnuns.UnfairAdvantage));
            var channels = list.Where(x => x.CanvasTypeBlock.Equals(CanvasEnuns.Channels));
            var customerSegments = list.Where(x => x.CanvasTypeBlock.Equals((int)CanvasEnuns.CustomerSegments));
            var cost = list.Where(x => x.CanvasTypeBlock.Equals((int)CanvasEnuns.Cost));
            var revenue = list.Where(x => x.CanvasTypeBlock.Equals((int)CanvasEnuns.Revenue));


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

            //await _canvasHub.Clients.All.SendAsync("Canvas", canvas);

            return Ok(
                new { canvas = list }
                );
        }
    }
}
 