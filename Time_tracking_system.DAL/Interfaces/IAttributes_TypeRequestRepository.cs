using System.Collections.Generic;
using System.Threading.Tasks;
using Time_tracking_system.Domain.Entity;

namespace Time_tracking_system.DAL.Interfaces
{
    public interface IAttributes_TypeRequestRepository : IRepositoryBase<Attributes_TypeRequest>
    {
        public Task<string> GetTypeDataByIdAttributeTypeRequest(int id);
        public Task<ICollection<Attributes_TypeRequest>> FindByIdTypeRequest(int id);
    }
}
