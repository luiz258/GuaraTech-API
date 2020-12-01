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
            return await _db.Connection.QueryAsync<CanvasCardListDto>("SELECT C.ID, C.Title, AC.FullName  FROM CANVAS AS C, TEAM_CANVAS , ACCOUNT AS AC WHERE C.IdUser = @id AND AC.ID = @id OR TEAM_CANVAS.IdUserGuests = @id AND AC.ID = @id", new { @id = id});
        }

        public async Task Create(Canvas canvas)
        {
            await _db.Connection.ExecuteAsync("INSERT INTO CANVAS (IdUser, ID, Title ) VALUES (@IdUser, @ID, @Title  )"
               , new {
                   @IdUser = canvas.UserId,
                   @ID = canvas.Id,
                   @Title = canvas.Title
               });
        }

        public Task<Canvas> GetCanvasBlock(Guid id)
        {
            throw new NotImplementedException();
        }

    

        public async Task<CanvasGetBlock> GetCanvasById(Guid id)
        {
             return await _db.Connection.QueryFirstOrDefaultAsync<CanvasGetBlock>("SELECT ID, IdUser,Title, ValueOffer, CustomerSegment, CommunicationChannels, CustomerRelationship, KeyFeatures, MainActivities, Partnerships, Recipe, Cost FROM CANVAS WHERE ID = @ID", new { @ID = id });
        }

        public async Task<CanvasDetailsDto> GetDetailsCanvas(Guid id)
        {
            
            return await _db.Connection.QueryFirstOrDefaultAsync<CanvasDetailsDto>("SELECT ID, Title, ValueOffer, CustomerSegment, CommunicationChannels, CustomerRelationship, KeyFeatures, MainActivities, Partnerships, Recipe, Cost FROM CANVAS WHERE ID = @ID", new { @ID = id });
        }

        public async Task Update(Canvas canvas)
        {
            try
            {
                await _db.Connection.ExecuteAsync("Update CANVAS set @Block=@Description where ID = @Id", new
                {
                    @Block = canvas,
                    @Description = canvas,
                    @Id = canvas.Id

                });
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task UpdateCommunicationChannels(Canvas canvas)
        {
            await _db.Connection.ExecuteAsync("Update CANVAS set CommunicationChannels=@Description where ID = @Id", new
            {
                @Description = canvas.Description,
                @Id = canvas.Id

            });
        }

        public async Task UpdateCost(Canvas canvas)
        {
            await _db.Connection.ExecuteAsync("Update CANVAS set Cost=@Description where ID = @Id", new
            {
                @Description = canvas.Description,
                @Id = canvas.Id

            });
        }

        public async Task UpdateCustomerRelationship(Canvas canvas)
        {
            await _db.Connection.ExecuteAsync("Update CANVAS set CustomerRelationship=@Description where ID = @Id", new
            {
                @Description = canvas.Description,
                @Id = canvas.Id

            });
        }

        public async Task UpdateCustomerSegment(Canvas canvas)
        {
            await _db.Connection.ExecuteAsync("Update CANVAS set CustomerSegment=@Description where ID = @Id", new
            {
                @Description = canvas.Description,
                @Id = canvas.Id

            });
        }

        public async Task UpdateKeyFeatures(Canvas canvas)
        {
            await _db.Connection.ExecuteAsync("Update CANVAS set KeyFeatures=@Description where ID = @Id", new
            {
                @Description = canvas.Description,
                @Id = canvas.Id

            });
        }

        public async Task UpdateMainActivities(Canvas canvas)
        {
            await _db.Connection.ExecuteAsync("Update CANVAS set MainActivities=@Description where ID = @Id", new
            {
                @Description = canvas.Description,
                @Id = canvas.Id

            });
        }

        public async Task UpdatePartnerships(Canvas canvas)
        {
            await _db.Connection.ExecuteAsync("Update CANVAS set Partnerships=@Description where ID = @Id", new
            {
                @Description = canvas.Description,
                @Id = canvas.Id

            });
        }

        public async Task UpdateRecipe(Canvas canvas)
        {
            await _db.Connection.ExecuteAsync("Update CANVAS set Recipe=@Description where ID = @Id", new
            {
                @Description = canvas.Description,
                @Id = canvas.Id

            });
        }

        public async Task UpdateValueOffer(Canvas canvas)
        {
            await _db.Connection.ExecuteAsync("Update CANVAS set ValueOffer=@Description where ID = @Id", new
            {
                @Description = canvas.Description,
                @Id = canvas.Id

            });
        }
    }
}
