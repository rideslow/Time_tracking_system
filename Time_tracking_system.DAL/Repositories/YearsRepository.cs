using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Time_tracking_system.Domain.Entity;
using Time_tracking_system.DAL.Interfaces;

namespace Time_tracking_system.DAL.Repositories
{
    public class YearsRepository : IYearsRepository
    {
        private readonly ApplicationDbContext _db;

        public YearsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        #region Create
        public async Task<bool> Create(Years entity)
        {
            await _db.Years.AddAsync(entity);
            return await Save();
        }
        #endregion

        #region FindAll
        public async Task<ICollection<Years>> FindAll()
        {
            return await _db.Years
                .ToListAsync();
        }
        #endregion

        #region FindByYear
        public async Task<Years> FindByYear(string year)
        {
            return await _db.Years
                .FirstOrDefaultAsync(x => x.Year == year);
        }
        #endregion

        #region FindById
        public async Task<Years> FindById(int id)
        {
            return await _db.Years
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion

        #region isExistsYear
        public async Task<bool> isExistsYear(string year)
        {
            return await _db.Years
                .AnyAsync(x => x.Year == year);
        }
        #endregion

        #region isExists
        public async Task<bool> isExists(int id)
        {
            return await _db.Years
                .AnyAsync(x => x.Id == id);
        }
        #endregion

        #region Save
        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0;
        }
        #endregion

        #region Update
        public async Task<bool> Update(Years entity)
        {
            _db.Years.Update(entity);
            return await Save();
        }
        #endregion

        public Task<bool> Delete(Years entity)
        {
            throw new NotImplementedException();
        }
        
    }
}
