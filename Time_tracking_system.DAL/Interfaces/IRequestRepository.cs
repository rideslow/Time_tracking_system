using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Time_tracking_system.Domain.Entity;

namespace Time_tracking_system.DAL.Interfaces
{
    public interface IRequestRepository : IRepositoryBase<Request>
    {
        //Существует запись по id?
        Task<bool> isExists(int id);
        //все заявки пользователя
        Task<ICollection<Request>> FindByIdApplicant(string idApplicant);
        //все заявки без статуса "Новая"
        Task<ICollection<Request>> FindAllRequestsWithoutStatusNew();
        //Количество дней отгулов по утвержденным заявкам в текущем году
        Task<int> NumberOfDaysOffInTheCurrentYear(string idApplicant);
        //количесвто заявок пользователя со статусом 
        Task<int> CountRequestWithStatusByIdApplicant(string idApplicant, string status);
        //найти все запросы которые включают дату
        Task<ICollection<Request>> FindAllRequestsThatIncludeDate(DateTime date);
    }
}
