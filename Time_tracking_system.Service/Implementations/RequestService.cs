using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Time_tracking_system.DAL.Interfaces;
using Time_tracking_system.Domain.Entity;
using Time_tracking_system.Domain.Enum;
using Time_tracking_system.Domain.Response;
using Time_tracking_system.Domain.ViewModels;
using Time_tracking_system.Service.Interfaces;

namespace Time_tracking_system.Service.Implementations
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepo;
        private readonly IAttributes_TypeRequestRepository _attributes_TypeRequestRepo;
        private readonly ITypeRequestRepository _typeRequestRepo;
        private readonly ITypeVocationRepository _typeVocationRepo;
        private readonly IStatusRequestRepository _statusRequestRepo;
        private readonly IValueRequestAttributesRepository _valueRequestAttributesRepo;
        private readonly IYearsRepository _yearsRepo;
        private readonly IHolidayCalendarRepository _holidayCalendarRepo;
        private readonly IAttributesRepository _attributesRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<CalendarService> _logger;

        public RequestService(IRequestRepository requestRepo,
            IAttributes_TypeRequestRepository attributes_TypeRequestRepo,
            ITypeRequestRepository typeRequestRepo,
            ITypeVocationRepository typeVocationRepo,
            IStatusRequestRepository statusRequestRepo,
            IValueRequestAttributesRepository valueRequestAttributesRepo,
            IYearsRepository yearsRepo,
            IHolidayCalendarRepository holidayCalendarRepo,
            IAttributesRepository attributesRepo,
            IMapper mapper,
            ILogger<CalendarService> logger)
        {
            _requestRepo = requestRepo;
            _attributes_TypeRequestRepo = attributes_TypeRequestRepo;
            _typeRequestRepo = typeRequestRepo;
            _typeVocationRepo = typeVocationRepo;
            _statusRequestRepo = statusRequestRepo;
            _valueRequestAttributesRepo = valueRequestAttributesRepo;
            _yearsRepo = yearsRepo;
            _holidayCalendarRepo = holidayCalendarRepo;
            _attributesRepo = attributesRepo;
            _mapper = mapper;
            _logger = logger;
        }

        #region GetAllApplicationsWithTheRoleEmployeeByUserId
        public async Task<IBaseResponse<List<RequestVM>>> GetAllApplicationsWithTheRoleEmployeeByUserId(string userId)
        {
            var baseResponse = new BaseResponse<List<RequestVM>>();
            try
            {
                var requestEmployee = await _requestRepo.FindByIdApplicant(userId);
                var listRequestVM = _mapper.Map<List<RequestVM>>(requestEmployee);
                baseResponse.Data = listRequestVM;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetAllApplicationsWithTheRoleEmployeeByUserId]: {ex.Message}");
                return new BaseResponse<List<RequestVM>>()
                {
                    Description = $"[GetAllApplicationsWithTheRoleEmployeeByUserId] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region GetAllRequestsWithoutStatusNew
        public async Task<IBaseResponse<List<RequestVM>>> GetAllRequestsWithoutStatusNew()
        {
            var baseResponse = new BaseResponse<List<RequestVM>>();
            try
            {
                var requestsWithoutStatusNew = await _requestRepo.FindAllRequestsWithoutStatusNew();
                var listRequestsVMWithoutStatusNew = _mapper.Map<List<RequestVM>>(requestsWithoutStatusNew);
                baseResponse.Data = listRequestsVMWithoutStatusNew;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetAllRequestsWithoutStatusNew]: {ex.Message}");
                return new BaseResponse<List<RequestVM>>()
                {
                    Description = $"[GetAllRequestsWithoutStatusNew] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region GetAttrAndRequestById
        public async Task<IBaseResponse<AttrAndRequestVM>> GetAttrAndRequestById(int id)
        {
            var baseResponse = new BaseResponse<AttrAndRequestVM>();
            try
            {
                var request = await _requestRepo.FindById(id);
                var valueAttrRequest = await _valueRequestAttributesRepo.FindByRequestId(id);
                if(request == null || valueAttrRequest.Count == 0)
                {
                    baseResponse.Description = "Запись не найдена.";
                    baseResponse.StatusCode = StatusCode.NotFound;
                    return baseResponse;
                }

                var requestVM = _mapper.Map<RequestVM>(request);
                var valueAttrRequestVM = _mapper.Map<List<ValueRequestAttributesVM>>(valueAttrRequest);

                baseResponse.Data = new AttrAndRequestVM
                {
                    request = requestVM,
                    listValueRequestAttr = valueAttrRequestVM
                };
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetRequestById]: {ex.Message}");
                return new BaseResponse<AttrAndRequestVM>()
                {
                    Description = $"[GetRequestById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
#endregion

        #region GetAttrAndTypeRequest
        public async Task<IBaseResponse<CreateRequestVM>> GetAttrAndTypeRequest()
        {
            var baseResponse = new BaseResponse<CreateRequestVM>();
            try
            {
                var collectionAttributes_TypeRequest = await _attributes_TypeRequestRepo.FindAll();
                var collectionTypeRequest = await _typeRequestRepo.FindAll();

                var typeRequestItems = collectionTypeRequest.Select(q => new SelectListItem
                {
                    Text = q.Name,
                    Value = q.Id.ToString()
                });

                var listTypeRequestModel = _mapper.Map<List<Attributes_TypeRequestVM>>(collectionAttributes_TypeRequest);

                baseResponse.Data = new CreateRequestVM
                {
                    attributes_TypeRequests = listTypeRequestModel,
                    typeRequest = typeRequestItems
                };
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetAttrAndTypeRequest]: {ex.Message}");
                return new BaseResponse<CreateRequestVM>()
                {
                    Description = $"[GetAttrAndTypeRequest] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region GetAllTypeVocation
        public async Task<IBaseResponse<List<TypeVocationVM>>> GetAllTypeVocation()
        {
            var baseResponse = new BaseResponse<List<TypeVocationVM>>();
            try
            {
                var collectionTypeVocation = await _typeVocationRepo.FindAll();
                var listTypeVocationModel = _mapper.Map<List<TypeVocationVM>>(collectionTypeVocation);
                baseResponse.Data = listTypeVocationModel;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetAllTypeVocation]: {ex.Message}");
                return new BaseResponse<List<TypeVocationVM>>()
                {
                    Description = $"[GetAllTypeVocation] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
#endregion

        #region CreateRequest
        public async Task<IBaseResponse<bool>> CreateRequest(CreateRequestVM model, string employeeId)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var startDate = model.StartDate;
                var endDate = model.EndDate;
                var findYearInDB = await _yearsRepo.FindByYear(startDate.Year.ToString());
                if (findYearInDB == null || !findYearInDB.CalendarInputStatus)
                {
                    baseResponse.Description = "Календарь на этот год еще не введен в обращение. Обратитесь к администратору.";
                    baseResponse.StatusCode = StatusCode.OK;
                    baseResponse.Data = false;
                    return baseResponse;
                }
                if (DateTime.Compare(startDate, endDate) > 0)
                {
                    baseResponse.Description = "Дата начала не может быть позже даты окончания";
                    baseResponse.StatusCode = StatusCode.OK;
                    baseResponse.Data = false;
                    return baseResponse;
                }
                int daysRequested = (int)(endDate - startDate).TotalDays;
                int countHolidayDate = await _holidayCalendarRepo.NumberOfHolidaysInTheDateRange(startDate, endDate);
                int countWeekends = 0;
                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        countWeekends += 1;
                    }
                }
                int countRequestidDays = daysRequested - countHolidayDate - countWeekends;
                var idStatusNew = await _statusRequestRepo.GetStatusIdNew();

                var requestModel = new RequestVM
                {
                    TypeRequest_id = model.TypeRequest_id,
                    Applicant_id = employeeId,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Comments = model.Comments,
                    DateCreated = DateTime.Now,
                    DateRequested = DateTime.Now,
                    CountDayOnRequest = countRequestidDays,
                    StatusRequest_id = idStatusNew
                };
                var leaveRequest = _mapper.Map<Request>(requestModel);
                var isSuccessRequest = await _requestRepo.Create(leaveRequest);

                foreach (var item in model.value_Attributes_TypeRequests)
                {
                    string typeData = await _attributes_TypeRequestRepo.GetTypeDataByIdAttributeTypeRequest(item.Id);
                    var valueRequestAttrModel = new ValueRequestAttributes
                    {
                        Request_id = leaveRequest.Id,
                        Attr_TypeRequest_id = item.Id
                    };
                    if (typeData == "text")
                        valueRequestAttrModel.ValueText = item.Value;
                    else if (typeData == "DateTime")
                        valueRequestAttrModel.ValueDate = Convert.ToDateTime(item.Value);
                    else if (typeData == "Bool")
                        valueRequestAttrModel.ValueBool = Convert.ToBoolean(item.Value);
                    else if (typeData == "Employee")
                        valueRequestAttrModel.ValueEmloyee_id = item.Value;
                    else if (typeData == "TypeVocation")
                        valueRequestAttrModel.ValueTypeVocation_id = Convert.ToInt32(item.Value);
                    var valueRequestAttr = _mapper.Map<ValueRequestAttributes>(valueRequestAttrModel);
                    await _valueRequestAttributesRepo.Create(valueRequestAttr);
                }

                if (!isSuccessRequest)
                {
                    baseResponse.Description = "Что-то пошло не так с отправкой вашей записи";
                    baseResponse.StatusCode = StatusCode.OK;
                    baseResponse.Data = false;
                    return baseResponse;
                }
                    baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = true;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[CreateRequest]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateRequest] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region EditRequest
        public async Task<IBaseResponse<bool>> EditRequest(AttrAndRequestVM model)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var startDate = model.request.StartDate;
                var endDate = model.request.EndDate;
                if (DateTime.Compare(startDate, endDate) > 0)
                {
                    baseResponse.Description = "Дата начала не может быть позже даты окончания";
                    baseResponse.StatusCode = StatusCode.OK;
                    baseResponse.Data = false;
                    return baseResponse;
                }

                model.request.DateRequested = DateTime.Now;
                model.request.CountDayOnRequest = (int)(endDate - startDate).TotalDays;

                var request = _mapper.Map<Request>(model.request);
                bool isSuccessRequest = await _requestRepo.Update(request);
                if (!isSuccessRequest)
                {
                    baseResponse.Description = "Что-то пошло не так с отправкой вашей записи";
                    baseResponse.StatusCode = StatusCode.OK;
                    baseResponse.Data = false;
                    return baseResponse;
                }
                foreach (var item in model.listValueRequestAttr)
                {
                    var valueRequestAttr = _mapper.Map<ValueRequestAttributes>(item);
                    bool isSuccessValueRequestAttr = await _valueRequestAttributesRepo.Update(valueRequestAttr);
                    if (!isSuccessValueRequestAttr)
                    {
                        baseResponse.Description = "Что-то пошло не так с отправкой вашей записи";
                        baseResponse.StatusCode = StatusCode.OK;
                        baseResponse.Data = false;
                        return baseResponse;
                    }
                }
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = true;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[EditRequest]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = $"[EditRequest] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
#endregion

        #region SubmitForReviewRequest
        public async Task<IBaseResponse<bool>> SubmitForReviewRequest(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var isExists = await _requestRepo.isExists(id);
                if (!isExists)
                {
                    baseResponse.StatusCode = StatusCode.NotFound;
                    return baseResponse;
                }
                var request = await _requestRepo.FindById(id);
                request.StatusRequest_id = await _statusRequestRepo.GetStatusIdSentForApproval();
                bool isSuccess = await _requestRepo.Update(request);
                if (!isSuccess)
                {
                    baseResponse.Description = "Что-то пошло не так с отправкой вашей записи";
                    baseResponse.Data = false;
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                baseResponse.Data = true;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[SubmitForReviewRequest]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = $"[SubmitForReviewRequest] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
#endregion

        #region SetStatusRequest
        public async Task<IBaseResponse<bool>> SetStatusRequest(int idRequest, string employeeId, string status)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var isExists = await _requestRepo.isExists(idRequest);
                if (!isExists)
                {
                    baseResponse.Data = false;
                    baseResponse.StatusCode = StatusCode.NotFound;
                    return baseResponse;
                }
                var request = await _requestRepo.FindById(idRequest);
                request.StatusRequest_id = await _statusRequestRepo.GetStatusIdAgreed();

                if(status == "Agreed")
                    request.StatusRequest_id = await _statusRequestRepo.GetStatusIdAgreed();
                else if(status == "NotAgreed")
                    request.StatusRequest_id = await _statusRequestRepo.GetStatusIdNotAgreed();
                else if (status == "Approved")
                    request.StatusRequest_id = await _statusRequestRepo.GetStatusIdApproved();
                else if (status == "NotApproved")
                    request.StatusRequest_id = await _statusRequestRepo.GetStatusIdNotApproved();
                else if (status == "Withdraw")
                    request.StatusRequest_id = await _statusRequestRepo.GetStatusIdWithdrawn();
                else if (status == "Cancel")
                {
                    if (request.StatusRequest.Name == "Утверждена")
                        request.StatusRequest_id = await _statusRequestRepo.GetStatusIdAwaitingCancellationByManager();
                    else
                        request.StatusRequest_id = await _statusRequestRepo.GetStatusIdCanceled();
                }
                else if (status == "CancellationConfirmationByManager")
                    request.StatusRequest_id = await _statusRequestRepo.GetStatusIdAwaitingCancellationByDirector();
                else
                    request.StatusRequest_id = await _statusRequestRepo.GetStatusIdCanceled();

                request.Cordinating_id = employeeId;
                bool isSuccess = await _requestRepo.Update(request);
                if (!isSuccess)
                {
                    baseResponse.Description = "Что-то пошло не так с вашей заявкой";
                    baseResponse.Data = false;
                    baseResponse.StatusCode = StatusCode.UpdateError;
                    return baseResponse;
                }
                baseResponse.Data = true;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[SetStatusRequest]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = $"[SetStatusRequest] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region GetRequestsByIdEmployee
        public async Task<IBaseResponse<List<RequestVM>>> GetRequestsByIdEmployee(string idEmployee)
        {
            var baseResponse = new BaseResponse<List<RequestVM>>();
            try
            {
                var listRequestsByIdEmployee = await _requestRepo.FindByIdApplicant(idEmployee);
                var listRequestsByIdEmployeeVM = _mapper.Map<List<RequestVM>>(listRequestsByIdEmployee);
                baseResponse.Data = listRequestsByIdEmployeeVM;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetRequestsByIdEmployee]: {ex.Message}");
                return new BaseResponse<List<RequestVM>>()
                {
                    Description = $"[GetRequestsByIdEmployee] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        //статистика
        #region GetStatisticsById
        public async Task<IBaseResponse<Dictionary<string, int>>> GetStatisticsById(string id)
        {
            var baseResponse = new BaseResponse<Dictionary<string, int>>();
            try
            {
                Dictionary<string, int> statistics = new Dictionary<string, int>();
                var listRequest = await _requestRepo.FindByIdApplicant(id);
                statistics.Add("countDayVocationInTheCurrentYear", await _valueRequestAttributesRepo.NumberOfUnusedVacationDaysInTheCurrentYear(id));
                statistics.Add("countDayVocationInThePastYears", await _valueRequestAttributesRepo.NumberOfUnusedVacationDaysInThePastYears(id));
                statistics.Add("countDaysOffInTheCurrentYear", await _requestRepo.NumberOfDaysOffInTheCurrentYear(id));
                statistics.Add("allCountRequest", listRequest.Count());
                statistics.Add("countRequestWithStatusNew", await _requestRepo.CountRequestWithStatusByIdApplicant(id, "Новая"));
                statistics.Add("countRequestWithStatusApproved", await _requestRepo.CountRequestWithStatusByIdApplicant(id, "Утверждена"));
                statistics.Add("countRequestWithStatusAgreed", await _requestRepo.CountRequestWithStatusByIdApplicant(id, "Согласована"));
                statistics.Add("countRequestWithStatusNotApproved", await _requestRepo.CountRequestWithStatusByIdApplicant(id, "Не утверждена"));
                statistics.Add("countRequestWithStatusNotAgreed", await _requestRepo.CountRequestWithStatusByIdApplicant(id, "Не согласована"));
                statistics.Add("countRequestWithStatusCanceled", await _requestRepo.CountRequestWithStatusByIdApplicant(id, "Отменена"));
                statistics.Add("countRequestWithStatusWithdrawn", await _requestRepo.CountRequestWithStatusByIdApplicant(id, "Отозвана"));
                baseResponse.Data = statistics;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetStatisticsById]: {ex.Message}");
                return new BaseResponse<Dictionary<string, int>>()
                {
                    Description = $"[GetStatisticsById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        //сервисы для атрибутов заявок
        #region GetAllAttributes
        public async Task<IBaseResponse<List<AttributesVM>>> GetAllAttributes()
        {
            var baseResponse = new BaseResponse<List<AttributesVM>>();
            try
            {
                var attributes = await _attributesRepo.FindAll();
                var listAttributes = _mapper.Map<List<AttributesVM>>(attributes);
                baseResponse.Data = listAttributes;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetAllAttributes]: {ex.Message}");
                return new BaseResponse<List<AttributesVM>>()
                {
                    Description = $"[GetAllAttributes] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region CreateAttributes
        public async Task<IBaseResponse<bool>> CreateAttributes(AttributesVM model)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var newAttributes = _mapper.Map<Attributes>(model);
                bool isSuccess = await _attributesRepo.Create(newAttributes);
                if (!isSuccess)
                {
                    baseResponse.Data = false;
                    baseResponse.StatusCode = StatusCode.CreateError;
                    baseResponse.Description = "Что-то пошло не так с созданием атрибута заявки";
                    return baseResponse;
                }
                baseResponse.Data = true;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[CreateAttributes]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateAttributes] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        //сервисы для типов заявок
        #region GetAllTypeRequest
        public async Task<IBaseResponse<List<TypeRequestVM>>> GetAllTypeRequest()
        {
            var baseResponse = new BaseResponse<List<TypeRequestVM>>();
            try
            {
                var typeRequest = await _typeRequestRepo.FindAll();
                var listTypeRequestVM = _mapper.Map<List<TypeRequestVM>>(typeRequest);
                baseResponse.Data = listTypeRequestVM;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetAllTypeRequest]: {ex.Message}");
                return new BaseResponse<List<TypeRequestVM>>()
                {
                    Description = $"[GetAllTypeRequest] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region CreateTypeRequest
        public async Task<IBaseResponse<bool>> CreateTypeRequest(TypeRequestVM model, List<int> attr)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var newTypeRequest = _mapper.Map<TypeRequest>(model);
                bool isSuccess = await _typeRequestRepo.Create(newTypeRequest);
                if (!isSuccess)
                {
                    baseResponse.Data = false;
                    baseResponse.StatusCode = StatusCode.CreateError;
                    baseResponse.Description = "Что-то пошло не так с созданием типа заявки";
                    return baseResponse;
                }
                foreach (var item in attr)
                {
                    var newAttr_TypeRequest = new Attributes_TypeRequest
                    {
                        TypeRequest_id = newTypeRequest.Id,
                        Attr_id = item
                    };
                    await _attributes_TypeRequestRepo.Create(newAttr_TypeRequest);
                }
                baseResponse.Data = true;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[CreateTypeRequest]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateTypeRequest] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region DetailsTypeRequest
        public async Task<IBaseResponse<List<Attributes_TypeRequestVM>>> DetailsTypeRequest(int id)
        {
            var baseResponse = new BaseResponse<List<Attributes_TypeRequestVM>>();
            try
            {
                var listAttrTypeRequests = await _attributes_TypeRequestRepo.FindByIdTypeRequest(id);
                if (listAttrTypeRequests.Count == 0)
                {
                    baseResponse.StatusCode = StatusCode.NotFound;
                    return baseResponse;
                }
                var listAttributes_TypeRequestVM = _mapper.Map<List<Attributes_TypeRequestVM>>(listAttrTypeRequests);
                baseResponse.Data = listAttributes_TypeRequestVM;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[DetailsTypeRequest]: {ex.Message}");
                return new BaseResponse<List<Attributes_TypeRequestVM>>()
                {
                    Description = $"[DetailsTypeRequest] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        //сервисы для типов отпусков
        #region CreateTypeVocation
        public async Task<IBaseResponse<bool>> CreateTypeVocation(TypeVocationVM model)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var newTypeVocation = _mapper.Map<TypeVocation>(model);
                bool isSuccess = await _typeVocationRepo.Create(newTypeVocation);
                if (!isSuccess)
                {
                    baseResponse.Data = false;
                    baseResponse.StatusCode = StatusCode.CreateError;
                    baseResponse.Description = "Что-то пошло не так с созданием типа отпуска";
                    return baseResponse;
                }
                baseResponse.Data = true;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[CreateTypeVocation]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateTypeVocation] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

    }
}
