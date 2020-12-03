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
        private readonly IHubContext<StreamingHub> _streaming;
        private readonly IHttpContextAccessor _accessor;
        public CanvasController(ICanvasRepository repCavas, 
            IHubContext<StreamingHub> streaming, 
            ICanvasTeamRepository repTeam,
            IAccountRepository repAccount,
            IHttpContextAccessor accessor,
            ICanvasRepository repCanvas
            )
        {
             _repTeam = repTeam;
            _repAccount = repAccount; 
            _repCanvas = repCanvas;
            _streaming = streaming;
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

            var canvas = new Canvas { Id = canvasDto.Id, UserId = id, Title = canvasDto.Title, CommunicationChannels = "", Cost =" ", CustomerRelationship =" ", CustomerSegment = " ", Description =" ", KeyFeatures = " ", MainActivities = " ", Partnerships = " ", Recipe = " ", ValueOffer= " " };
            var teamCanvas = new TeamCanvas { IdCanvas = canvasDto.Id, IdUserGuests = id };
            
            await _repCanvas.Create(canvas);
            await _repTeam.AddUserTeam(teamCanvas);
           
            return Ok(new { id= canvasDto.Id, message = "Criado com sucesso !", success = true });
        }


        [HttpPut]
        [Authorize]
        [Route("v1/canvas/value-offer")]
        public async Task<IActionResult> CanvasValueOffer([FromBody] CanvasEditDto canvasDto)
        {
            var Userid = _accessor.HttpContext.User.Claims.Where(a => a.Type == "Id").FirstOrDefault().Value;
            var id = Guid.Parse(Userid);

            var team = new TeamCanvas { IdCanvas = canvasDto.IdCanvas, IdUserGuests = id };
             var canvaslist = await _repCanvas.GetCanvasById(canvasDto.IdCanvas);
             if (canvaslist == null) return NotFound(new { message = "Canvas não existe!", success = false });
            
            var teamList = await _repTeam.SearchUsersCanvas(team);
            var validadeTeam = teamList.Where(x => x.IdUserGuests == id);

            if (validadeTeam.Equals(id)  || canvaslist.IdUser != id) return NotFound(new { message = "Usuario não faz parte do time !", success = false });

            var canvas = new Canvas { UserId = id, Id = canvasDto.IdCanvas,  Description = canvasDto.Description };

            await _repCanvas.UpdateValueOffer(canvas);
            return Ok(new { message = "Salvo com sucesso !", success = true });
        }
        [HttpPut]
        [Authorize]
        [Route("v1/canvas/customer-segment")]
        public async Task<IActionResult> CanvasCustomerSegment([FromBody] CanvasEditDto canvasDto)
        {
            var Userid = _accessor.HttpContext.User.Claims.Where(a => a.Type == "Id").FirstOrDefault().Value;
            var id = Guid.Parse(Userid);

            var team = new TeamCanvas { IdCanvas = canvasDto.IdCanvas, IdUserGuests = id };
            var canvaslist = await _repCanvas.GetCanvasById(canvasDto.IdCanvas);
            if (canvaslist == null) return NotFound(new { message = "Canvas não existe!", success = false });

            var teamList = await _repTeam.SearchUsersCanvas(team);
            var validadeTeam = teamList.Where(x => x.IdUserGuests == id);

            if (validadeTeam.Equals(id) || canvaslist.IdUser != id) return NotFound(new { message = "Usuario não faz parte do time !", success = false });

            var canvas = new Canvas { UserId = id, Id = canvasDto.IdCanvas, Description = canvasDto.Description };

            await _repCanvas.UpdateCustomerSegment(canvas);
            return Ok(new { message = "Salvo com sucesso !", success = true });
        }
        [HttpPut]
        [Authorize]
        [Route("v1/canvas/customer-relationship")]
        public async Task<IActionResult> CanvasCustomerRelationship([FromBody] CanvasEditDto canvasDto)
        {
            var Userid = _accessor.HttpContext.User.Claims.Where(a => a.Type == "Id").FirstOrDefault().Value;
            var id = Guid.Parse(Userid);

            var team = new TeamCanvas { IdCanvas = canvasDto.IdCanvas, IdUserGuests = id };
            var canvaslist = await _repCanvas.GetCanvasById(canvasDto.IdCanvas);
            if (canvaslist == null) return NotFound(new { message = "Canvas não existe!", success = false });

            var teamList = await _repTeam.SearchUsersCanvas(team);
            var validadeTeam = teamList.Where(x => x.IdUserGuests == id);

            if (validadeTeam.Equals(id) || canvaslist.IdUser != id) return NotFound(new { message = "Usuario não faz parte do time !", success = false });

            var canvas = new Canvas { UserId = id, Id = canvasDto.IdCanvas, Description = canvasDto.Description };

            await _repCanvas.UpdateCustomerRelationship(canvas);
            return Ok(new { message = "Salvo com sucesso !", success = true });
        }
        [HttpPut]
        [Authorize]
        [Route("v1/canvas/key-features")]
        public async Task<IActionResult> CanvasKeyFeatures([FromBody] CanvasEditDto canvasDto)
        {
            var Userid = _accessor.HttpContext.User.Claims.Where(a => a.Type == "Id").FirstOrDefault().Value;
            var id = Guid.Parse(Userid);

            var team = new TeamCanvas { IdCanvas = canvasDto.IdCanvas, IdUserGuests = id };
            var canvaslist = await _repCanvas.GetCanvasById(canvasDto.IdCanvas);
            if (canvaslist == null) return NotFound(new { message = "Canvas não existe!", success = false });

            var teamList = await _repTeam.SearchUsersCanvas(team);
            var validadeTeam = teamList.Where(x => x.IdUserGuests == id);

            if (validadeTeam.Equals(id) || canvaslist.IdUser != id) return NotFound(new { message = "Usuario não faz parte do time !", success = false });

            var canvas = new Canvas { UserId = id, Id = canvasDto.IdCanvas, Description = canvasDto.Description };

            await _repCanvas.UpdateKeyFeatures(canvas);
            return Ok(new { message = "Salvo com sucesso !", success = true });
        }
        [HttpPut]
        [Authorize]
        [Route("v1/canvas/main-activities")]
        public async Task<IActionResult> CanvasMainActivities([FromBody] CanvasEditDto canvasDto)
        {
            var Userid = _accessor.HttpContext.User.Claims.Where(a => a.Type == "Id").FirstOrDefault().Value;
            var id = Guid.Parse(Userid);

            var team = new TeamCanvas { IdCanvas = canvasDto.IdCanvas, IdUserGuests = id };
            var canvaslist = await _repCanvas.GetCanvasById(canvasDto.IdCanvas);
            if (canvaslist == null) return NotFound(new { message = "Canvas não existe!", success = false });

            var teamList = await _repTeam.SearchUsersCanvas(team);
            var validadeTeam = teamList.Where(x => x.IdUserGuests == id);

            if (validadeTeam.Equals(id) || canvaslist.IdUser != id) return NotFound(new { message = "Usuario não faz parte do time !", success = false });

            var canvas = new Canvas { UserId = id, Id = canvasDto.IdCanvas, Description = canvasDto.Description };

            await _repCanvas.UpdateMainActivities(canvas);
            return Ok(new { message = "Salvo com sucesso !", success = true });
        }
        [HttpPut]
        [Authorize]
        [Route("v1/canvas/canvas-partnerships")]
        public async Task<IActionResult> CanvasPartnerships([FromBody] CanvasEditDto canvasDto)
        {
            var Userid = _accessor.HttpContext.User.Claims.Where(a => a.Type == "Id").FirstOrDefault().Value;
            var id = Guid.Parse(Userid);

            var team = new TeamCanvas { IdCanvas = canvasDto.IdCanvas, IdUserGuests = id };
            var canvaslist = await _repCanvas.GetCanvasById(canvasDto.IdCanvas);
            if (canvaslist == null) return NotFound(new { message = "Canvas não existe!", success = false });

            var teamList = await _repTeam.SearchUsersCanvas(team);
            var validadeTeam = teamList.Where(x => x.IdUserGuests == id);

            if (validadeTeam.Equals(id) || canvaslist.IdUser != id) return NotFound(new { message = "Usuario não faz parte do time !", success = false });

            var canvas = new Canvas { UserId = id, Id = canvasDto.IdCanvas, Description = canvasDto.Description };

            await _repCanvas.UpdatePartnerships(canvas);
            return Ok(new { message = "Salvo com sucesso !", success = true });
        }
        [HttpPut]
        [Authorize]
        [Route("v1/canvas/recipe")]
        public async Task<IActionResult> CanvasRecipe([FromBody] CanvasEditDto canvasDto)
        {
            var Userid = _accessor.HttpContext.User.Claims.Where(a => a.Type == "Id").FirstOrDefault().Value;
            var id = Guid.Parse(Userid);

            var team = new TeamCanvas { IdCanvas = canvasDto.IdCanvas, IdUserGuests = id };
            var canvaslist = await _repCanvas.GetCanvasById(canvasDto.IdCanvas);
            if (canvaslist == null) return NotFound(new { message = "Canvas não existe!", success = false });

            var teamList = await _repTeam.SearchUsersCanvas(team);
            var validadeTeam = teamList.Where(x => x.IdUserGuests == id);

            if (validadeTeam.Equals(id) || canvaslist.IdUser != id) return NotFound(new { message = "Usuario não faz parte do time !", success = false });

            var canvas = new Canvas { UserId = id, Id = canvasDto.IdCanvas, Description = canvasDto.Description };

            await _repCanvas.UpdateRecipe(canvas);
            return Ok(new { message = "Salvo com sucesso !", success = true });
        }

        [HttpPut]
        [Authorize]
        [Route("v1/canvas/cost")]
        public async Task<IActionResult> CanvasCost([FromBody] CanvasEditDto canvasDto)
        {
            var Userid = _accessor.HttpContext.User.Claims.Where(a => a.Type == "Id").FirstOrDefault().Value;
            var id = Guid.Parse(Userid);

            if (id == null) return NotFound(new { message = "Usuario não cadastrado!", success = false });

            var team = new TeamCanvas { IdCanvas = canvasDto.IdCanvas, IdUserGuests = id };
            var canvaslist = await _repCanvas.GetCanvasById(canvasDto.IdCanvas);
            if (canvaslist == null) return NotFound(new { message = "Canvas não existe!", success = false });

            var teamList = await _repTeam.SearchUsersCanvas(team);
            var validadeTeam = teamList.Where(x => x.IdUserGuests == id);

            if (validadeTeam.Equals(id) || canvaslist.IdUser != id) return NotFound(new { message = "Usuario não faz parte do time !", success = false });

            var canvas = new Canvas { UserId = id, Id = canvasDto.IdCanvas, Description = canvasDto.Description };

            await _repCanvas.UpdateCost(canvas);
            return Ok(new { message = "Salvo com sucesso !", success = true });
        }

        [HttpPut]
        [Authorize]
        [Route("v1/canvas/communication-channels")]
        public async Task<IActionResult> CanvasCommunicationChannels([FromBody] CanvasEditDto canvasDto)
        {
            var Userid = _accessor.HttpContext.User.Claims.Where(a => a.Type == "Id").FirstOrDefault().Value;
            var id = Guid.Parse(Userid);

            var team = new TeamCanvas { IdCanvas = canvasDto.IdCanvas, IdUserGuests = id };
            var canvaslist = await _repCanvas.GetCanvasById(canvasDto.IdCanvas);
            if (canvaslist == null) return NotFound(new { message = "Canvas não existe!", success = false });

            var teamList = await _repTeam.SearchUsersCanvas(team);
            var validadeTeam = teamList.Where(x => x.IdUserGuests == id);

            if (validadeTeam.Equals(id) || canvaslist.IdUser != id) return NotFound(new { message = "Usuario não faz parte do time !", success = false });

            var canvas = new Canvas { UserId = id, Id = canvasDto.IdCanvas, Description = canvasDto.Description };

            await _repCanvas.UpdateCommunicationChannels(canvas);
            return Ok(new { message = "Salvo com sucesso !", success = true });
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
 