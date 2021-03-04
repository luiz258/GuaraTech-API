using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuaraTech.DTO;
using GuaraTech.Euns;
using GuaraTech.Hubs;
using GuaraTech.Infra.Repository.IRepository;
using GuaraTech.Models;
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
        private readonly IHubContext<StreamingHub> _streaming;
        private readonly IHttpContextAccessor _accessor;
        public CanvasController(ICanvasRepository repCavas, 
            IHubContext<StreamingHub> streaming, 
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
            _streaming = streaming;
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

            var canvas = new Canvas { Id = canvasDto.Id, UserId = id, Title = canvasDto.Title};
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
            
        var canvas = new CanvasPostit { Id = Guid.NewGuid(), IdCanvas = entity.IdCanvas, Description = entity.Description, PostitColor = entity.PostitColor, TypeBlockCanvas =  entity.TypeBlockCanvas};

            await _repPostit.Create(canvas);
            return Ok(new { id = canvas.Id, message = "Criado com sucesso !", success = true });
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

            var canvas = new CanvasPostit { Id = entity.Id, IdCanvas = entity.IdCanvas, Description = entity.Description, PostitColor = entity.PostitColor, TypeBlockCanvas = entity.TypeBlockCanvas };

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
        [Authorize]
        public async Task<IEnumerable<ListPostitDto>> ListPostitCanvas(Guid IdCanvas)
        {
            var list = await _repPostit.ListPostit(IdCanvas);
            return list;
        }

        //Team Business

        [HttpGet]
        [Authorize]
        [Route("v1/canvas/team")]
        public async Task<IEnumerable<TeamCanvas>> GetTeam(TeamCanvas team)
        {
            var getCanvas = await _repTeam.SearchUsersCanvas(team);
            return getCanvas;
        }

        [HttpPost]
        [Authorize]
        [Route("v1/canvas/add-team")]
        public async Task<IActionResult> AddUserOnTeam([FromBody] TeamCanvasDto teamDto)
        {
            var Userid = _accessor.HttpContext.User.Claims.Where(a => a.Type == "Id").FirstOrDefault().Value;
            var id = Guid.Parse(Userid);

            var team = new TeamCanvas { IdCanvas = teamDto.IdCanvas, IdUserGuests = id };
            
              var checkQuantityMembersOnCanvas = await _repTeam.GetCanvas(team.IdCanvas);
              var CheckUserTeam = await _repTeam.SearchUsersCanvas(team);

            if (CheckUserTeam.Count() == 1) return Ok(new { message = "Esse membro ja está no time!", success = false });
            if (checkQuantityMembersOnCanvas.Count() > 5) return Ok(new { message = "O time só pode counter até 5 membros !", success = false });

            await _repTeam.AddUserTeam(team);

            return Ok(new { message = "Salvo com sucesso !", success = true });

        }

    }
}
 