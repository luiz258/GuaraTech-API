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

        public Task UpdateCommunicationChannels(Canvas canvas)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCost(Canvas canvas)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCustomerRelationship(Canvas canvas)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCustomerSegment(Canvas canvas)
        {
            throw new NotImplementedException();
        }

        public Task UpdateKeyFeatures(Canvas canvas)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMainActivities(Canvas canvas)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePartnerships(Canvas canvas)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRecipe(Canvas canvas)
        {
            throw new NotImplementedException();
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
