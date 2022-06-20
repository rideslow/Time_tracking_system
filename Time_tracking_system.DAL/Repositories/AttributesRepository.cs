using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Time_tracking_system.DAL;
using Time_tracking_system.Domain.Entity;
using Time_tracking_system.DAL.Interfaces;

namespace Time_tracking_system.DAL.Repositories
{
    public class AttributesRepository : IAttributesRepository
    {
        private readonly ApplicationDbContext _db;

        public AttributesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        #region Create
        public async Task<bool> Create(Attributes entity)
        {
            await _db.Attributes.AddAsync(entity);
            return await Save();
        }
        #endregion

        #region Delete
        public async Task<bool> Delete(Attributes entity)
        {
            _db.Attributes.Remove(entity);
            return await Save();
        }
        #endregion

        #region FindAll
        public async Task<ICollection<Attributes>> FindAll()
        {
            return await _db.Attributes
                .ToListAsync();
        }
        #endregion

        #region FindById
        public async Task<Attributes> FindById(int id)
        {
            return await _db.Attributes
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
        public async Task<bool> Update(Attributes entity)
        {
            _db.Attributes.Update(entity);
            return await Save();
        }
        #endregion
    }
}
