using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Time_tracking_system.Domain.Entity;
using Time_tracking_system.DAL.Interfaces;

namespace Time_tracking_system.DAL.Repositories
{
    public class ValueRequestAttributesRepository : IValueRequestAttributesRepository
    {
        private readonly ApplicationDbContext _db;

        public ValueRequestAttributesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        #region Create
        public async Task<bool> Create(ValueRequestAttributes entity)
        {
            await _db.ValuesRequestsAttributes.AddAsync(entity);
            return await Save();
        }
        #endregion

        #region Delete
        public async Task<bool> Delete(ValueRequestAttributes entity)
        {
            _db.ValuesRequestsAttributes.Remove(entity);
            return await Save();
        }
        #endregion

        #region FindAll
        public async Task<ICollection<ValueRequestAttributes>> FindAll()
        {
            return await _db.ValuesRequestsAttributes
                .Include(x => x.Attributes_TypeRequest.Attributes)
                .Include(x => x.TypeVocation)
                .Include(x => x.Employee)
                .ToListAsync();
        }
        #endregion

        #region FindById
        public async Task<ValueRequestAttributes> FindById(int id)
        {
            return await _db.ValuesRequestsAttributes
                .Include(x => x.Attributes_TypeRequest.Attributes)
                .Include(x => x.TypeVocation)
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion

        #region FindByRequestId
        public async Task<ICollection<ValueRequestAttributes>> FindByRequestId(int requestId)
        {
            var valueRequestAttributes = await FindAll();
            return valueRequestAttributes
                .Where(x => x.Request_id == requestId).ToList();
        }
        #endregion

        #region isExists
        public async Task<bool> isExists(int id)
        {
            var exists = await _db.ValuesRequestsAttributes.AnyAsync(x => x.Id == id);
            return exists;
        }
        #endregion

        #region Save
        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0;
        }
        #endregion

        #region Update
        public async Task<bool> Update(ValueRequestAttributes entity)
        {
            _db.ValuesRequestsAttributes.Update(entity);
            return await Save();
        }
        #endregion

        #region NumberOfUnusedVacationDaysInTheCurrentYear
        public async Task<int> NumberOfUnusedVacationDaysInTheCurrentYear(string idApplicant)
        {
            var countDay = await _db.ValuesRequestsAttributes
                .Where(x => x.Request.Applicant_id == idApplicant)
                .Where(x => x.Request.StartDate.Year == DateTime.Now.Year)
                .Where(x => x.Request.StatusRequest.Name == "Утверждена")
                .Where(x => x.TypeVocation.Name == "Основной оплачиваемый отпуск")
                .SumAsync(x => x.Request.CountDayOnRequest);
            if (countDay == null)
                return 20;
            else
                return 20 - (int)countDay;
        }
        #endregion

        #region NumberOfUnusedVacationDaysInThePastYears
        public async Task<int> NumberOfUnusedVacationDaysInThePastYears(string idApplicant)
        {
            var countDay = await _db.ValuesRequestsAttributes
                .Where(x => x.Request.Applicant_id == idApplicant)
                .Where(x => x.Request.StartDate.Year < DateTime.Now.Year)
                .Where(x => x.Request.StatusRequest.Name == "Утверждена")
                .Where(x => x.TypeVocation.Name == "Основной оплачиваемый отпуск")
                .SumAsync(x => x.Request.CountDayOnRequest);
            var list = await _db.ValuesRequestsAttributes
                .Where(x => x.Request.Applicant_id == idApplicant)
                .Where(x => x.Request.StartDate.Year < DateTime.Now.Year)
                .Where(x => x.Request.StatusRequest.Name == "Утверждена")
                .Where(x => x.TypeVocation.Name == "Основной оплачиваемый отпуск")
                .ToListAsync();
            var countUniqueYears = list.GroupBy(x => x.Request.StartDate.Year).Select(g => g.First()).Count();

            return (int)((20 * countUniqueYears) - countDay);
        }
        #endregion
    }
}
