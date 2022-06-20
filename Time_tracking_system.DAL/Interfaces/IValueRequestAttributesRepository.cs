using System.Collections.Generic;
using System.Threading.Tasks;
using Time_tracking_system.Domain.Entity;

namespace Time_tracking_system.DAL.Interfaces
{
    public interface IValueRequestAttributesRepository : IRepositoryBase<ValueRequestAttributes>
    {
        //Существует запись по id?
        Task<bool> isExists(int id);
        //доп функции
        public Task<ICollection<ValueRequestAttributes>> FindByRequestId(int requestId);
        //Количество неиспользованных дней отпуска в текущем году
        Task<int> NumberOfUnusedVacationDaysInTheCurrentYear(string idApplicant);
        //Количество неиспользованных дней отпуска за прошедшие года
        Task<int> NumberOfUnusedVacationDaysInThePastYears(string idApplicant);
    }
}
