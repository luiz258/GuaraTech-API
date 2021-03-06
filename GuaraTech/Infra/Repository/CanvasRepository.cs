using Dapper;
using GuaraTech.DTO;
using GuaraTech.Infra;
using GuaraTech.Models;
using GuaraTech.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Repository
{
    public class CanvasRepository : ICanvasRepository
    {
        private readonly DBContext _db;
        public CanvasRepository(DBContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<CanvasCardListDto>> CanvasCardList(Guid id)
        {
            return await _db.Connection.QueryAsync<CanvasCardListDto>("SELECT C.ID, C.Title, AC.FullName  FROM CANVAS AS C, TEAM_CANVAS  AS TC, ACCOUNT AS AC WHERE TC.IdUserGuests = @id AND C.ID = TC.IdCanvas AND AC.ID = TC.IdUserGuests AND C.CanvasState = 0 ORDER BY C.DateCreated DESC ", new { @id = id});
        }

        public async Task Create(Canvas canvas)
        {
            await _db.Connection.ExecuteAsync("INSERT INTO CANVAS (IdUser, ID, Title, IsPrivate, CanvasState,  DateCreated ) VALUES (@IdUser, @ID, @Title, @IsPrivate, @CanvasState, @DateCreated)"
               , new {

                   @IdUser = canvas.UserId,
                   @ID = canvas.Id,
                   @Title = canvas.Title,
                   @IsPrivate = canvas.IsPrivate,
                   @CanvasState = canvas.CanvasState,
                   @DateCreated = canvas.DateCreated
               });
        }

        public async Task Delete(Guid IdCanvas)
        {
            using (var conn = _db)
            {
                await conn.Connection.ExecuteAsync("Update CANVAS set CanvasState=1 where ID = @Id", new { @Id = IdCanvas });
            }
        }

        public Task<Canvas> GetCanvasBlock(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<CanvasGetBlock> GetCanvasById(Guid id)
        {
            return await _db.Connection.QueryFirstOrDefaultAsync<CanvasGetBlock>("SELECT ID, IdUser, Title FROM CANVAS WHERE ID = @ID", new { @ID = id });
        }

        public async Task<CanvasDetailsDto> GetDetailsCanvas(Guid id)
        {
            
            return await _db.Connection.QueryFirstOrDefaultAsync<CanvasDetailsDto>("SELECT ID, IdUser, Title FROM CANVAS WHERE ID = @ID", new { @ID = id });
        }

        public async Task Update(Canvas canvas)
        {
            try
            {
                await _db.Connection.ExecuteAsync("Update CANVAS set Title=@Title where ID = @Id", new
                {
                    @Title = canvas.Title,
                    @Id = canvas.Id

                });
            }
            catch (Exception e)
            {

                throw;
            }
        }



    



     
    }
}
