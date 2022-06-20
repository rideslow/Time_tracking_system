using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Time_tracking_system.Domain.Entity;
using Time_tracking_system.DAL.Interfaces;

namespace Time_tracking_system.DAL.Repositories
{
    public class TypeVocationRepository : ITypeVocationRepository
    {
        private readonly ApplicationDbContext _db;

        public TypeVocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        #region Create
        public async Task<bool> Create(TypeVocation entity)
        {
            await _db.TypesVocations.AddAsync(entity);
            return await Save();
        }
        #endregion

        #region Delete
        public async Task<bool> Delete(TypeVocation entity)
        {
            _db.TypesVocations.Remove(entity);
            return await Save();
        }
        #endregion

        #region FindAll
        public async Task<ICollection<TypeVocation>> FindAll()
        {
            return await _db.TypesVocations.ToListAsync();
        }
        #endregion

        #region FindById
        public async Task<TypeVocation> FindById(int id)
        {
            return await _db.TypesVocations
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
        public async Task<bool> Update(TypeVocation entity)
        {
            _db.TypesVocations.Update(entity);
            return await Save();
        }
        #endregion
    }
}
