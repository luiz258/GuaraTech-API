using GuaraTech.DTO;
using GuaraTech.Infra.Repository.IRepository;
using GuaraTech.Models;
using GuaraTech.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Controllers
{
    public class TeamController : Controller
    {
        private readonly ICanvasRepository _repCanvas;
        private readonly IAccountRepository _repAccount;
        private readonly ICanvasTeamRepository _repTeam;
        private readonly IHttpContextAccessor _accessor;


        public TeamController(ICanvasRepository repCavas,
           
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
            _accessor = accessor;
        }


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
