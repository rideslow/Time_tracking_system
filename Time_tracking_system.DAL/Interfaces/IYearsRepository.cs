using System.Threading.Tasks;
using Time_tracking_system.Domain.Entity;

namespace Time_tracking_system.DAL.Interfaces
{
    public interface IYearsRepository : IRepositoryBase<Years>
    {
        Task<Years> FindByYear(string year);
        Task<bool> isExists(int id);
        Task<bool> isExistsYear(string year);
    }
}
