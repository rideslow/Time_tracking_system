using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Time_tracking_system.Domain.Entity;
using Time_tracking_system.DAL.Interfaces;

namespace Time_tracking_system.DAL.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public RequestRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        #region Create
        public async Task<bool> Create(Request entity)
        {
            await _db.Requests.AddAsync(entity);
            return await Save();
        }
        #endregion

        #region Delete
        public async Task<bool> Delete(Request entity)
        {
            _db.Requests.Remove(entity);
            return await Save();
        }
        #endregion

        #region FindAll
        public async Task<ICollection<Request>> FindAll()
        {
            return await _db.Requests
                .Include(x => x.TypeRequest)
                .Include(x => x.Applicant)
                .Include(x => x.Cordinating)
                .Include(x => x.StatusRequest)
                .ToListAsync();
        }
        #endregion

        #region FindById
        public async Task<Request> FindById(int id)
        {
            return await _db.Requests
                .Include(x => x.TypeRequest)
                .Include(x => x.Applicant)
                .Include(x => x.Cordinating)
                .Include(x => x.StatusRequest)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion

        #region FindByIdApplicant
        public async Task<ICollection<Request>> FindByIdApplicant(string idApplicant)
        {
            return await _db.Requests
                .Include(x => x.TypeRequest)
                .Include(x => x.Applicant)
                .Include(x => x.Cordinating)
                .Include(x => x.StatusRequest)
                .Where(x => x.Applicant_id == idApplicant).ToListAsync();
        }
        #endregion

        #region FindAllRequestsWithoutStatusNew
        public async Task<ICollection<Request>> FindAllRequestsWithoutStatusNew()
        {
            return await _db.Requests
                .Include(x => x.TypeRequest)
                .Include(x => x.Applicant)
                .Include(x => x.Cordinating)
                .Include(x => x.StatusRequest)
                .Where(x => x.StatusRequest.Name != "Новая").ToListAsync();
        }
        #endregion

        #region isExists
        public async Task<bool> isExists(int id)
        {
            var exists = await _db.Requests.AnyAsync(x => x.Id == id);
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
        public async Task<bool> Update(Request entity)
        {
            _db.Requests.Update(entity);
            return await Save();
        }
        #endregion

        #region NumberOfDaysOffInTheCurrentYear
        public async Task<int> NumberOfDaysOffInTheCurrentYear(string idApplicant)
        {
            var countDay = await _db.Requests
                .Where(x => x.Applicant_id == idApplicant)
                .Where(x => x.StartDate.Year == DateTime.Now.Year)
                .Where(x => x.StatusRequest.Name != "Утверждена")
                .Where(x => x.TypeRequest.Name == "Заявка на отгул")
                .SumAsync(x => x.CountDayOnRequest);
            if (countDay == null)
                return 0;
            else
                return (int)countDay;
        }
        #endregion

        #region CountRequestWithStatusByIdApplicant
        public async Task<int> CountRequestWithStatusByIdApplicant(string idApplicant, string status)
        {
            return await _db.Requests
                .Where(x => x.Applicant_id == idApplicant)
                .Where(x => x.StatusRequest.Name == status)
                .CountAsync();
        }
        #endregion

        #region FindAllRequestsThatIncludeDate
        public async Task<ICollection<Request>> FindAllRequestsThatIncludeDate(DateTime date)
        {
            return await _db.Requests
                .Where(x => x.StartDate <= date && x.EndDate >= date)
                .ToListAsync();
        }
        #endregion

    }
}
