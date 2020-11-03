using Dapper;
using GuaraTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Infra.Repository.IRepository
{
    public class CanvasTeamRepository : ICanvasTeamRepository
    {
        private readonly DBContext _db;
        public CanvasTeamRepository(DBContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<TeamCanvas>> SearchUsersCanvas(TeamCanvas team)
        {
            return await _db.Connection.QueryAsync<TeamCanvas>("SELECT * FROM TEAM_CANVAS where @IdCanvas = IdCanvas AND IdUserGuests = @IdUserGuests ", new
            {
                @IdUserGuests = team.IdUserGuests,
                @IdCAnvas = team.IdCanvas
            });
        }
        public async Task AddUserTeam(TeamCanvas team)
        {
             await _db.Connection.ExecuteAsync("INSERT INTO TEAM_CANVAS (ID, IdCanvas, IdUserGuests) VALUES  (@ID, @IdCanvas, @IdUserGuests)", new
            {
                 @ID = team.Id,
                 @IdCanvas = team.IdCanvas,
                 @IdUserGuests = team.IdUserGuests
             });
        }

        public async Task DeleteUserTheTeam(TeamCanvas team)
        {
            await _db.Connection.ExecuteAsync("DELETE FROM TEAM_CANVAS WHERE IdCanvas = @IdCanvas and IdUserGuests = @IdUserGuests", new { @IdCanvas= team.IdCanvas, @IdUserGuests = team.IdUserGuests });
        }

        public async Task<IEnumerable<TeamCanvas>> GetCanvas(Guid IdCanvas)
        {
            return await _db.Connection.QueryAsync<TeamCanvas>("SELECT * FROM TEAM_CANVAS where @IdCanvas = IdCanvas ", new
            {
                @IdCAnvas = IdCanvas
            });
        }
    }
}
