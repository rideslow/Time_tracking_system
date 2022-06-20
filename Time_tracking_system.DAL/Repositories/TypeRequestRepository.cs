using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Time_tracking_system.Domain.Entity;
using Time_tracking_system.DAL.Interfaces;

namespace Time_tracking_system.DAL.Repositories
{
    public class TypeRequestRepository : ITypeRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public TypeRequestRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        #region Create
        public async Task<bool> Create(TypeRequest entity)
        {
            await _db.TypesRequests.AddAsync(entity);
            return await Save();
        }
        #endregion

        #region Delete
        public async Task<bool> Delete(TypeRequest entity)
        {
            _db.TypesRequests.Remove(entity);
            return await Save();
        }
        #endregion

        #region FindAll
        public async Task<ICollection<TypeRequest>> FindAll()
        {
            return await _db.TypesRequests.ToListAsync();
        }
        #endregion

        #region FindById
        public async Task<TypeRequest> FindById(int id)
        {
            return await _db.TypesRequests
               .FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion

        #region Save
        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0;
        }
        #endregion

        #region Update
        public async Task<bool> Update(TypeRequest entity)
        {
            _db.TypesRequests.Update(entity);
            return await Save();
        }
        #endregion
    }
}
