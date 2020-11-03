using GuaraTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Infra.Repository.IRepository
{
    public interface ICanvasTeamRepository
    {
        //team management
        Task AddUserTeam(TeamCanvas team);
        Task DeleteUserTheTeam(TeamCanvas team);
        Task<IEnumerable<TeamCanvas>> SearchUsersCanvas(TeamCanvas team);
        Task<IEnumerable<TeamCanvas>> GetCanvas(Guid IdCanvas);
    }
}
