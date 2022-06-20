using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Time_tracking_system.DAL.Interfaces;
using Time_tracking_system.Domain.Entity;
using Time_tracking_system.Domain.Enum;
using Time_tracking_system.Domain.Response;
using Time_tracking_system.Domain.ViewModels;
using Time_tracking_system.Service.Interfaces;

namespace Time_tracking_system.Service.Implementations
{
    public class CalendarService : ICalendarService
    {
        private readonly IYearsRepository _yearsRepo;
        private readonly IHolidayCalendarRepository _holidayCalendarRepo;
        private readonly IRequestRepository _requestRepo;
        private readonly IStatusRequestRepository _statusRequestRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<CalendarService> _logger;

        public CalendarService(IRequestRepository requestRepo,
            IStatusRequestRepository statusRequestRepo,
            IYearsRepository yearsRepo,
            IHolidayCalendarRepository holidayCalendarRepo,
            IMapper mapper,
            ILogger<CalendarService> logger)
        {
            _yearsRepo = yearsRepo;
            _holidayCalendarRepo = holidayCalendarRepo;
            _requestRepo = requestRepo;
            _statusRequestRepo = statusRequestRepo;
            _mapper = mapper;
            _logger = logger;
        }

        #region GetAllYears
        public async Task<IBaseResponse<List<YearsVM>>> GetAllYears()
        {
            var baseResponse = new BaseResponse<List<YearsVM>>();
            try
            {
                var yearsList = await _yearsRepo.FindAll();
                var listYearsVM = _mapper.Map<List<YearsVM>>(yearsList);
                baseResponse.Data = listYearsVM;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetAllYears]: {ex.Message}");
                return new BaseResponse<List<YearsVM>>()
                {
                    Description = $"[GetAllYears] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region DetailsHolidayCalendar
        public async Task<IBaseResponse<YearAndHolidayCalendarVM>> DetailsHolidayCalendar(int id)
        {
            var baseResponse = new BaseResponse<YearAndHolidayCalendarVM>();
            try
            {
                bool isExistsYear = await _yearsRepo.isExists(id);
                if (!isExistsYear)
                {
                    baseResponse.StatusCode = StatusCode.NotFound;
                    return baseResponse;
                }
                var year = await _yearsRepo.FindById(id);
                var listHolidayCalendarByIdYear = await _holidayCalendarRepo.FindAllByIdYear(id);
                var yearVM = _mapper.Map<YearsVM>(year);
                var listHolidayCalendarByIdYearVM = _mapper.Map<List<HolidayCalendarVM>>(listHolidayCalendarByIdYear);
                baseResponse.Data = new YearAndHolidayCalendarVM
                {
                    ListHolidayCalendar = listHolidayCalendarByIdYearVM,
                    Year = yearVM
                };
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[DetailsHolidayCalendar]: {ex.Message}");
                return new BaseResponse<YearAndHolidayCalendarVM>()
                {
                    Description = $"[DetailsHolidayCalendar] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region CreateHolidayCalendar
        public async Task<IBaseResponse<bool>> CreateHolidayCalendar(string selectidDate, string year)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                bool isExists = await _yearsRepo.isExistsYear(year);
                if (isExists)
                {
                    //уже такой год существует в системе
                    baseResponse.StatusCode = StatusCode.OK;
                    baseResponse.Data = false;
                    return baseResponse;
                }
                var newYear = new Years
                {
                    Year = year,
                    CalendarInputStatus = false
                };
                await _yearsRepo.Create(newYear);
                var listHolidayCalendarVM = JsonSerializer.Deserialize<List<HolidayCalendarVM>>(selectidDate);
                if (listHolidayCalendarVM.Count() != 0)
                {
                    foreach (var item in listHolidayCalendarVM)
                    {
                        item.Years_id = newYear.Id;
                    }
                }
                var listHoliday = _mapper.Map<List<HolidayCalendar>>(listHolidayCalendarVM);
                foreach (var item in listHoliday)
                {
                    await _holidayCalendarRepo.Create(item);
                }
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = true;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[CreateHolidayCalendar]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateHolidayCalendar] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region PutTheCalendarIntoCirculation
        public async Task<IBaseResponse<bool>> PutTheCalendarIntoCirculation(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var isExists = await _yearsRepo.isExists(id);
                if (!isExists)
                {
                    baseResponse.StatusCode = StatusCode.NotFound;
                    baseResponse.Data = false;
                    return baseResponse;
                }
                var year = await _yearsRepo.FindById(id);
                year.CalendarInputStatus = true;
                bool isSuccess = await _yearsRepo.Update(year);
                if (!isSuccess)
                {
                    baseResponse.StatusCode = StatusCode.UpdateError;
                    baseResponse.Data = false;
                    return baseResponse;
                }
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = true;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[PutTheCalendarIntoCirculation]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = $"[PutTheCalendarIntoCirculation] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region GetYearAndHolidayCalendar
        public async Task<IBaseResponse<YearAndHolidayCalendarVM>> GetYearAndHolidayCalendar(int id)
        {
            var baseResponse = new BaseResponse<YearAndHolidayCalendarVM>();
            try
            {
                bool isExistsYear = await _yearsRepo.isExists(id);
                if (!isExistsYear)
                {
                    baseResponse.StatusCode = StatusCode.NotFound;
                    return baseResponse;
                }
                var year = await _yearsRepo.FindById(id);
                var listHolidayCalendarByIdYear = await _holidayCalendarRepo.FindAllByIdYear(id);
                var yearVM = _mapper.Map<YearsVM>(year);
                var listHolidayCalendarByIdYearVM = _mapper.Map<List<HolidayCalendarVM>>(listHolidayCalendarByIdYear);
                baseResponse.Data = new YearAndHolidayCalendarVM
                {
                    ListHolidayCalendar = listHolidayCalendarByIdYearVM,
                    Year = yearVM
                };
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetYearAndHolidayCalendar]: {ex.Message}");
                return new BaseResponse<YearAndHolidayCalendarVM>()
                {
                    Description = $"[GetYearAndHolidayCalendar] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region EditHolidayCalendar
        public async Task<IBaseResponse<bool>> EditHolidayCalendar(string selectidDate)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var listHolidayCalendarVM = JsonSerializer.Deserialize<List<HolidayCalendarVM>>(selectidDate);
                if (listHolidayCalendarVM.Count() == 0)
                {
                    baseResponse.Data = true;
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                foreach (var item in listHolidayCalendarVM)
                {
                    if (item.Status == "New")
                    {
                        var newHoliday = _mapper.Map<HolidayCalendar>(item);
                        await _holidayCalendarRepo.Create(newHoliday);
                        var allRequestsThatIncludeDate = await _requestRepo.FindAllRequestsThatIncludeDate(item.HolidayDate);
                        foreach (var request in allRequestsThatIncludeDate)
                        {
                            request.StatusRequest_id = await _statusRequestRepo.GetStatusIdCanceled();
                            await _requestRepo.Update(request);
                        }
                    }
                    else if (item.Status == "Removed")
                    {
                        var newHoliday = _mapper.Map<HolidayCalendar>(item);
                        await _holidayCalendarRepo.Delete(newHoliday);
                    }
                }
                baseResponse.Data = true;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[EditHolidayCalendar]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = $"[EditHolidayCalendar] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region GetAllHolidays
        public async Task<IBaseResponse<List<HolidayCalendarVM>>> GetAllHolidays()
        {
            var baseResponse = new BaseResponse<List<HolidayCalendarVM>>();
            try
            {
                var listHolidays = await _holidayCalendarRepo.FindAll();
                var listHolidaysVM = _mapper.Map<List<HolidayCalendarVM>>(listHolidays);
                baseResponse.Data = listHolidaysVM;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetAllHolidays]: {ex.Message}");
                return new BaseResponse<List<HolidayCalendarVM>>()
                {
                    Description = $"[GetAllHolidays] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion
    }
}
