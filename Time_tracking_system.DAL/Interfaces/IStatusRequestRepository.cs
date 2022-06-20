using System.Threading.Tasks;

namespace Time_tracking_system.DAL.Interfaces
{
    public interface IStatusRequestRepository
    {        
        //новая
        Task<int> GetStatusIdNew();
        //не согласована
        Task<int> GetStatusIdNotAgreed();
        //согласована
        Task<int> GetStatusIdAgreed();
        //отправлена на согласование
        Task<int> GetStatusIdSentForApproval();
        //не утверждена
        Task<int> GetStatusIdNotApproved();
        //утверждена
        Task<int> GetStatusIdApproved();
        //отозвана
        Task<int> GetStatusIdWithdrawn();
        //отменена
        Task<int> GetStatusIdCanceled();
        //Ожидает отмены руководителем
        Task<int> GetStatusIdAwaitingCancellationByManager();
        //Ожидает отмены директором
        Task<int> GetStatusIdAwaitingCancellationByDirector();
    }
}
