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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GuaraTech.Controllers
{
    public class CanvasController : Controller
    {
        private readonly ICanvasRepository _repCavas;
        private readonly ICanvasTeamRepository _repTeam;
        private readonly IHubContext<StreamingHub> _streaming;
        public CanvasController(ICanvasRepository repCavas, IHubContext<StreamingHub> streaming, ICanvasTeamRepository repTeam)
        {
             _repTeam = repTeam; 
            _repCavas = repCavas;
            _streaming = streaming;
        }

        [HttpPost]
        [Route("v1/canvas/create")]
       public async Task<IActionResult> CreateCanvas([FromBody] CanvasCreateDto canvasDto)
        {

            var canvas = new Canvas { Id = canvasDto.Id, UserId = canvasDto.UserId, Title = canvasDto.Title };
             await _repCavas.Create(canvas);
           
            return Ok(new { message = "Salvo com sucesso !" });
        }

        [HttpPut]
        [Route("v1/canvas/edit")]
        public async Task<IActionResult> EditCanvas([FromBody] CanvasEditDto canvasDto)
        {
            var canvaslist = await _repCavas.GetCanvasById(canvasDto.Id);
            if (canvaslist == null) return NotFound(new { message = "Canvas não existe!", success = false });

            var canvasBlock = new CanvasBlock {  Description = canvasDto.Description };
            var canvas = new Canvas { UserId = canvasDto.IdUser, Id = canvasDto.Id, Description = canvasDto.Description };

            await _repCavas.Update(canvas);
            return Ok(new { message = "Salvo com sucesso !", success = true });
        }

        [HttpPut]
        [Route("v1/canvas/value-offer")]
        public async Task<IActionResult> CanvasValueOffer([FromBody] CanvasEditDto canvasDto)
        {
              var team = new TeamCanvas { IdCanvas = canvasDto.Id, IdUserGuests = canvasDto.IdUser };
             var canvaslist = await _repCavas.GetCanvasById(canvasDto.Id);
             if (canvaslist == null) return NotFound(new { message = "Canvas não existe!", success = false });
            
            var teamList = await _repTeam.SearchUsersCanvas(team);
            var validadeTeam = teamList.Where(x => x.IdUserGuests == canvasDto.IdUser);

            if (validadeTeam.Equals(canvasDto.IdUser)  || canvaslist.IdUser != canvasDto.IdUser) return NotFound(new { message = "Usuario não faz parte do time !", success = false });

            var canvas = new Canvas { UserId = canvasDto.IdUser, Id = canvasDto.Id,  Description = canvasDto.Description };

            await _repCavas.UpdateValueOffer(canvas);
            return Ok(new { message = "Salvo com sucesso !", success = true });
        }
        [HttpPut]
        [Route("v1/canvas/customer-segment")]
        public async Task<IActionResult> CanvasCustomerSegment([FromBody] CanvasEditDto canvasDto)
        {
            // var canvaslist = await _repCavas.GetCanvasById(canvasDto.Id);
            ///if (canvaslist == null) return NotFound(new { message = "Canvas não existe!", success = false });

           // var canvasBlock = new CanvasBlock { Block = canvasDto.block.ToString(), Description = canvasDto.Description };
            var canvas = new Canvas { UserId = canvasDto.IdUser, Id = canvasDto.Id };

            await _repCavas.Update(canvas);
            return Ok(new { message = "Salvo com sucesso !", success = true });
        }
        [HttpPut]
        [Route("v1/canvas/CustomerRelationship")]
        public async Task<IActionResult> CanvasCustomerRelationship([FromBody] CanvasEditDto canvasDto)
        {
            // var canvaslist = await _repCavas.GetCanvasById(canvasDto.Id);
            ///if (canvaslist == null) return NotFound(new { message = "Canvas não existe!", success = false });

          //  var canvasBlock = new CanvasBlock { Block = canvasDto.block.ToString(), Description = canvasDto.Description };
            var canvas = new Canvas { UserId = canvasDto.IdUser, Id = canvasDto.Id };

            await _repCavas.Update(canvas);
            return Ok(new { message = "Salvo com sucesso !", success = true });
        }
        [HttpPut]
        [Route("v1/canvas/CanvasKeyFeatures")]
        public async Task<IActionResult> CanvasKeyFeatures([FromBody] CanvasEditDto canvasDto)
        {
            // var canvaslist = await _repCavas.GetCanvasById(canvasDto.Id);
            ///if (canvaslist == null) return NotFound(new { message = "Canvas não existe!", success = false });

      //      var canvasBlock = new CanvasBlock { Block = canvasDto.block.ToString(), Description = canvasDto.Description };
            var canvas = new Canvas { UserId = canvasDto.IdUser, Id = canvasDto.Id };

            await _repCavas.Update(canvas);
            return Ok(new { message = "Salvo com sucesso !", success = true });
        }
        [HttpPut]
        [Route("v1/canvas/CanvasMainActivities")]
        public async Task<IActionResult> CanvasMainActivities([FromBody] CanvasEditDto canvasDto)
        {
            // var canvaslist = await _repCavas.GetCanvasById(canvasDto.Id);
            ///if (canvaslist == null) return NotFound(new { message = "Canvas não existe!", success = false });

       ///     var canvasBlock = new CanvasBlock { Block = canvasDto.block.ToString(), Description = canvasDto.Description };
            var canvas = new Canvas { UserId = canvasDto.IdUser, Id = canvasDto.Id };

            await _repCavas.Update(canvas);
            return Ok(new { message = "Salvo com sucesso !", success = true });
        }
        [HttpPut]
        [Route("v1/canvas/CanvasPartnerships")]
        public async Task<IActionResult> CanvasPartnerships([FromBody] CanvasEditDto canvasDto)
        {
            // var canvaslist = await _repCavas.GetCanvasById(canvasDto.Id);
            ///if (canvaslist == null) return NotFound(new { message = "Canvas não existe!", success = false });

         ///   var canvasBlock = new CanvasBlock { Block = canvasDto.block.ToString(), Description = canvasDto.Description };
            var canvas = new Canvas { UserId = canvasDto.IdUser, Id = canvasDto.Id };

            await _repCavas.Update(canvas);
            return Ok(new { message = "Salvo com sucesso !", success = true });
        }
        [HttpPut]
        [Route("v1/canvas/CanvasRecipe")]
        public async Task<IActionResult> CanvasRecipe([FromBody] CanvasEditDto canvasDto)
        {
            // var canvaslist = await _repCavas.GetCanvasById(canvasDto.Id);
            ///if (canvaslist == null) return NotFound(new { message = "Canvas não existe!", success = false });

        ///    var canvasBlock = new CanvasBlock { Block = canvasDto.block.ToString(), Description = canvasDto.Description };
            var canvas = new Canvas { UserId = canvasDto.IdUser, Id = canvasDto.Id };

            await _repCavas.Update(canvas);
            return Ok(new { message = "Salvo com sucesso !", success = true });
        }

        [HttpPut]
        [Route("v1/canvas/CanvasCost")]
        public async Task<IActionResult> CanvasCost([FromBody] CanvasEditDto canvasDto)
        {
            // var canvaslist = await _repCavas.GetCanvasById(canvasDto.Id);
            ///if (canvaslist == null) return NotFound(new { message = "Canvas não existe!", success = false });

          ///  var canvasBlock = new CanvasBlock { Block = canvasDto.block.ToString(), Description = canvasDto.Description };
            var canvas = new Canvas { UserId = canvasDto.IdUser, Id = canvasDto.Id };

            await _repCavas.Update(canvas);
            return Ok(new { message = "Salvo com sucesso !", success = true });
        }


        [HttpGet]
        [Route("v1/canvas/block/{block}/{Id}")]
        public async Task<CanvasGetBlock> GetCanvasById(Guid id)
        {
            var getCanvas = await _repCavas.GetCanvasById(id);
            return getCanvas;
        }

        //Team Business

        [HttpGet]
        [Route("v1/canvas/team")]
        public async Task<IEnumerable<TeamCanvas>> GetTeam(TeamCanvas team)
        {
            var getCanvas = await _repTeam.SearchUsersCanvas(team);
            return getCanvas;
        }

        [HttpPost]
        [Route("v1/canvas/add-team")]
        public async Task<IActionResult> AddUserOnTeam([FromBody] TeamCanvasDto teamDto)
        {
            var team = new TeamCanvas { IdCanvas = teamDto.IdCanvas, IdUserGuests = teamDto.IdUser };
            
              var checkQuantityMembersOnCanvas = await _repTeam.GetCanvas(team.IdCanvas);
              var CheckUserTeam = await _repTeam.SearchUsersCanvas(team);

            if (CheckUserTeam.Count() == 1) return Ok(new { message = "Esse membro ja está no time!", success = false });
            if (checkQuantityMembersOnCanvas.Count() > 5) return Ok(new { message = "O time só pode counter até 5 membros !", success = false });

            await _repTeam.AddUserTeam(team);

            return Ok(new { message = "Salvo com sucesso !", success = true });

        }

    }
}
 