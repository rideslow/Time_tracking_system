using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Time_tracking_system.Domain.Entity;
using Time_tracking_system.DAL.Interfaces;

namespace Time_tracking_system.DAL.Repositories
{
    public class Attributes_TypeRequestRepository : IAttributes_TypeRequestRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IAttributesRepository _attributesRepo;

        public Attributes_TypeRequestRepository(
            ApplicationDbContext db,
            IAttributesRepository attributesRepo)
        {
            _db = db;
            _attributesRepo = attributesRepo;
        }

        #region Create
        public async Task<bool> Create(Attributes_TypeRequest entity)
        {
            await _db.Attributes_TypesRequests.AddAsync(entity);
            return await Save();
        }
        #endregion

        #region Delete
        public async Task<bool> Delete(Attributes_TypeRequest entity)
        {
            _db.Attributes_TypesRequests.Remove(entity);
            return await Save();
        }
        #endregion

        #region FindAll
        public async Task<ICollection<Attributes_TypeRequest>> FindAll()
        {
            return await _db.Attributes_TypesRequests
                .Include(x => x.TypeRequest)
                .Include(x => x.Attributes)
                .ToListAsync();
        }
        #endregion

        #region FindById
        public async Task<Attributes_TypeRequest> FindById(int id)
        {
            return await _db.Attributes_TypesRequests
                .Include(x => x.TypeRequest)
                .Include(x => x.Attributes)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion

        //получить тип данных по айди атрибута 
        #region GetTypeDataByIdAttributeTypeRequest
        public async Task<string> GetTypeDataByIdAttributeTypeRequest(int id)
        {
            var attributeTypeRequest = await FindById(id);
            var attribute = await _attributesRepo.FindById(attributeTypeRequest.Attr_id);
            return attribute.TypeData;
        }
        #endregion

        //Найти все записи по айди Типа заявки
        #region FindByIdTypeRequest
        public async Task<ICollection<Attributes_TypeRequest>> FindByIdTypeRequest(int id)
        {
            return await _db.Attributes_TypesRequests
                .Include(x => x.TypeRequest)
                .Include(x => x.Attributes)
                .Where(x => x.TypeRequest_id == id)
                .ToListAsync();
        }
        #endregion

        #region Save
        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0;
        }
        #endregion

        #region Update
        public async Task<bool> Update(Attributes_TypeRequest entity)
        {
            _db.Attributes_TypesRequests.Update(entity);
            return await Save();
        }
        #endregion
    }
}
