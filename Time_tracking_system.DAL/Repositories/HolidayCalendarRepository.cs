using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Time_tracking_system.Domain.Entity;
using Time_tracking_system.DAL.Interfaces;

namespace Time_tracking_system.DAL.Repositories
{
    public class HolidayCalendarRepository : IHolidayCalendarRepository
    {
        private readonly ApplicationDbContext _db;

        public HolidayCalendarRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        #region Create
        public async Task<bool> Create(HolidayCalendar entity)
        {
            await _db.HolidaysCalendar.AddAsync(entity);
            return await Save();
        }
        #endregion

        #region Delete
        public async Task<bool> Delete(HolidayCalendar entity)
        {
            _db.HolidaysCalendar.Remove(entity);
            //Перестаем отслеживать объект года, иначе будет возникать ошибка в отслеживании
            //и мы не сможем удалить сразу несколько записей подряд 
            _db.Entry<Years>(entity.Years).State = EntityState.Detached;
            return await Save();
        }
        #endregion

        #region FindAll
        public async Task<ICollection<HolidayCalendar>> FindAll()
        {
            return await _db.HolidaysCalendar
                .Include(x => x.Years)
                .ToListAsync();
        }
        #endregion

        #region FindAllByIdYear
        public async Task<ICollection<HolidayCalendar>> FindAllByIdYear(int idYear)
        {
            return await _db.HolidaysCalendar
                .Where(x => x.Years_id == idYear).ToListAsync();
        }
        #endregion

        #region NumberOfHolidaysInTheDateRange
        public async Task<int> NumberOfHolidaysInTheDateRange(DateTime startDate, DateTime endDate)
        {
            return await _db.HolidaysCalendar
                .Where(x => x.HolidayDate >= startDate && x.HolidayDate <= endDate)
                .CountAsync();
        }
        #endregion
        

        #region Save
        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0;
        }
        #endregion
    }
}
