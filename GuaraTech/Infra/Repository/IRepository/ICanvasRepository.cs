using GuaraTech.DTO;
using GuaraTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Repository.IRepository
{
    public interface ICanvasRepository
    {
        Task Create(Canvas canvas);
        Task Update(Canvas canvas);
        Task<CanvasGetBlock> GetCanvasById(Guid id);
        Task<Canvas> GetCanvasBlock(Guid id);


        Task UpdateValueOffer(Canvas canvas);
        Task UpdateCustomerSegment(Canvas canvas);
        Task UpdateCommunicationChannels(Canvas canvas);
        Task UpdateCustomerRelationship(Canvas canvas);
        Task UpdateKeyFeatures(Canvas canvas);
        Task UpdateMainActivities(Canvas canvas);
        Task UpdatePartnerships(Canvas canvas);
        Task UpdateRecipe(Canvas canvas);
        Task UpdateCost(Canvas canvas);


    }
}

