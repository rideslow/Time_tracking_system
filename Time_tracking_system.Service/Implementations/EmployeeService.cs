using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Time_tracking_system.Domain.Entity;
using Time_tracking_system.Domain.Enum;
using Time_tracking_system.Domain.Response;
using Time_tracking_system.Domain.ViewModels;
using Time_tracking_system.Service.Interfaces;

namespace Time_tracking_system.Service.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ILogger<CalendarService> _logger;

        public EmployeeService(UserManager<Employee> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            ILogger<CalendarService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _logger = logger;
        }

        #region GetAllEmployees
        public async Task<IBaseResponse<List<EmployeeVM>>> GetAllEmployees()
        {
            var baseResponse = new BaseResponse<List<EmployeeVM>>();
            try
            {
                var collectionEmployee = await _userManager.Users.ToListAsync();
                var listEmployeeMap = _mapper.Map<List<EmployeeVM>>(collectionEmployee);
                baseResponse.Data = listEmployeeMap;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetAllEmployees]: {ex.Message}");
                return new BaseResponse<List<EmployeeVM>>()
                {
                    Description = $"[GetAllEmployees] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region GetEditEmployees
        public async Task<IBaseResponse<List<EmployeeVM>>> GetEditEmployees(Employee authorizedEmployee)
        {
            var baseResponse = new BaseResponse<List<EmployeeVM>>();
            try
            {
                var roleEmployee = await _userManager.GetRolesAsync(authorizedEmployee);
                if (roleEmployee[0].ToString() == "Руководитель")
                {
                    var usersInRole = await _userManager.GetUsersInRoleAsync("Сотрудник");
                    usersInRole.Add(authorizedEmployee);
                    usersInRole.OrderBy(x => x.FirstName);
                    var listEmployeeVM = _mapper.Map<List<EmployeeVM>>(usersInRole);
                    baseResponse.Data = listEmployeeVM;
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                else if (roleEmployee[0].ToString() == "Администратор" || roleEmployee[0].ToString() == "Директор")
                {
                    var listEmployees = await _userManager.Users.OrderBy(x => x.FirstName).ToListAsync();
                    var listEmployeeVM = _mapper.Map<List<EmployeeVM>>(listEmployees);
                    baseResponse.Data = listEmployeeVM;
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                else
                {
                    var employee = new List<Employee>() { authorizedEmployee };
                    var listEmployeeVM = _mapper.Map<List<EmployeeVM>>(employee);
                    baseResponse.Data = listEmployeeVM;
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetEditEmployees]: {ex.Message}");
                return new BaseResponse<List<EmployeeVM>>()
                {
                    Description = $"[GetEditEmployees] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region EditEmployee
        public async Task<IBaseResponse<bool>> EditEmployee(EditEmployeeVM model, bool currentUserRoleIsAdmin)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                if (currentUserRoleIsAdmin)
                {
                    var employee = _mapper.Map<Employee>(model.EmpVM);
                    var oldEmployee = await _userManager.FindByIdAsync(employee.Id);
                    oldEmployee.FirstName = employee.FirstName;
                    oldEmployee.LastName = employee.LastName;
                    oldEmployee.Patronymic = employee.Patronymic;
                    oldEmployee.PhoneNumber = employee.PhoneNumber;
                    oldEmployee.Post = employee.Post;
                    oldEmployee.Email = employee.Email;
                    oldEmployee.UserName = employee.Email;
                    var oldRole = await _userManager.GetRolesAsync(oldEmployee);
                    var newRole = await _roleManager.FindByIdAsync(model.RoleId);
                    if (oldRole.Count != 0)
                    {
                        if (oldRole[0].ToString() != newRole.ToString())
                        {
                            await _userManager.RemoveFromRoleAsync(oldEmployee, oldRole[0].ToString());
                            await _userManager.AddToRoleAsync(oldEmployee, newRole.ToString());
                        }
                    }
                    else
                        await _userManager.AddToRoleAsync(oldEmployee, newRole.ToString());
                    await _userManager.UpdateAsync(oldEmployee);
                    baseResponse.Data = true;
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                else
                {
                    var employee = _mapper.Map<Employee>(model.EmpVM);
                    var oldEmployee = await _userManager.FindByIdAsync(employee.Id);
                    oldEmployee.Email = employee.Email;
                    oldEmployee.UserName = employee.Email;
                    oldEmployee.PhoneNumber = employee.PhoneNumber;
                    await _userManager.UpdateAsync(oldEmployee);
                    baseResponse.Data = true;
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[EditEmployee]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = $"[EditEmployee] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region GetAllRoles
        public async Task<IBaseResponse<List<IdentityRole>>> GetAllRoles()
        {
            var baseResponse = new BaseResponse<List<IdentityRole>>();
            try
            {
                baseResponse.Data = await _roleManager.Roles.ToListAsync();
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetAllRoles]: {ex.Message}");
                return new BaseResponse<List<IdentityRole>>()
                {
                    Description = $"[GetAllRoles] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region GetEmployeeAndRole
        public async Task<IBaseResponse<EditEmployeeVM>> GetEmployeeAndRole(string id)
        {
            var baseResponse = new BaseResponse<EditEmployeeVM>();
            try
            {
                var employee = await _userManager.FindByIdAsync(id);
                var roleEmp = await _userManager.GetRolesAsync(employee);
                var fullRoleEmp = await _roleManager.FindByNameAsync(roleEmp[0].ToString());
                var employeeVM = _mapper.Map<EmployeeVM>(employee);
                baseResponse.Data = new EditEmployeeVM
                {
                    RoleId = fullRoleEmp.Id,
                    EmpVM = employeeVM
                };
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetEmployeeAndRole]: {ex.Message}");
                return new BaseResponse<EditEmployeeVM>()
                {
                    Description = $"[GetEmployeeAndRole] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region GetEmployeesInCalendar
        public async Task<IBaseResponse<List<EmployeeVM>>> GetEmployeesInCalendar(Employee authorizedEmployee)
        {
            var baseResponse = new BaseResponse<List<EmployeeVM>>();
            try
            {
                var roleEmployee = await _userManager.GetRolesAsync(authorizedEmployee);
                if (roleEmployee[0].ToString() == "Сотрудник")
                {
                    var employee = new List<Employee>() { authorizedEmployee };
                    var listEmployeesVM = _mapper.Map<List<EmployeeVM>>(employee);
                    baseResponse.Data = listEmployeesVM;
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                else
                {
                    var listEmployees = await _userManager.Users.OrderBy(x => x.FirstName).ToListAsync();
                    var listEmployeesVM = _mapper.Map<List<EmployeeVM>>(listEmployees);
                    baseResponse.Data = listEmployeesVM;
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetEmployeesInCalendar]: {ex.Message}");
                return new BaseResponse<List<EmployeeVM>>()
                {
                    Description = $"[GetEmployeesInCalendar] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        #endregion
    }
}
