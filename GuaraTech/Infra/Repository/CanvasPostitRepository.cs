using GuaraTech.Euns;
using GuaraTech.Infra.Repository.IRepository;
using GuaraTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace GuaraTech.Infra.Repository
{
    public class CanvasPostitRepository : ICanvasPostitRepository
    {
        private readonly DBContext _db;
        public CanvasPostitRepository(DBContext db)
        {
            _db = db;
        }
        public async Task Create(CanvasPostit postit)
        {
            var sql = "INSERT INTO CANVAS_POSTIT (Id, IdCanvas, DescriptionPostit, ColorPostit, CanvasTypeBlock, DateCreated) values (@Id, @IdCanvas, @DescriptionPostit, @ColorPostit, @CanvasTypeBlock, @DateCreated)";

            using (var conn = _db)
            {
                var comand = conn.Connection.Execute(sql, new { 
                    @Id = postit.Id, 
                    @IdCanvas = postit.IdCanvas, 
                    @DescriptionPostit = postit.Description, 
                    @ColorPostit = postit.PostitColor, 
                    @CanvasTypeBlock = postit.TypeBlockCanvas,
                    DateCreated = postit.DateCreated,
                });
            }
        }

        public async Task Delete(Guid id)
        {
            var sql = "DELETE FROM CANVAS_POSTIT WHERE Id = @Id";

            using (var conn = _db)
            {
                var comand = await conn.Connection.ExecuteAsync(sql, new
                {
                    @Id = id
                });
            }
        }

        public async Task Edit(CanvasPostit postit)
        {
            var sql = "UPDATE CANVAS_POSTIT SET IdCanvas = @IdCanvas, DescriptionPostit = @DescriptionPostit, ColorPostit = @ColorPostit, CanvasTypeBlock = @CanvasTypeBlock WHERE IdCanvas = @IdCanvas and Id = @Id";

            using (var conn = _db)
            {
                var comand = await conn.Connection.ExecuteAsync(sql, new
                {
                    @Id = postit.Id,
                    @IdCanvas = postit.IdCanvas,
                    @DescriptionPostit = postit.Description,
                    @ColorPostit = postit.PostitColor,
                    @CanvasTypeBlock = postit.TypeBlockCanvas,
                });
            }
        }

        public async Task<IEnumerable<CanvasPostit>> GetPostitByTypeBlock(Guid IdCanvas, 
            CanvasEnuns typeBlock)
        {
            var sql = "SELECT * FROM CANVAS_POSTIT WHERE IdCanvas = @IdCanvas and ORDERBY DateCreated ASC";

            using (var conn = _db)
            {
                return await conn.Connection.QueryFirstOrDefault(sql, new
                {
                    @IdCanvas = IdCanvas,
                    @CanvasTypeBlock = typeBlock,
                }).ToList();
            }
        }

        public async Task<IEnumerable<ListPostitDto>> ListPostit(Guid IdCanvas)
        {
            var sql = "SELECT * FROM CANVAS_POSTIT WHERE IdCanvas = @IdCanvas ORDER BY DateCreated ASC ";

            using (var conn = _db)
            {
                return await conn.Connection.QueryAsync<ListPostitDto>(sql, new
                {
                    @IdCanvas = IdCanvas,
                });
            }
        }
    }
}
