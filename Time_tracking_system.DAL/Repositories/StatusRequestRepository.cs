using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Time_tracking_system.DAL.Interfaces;

namespace Time_tracking_system.DAL.Repositories
{
    public class StatusRequestRepository : IStatusRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public StatusRequestRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        #region GetStatusIdAgreed
        public async Task<int> GetStatusIdAgreed()
        {
            var status = await _db.StatusRequests
                .FirstOrDefaultAsync(x => x.Name == "Согласована");
            return status.Id;
        }
        #endregion

        #region GetStatusIdApproved
        public async Task<int> GetStatusIdApproved()
        {
            var status = await _db.StatusRequests
                .FirstOrDefaultAsync(x => x.Name == "Утверждена");
            return status.Id;
        }
        #endregion

        #region GetStatusIdCanceled
        public async Task<int> GetStatusIdCanceled()
        {
            var status = await _db.StatusRequests
                .FirstOrDefaultAsync(x => x.Name == "Отменена");
            return status.Id;
        }
        #endregion

        #region GetStatusIdNew
        public async Task<int> GetStatusIdNew()
        {
            var status = await _db.StatusRequests
                .FirstOrDefaultAsync(x => x.Name == "Новая");
            return status.Id;
        }
        #endregion

        #region GetStatusIdNotAgreed
        public async Task<int> GetStatusIdNotAgreed()
        {
            var status = await _db.StatusRequests
                .FirstOrDefaultAsync(x => x.Name == "Не согласована");
            return status.Id;
        }
        #endregion

        #region GetStatusIdNotApproved
        public async Task<int> GetStatusIdNotApproved()
        {
            var status = await _db.StatusRequests
                .FirstOrDefaultAsync(x => x.Name == "Не утверждена");
            return status.Id;
        }
        #endregion

        #region GetStatusIdSentForApproval
        public async Task<int> GetStatusIdSentForApproval()
        {
            var status = await _db.StatusRequests
                .FirstOrDefaultAsync(x => x.Name == "Отправлена на согласование");
            return status.Id;
        }
        #endregion

        #region GetStatusIdWithdrawn
        public async Task<int> GetStatusIdWithdrawn()
        {
            var status = await _db.StatusRequests
                .FirstOrDefaultAsync(x => x.Name == "Отозвана");
            return status.Id;
        }
        #endregion

        #region GetStatusIdAwaitingCancellationByManager
        public async Task<int> GetStatusIdAwaitingCancellationByManager()
        {
            var status = await _db.StatusRequests
                .FirstOrDefaultAsync(x => x.Name == "Ожидает отмены руководителем");
            return status.Id;
        }
        #endregion

        #region GetStatusIdAwaitingCancellationByDirector
        public async Task<int> GetStatusIdAwaitingCancellationByDirector()
        {
            var status = await _db.StatusRequests
                .FirstOrDefaultAsync(x => x.Name == "Ожидает отмены директором");
            return status.Id;
        }
        #endregion
    }
}
