using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_tracking_system.Domain.Response;
using Time_tracking_system.Domain.ViewModels;

namespace Time_tracking_system.Service.Interfaces
{
    public interface IRequestService
    {
        Task<IBaseResponse<List<RequestVM>>> GetAllApplicationsWithTheRoleEmployeeByUserId(string userId);
        Task<IBaseResponse<List<RequestVM>>> GetAllRequestsWithoutStatusNew();
        Task<IBaseResponse<AttrAndRequestVM>> GetAttrAndRequestById(int id);
        Task<IBaseResponse<CreateRequestVM>> GetAttrAndTypeRequest();
        Task<IBaseResponse<List<TypeVocationVM>>> GetAllTypeVocation();
        Task<IBaseResponse<bool>> CreateRequest(CreateRequestVM model, string employeeId);
        Task<IBaseResponse<bool>> EditRequest(AttrAndRequestVM model);
        Task<IBaseResponse<bool>> SubmitForReviewRequest(int id);
        Task<IBaseResponse<bool>> SetStatusRequest(int idRequest, string employeeId, string status);
        Task<IBaseResponse<List<RequestVM>>> GetRequestsByIdEmployee(string idEmployee);

        //статистика
        Task<IBaseResponse<Dictionary<string, int>>> GetStatisticsById(string id);

        //сервисы для атрибутов заявок
        Task<IBaseResponse<List<AttributesVM>>> GetAllAttributes();
        Task<IBaseResponse<bool>> CreateAttributes(AttributesVM model);

        //сервисы для типов заявок
        Task<IBaseResponse<List<TypeRequestVM>>> GetAllTypeRequest();
        Task<IBaseResponse<bool>> CreateTypeRequest(TypeRequestVM model, List<int> attr);
        Task<IBaseResponse<List<Attributes_TypeRequestVM>>> DetailsTypeRequest(int id);

        //сервисы для типов отпусков
        Task<IBaseResponse<bool>> CreateTypeVocation(TypeVocationVM model);
    }
}
